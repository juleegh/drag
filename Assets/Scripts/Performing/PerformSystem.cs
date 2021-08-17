using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformSystem : MonoBehaviour, RequiredComponent
{
    public static PerformSystem Instance { get { return instance; } }
    private static PerformSystem instance;

    [SerializeField] private Song song;
    [SerializeField] private MovesProperties movesProperties;
    public MovesProperties MovesProperties { get { return movesProperties; } }
    private EmotionFeed emotionFeed;
    public EmotionFeed EmotionFeed { get { return emotionFeed; } }

    private int currentMove;
    public int CurrentMoveIndex { get { return currentMove; } }
    public MoveSlot CurrentSlot { get { return SongSequence.Instance.Slots[currentMove]; } }

    public PerformState PerformState { get { return performState; } }
    private PerformState performState;
    public float Tempo { get { return song.tempo; } }
    float delay = 0;
    float count = 0;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.TempoEnded, NextMove);
        //PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, StartPerformingSystem);
    }

    private void StartPerformingSystem()
    {
        SoundManager.Instance.SetTrack(song.track);
        TempoCounter.Instance.SetTempo(song.tempo);
        PosePerformer.Instance.SetTempo(song.tempo);
        LoadSongToSelector();
        performState = PerformState.PickingSequence;
        StartPerforming();
    }

    private void LoadSongToSelector()
    {
        SongLoader loader = new SongLoader();
        SongSequence.Instance.ConfigureSongSequences(loader.LoadSong(song));

        emotionFeed = new EmotionFeed();
        emotionFeed.DefineTargets(SongSequence.Instance.GetMovesForCurrentSequence());
    }

    private void StartPerforming()
    {
        currentMove = 0;
        SoundManager.Instance.StartTrack();
        StartCoroutine(TinyWait());
    }

    private void NextMove()
    {
        if (performState == PerformState.PickingSequence)
            return;

        PosePerformer.Instance.HitTempo();

        if (!SongSequence.Instance.Slots[currentMove].performed)
        {
            SongSequence.Instance.Slots[currentMove].performed = true;
        }

        currentMove++;

        if (SongSequence.Instance.GetNextSequenceIndex() == currentMove && currentMove != SongSequence.Instance.Slots.Count)
            emotionFeed.DefineTargets(SongSequence.Instance.GetMovesForCurrentSequence());

        if (currentMove == SongSequence.Instance.Slots.Count)
        {
            count--;
            performState = PerformState.PickingSequence;
            //TempoCounter.Instance.StopTempoCount();
        }
        else
        {
            PerformingEventsManager.Instance.Notify(PerformingEvent.MovesShifted);
        }
    }

    private IEnumerator TinyWait()
    {
        yield return new WaitForSeconds(song.initialDelay);
        performState = PerformState.Executing;
        TempoCounter.Instance.StartTempoCount();
    }

    public void PerformedMove(Move move)
    {
        if (TempoCounter.Instance.IsOnPostTempo)
            PostTempo(move);
        else
            PreTempo(move);
    }

    private void PreTempo(Move move)
    {
        if (SongSequence.Instance.Slots[currentMove].performed)
            return;

        bool isCorrect = TempoCounter.Instance.IsOnPreTempo;
        SongSequence.Instance.Slots[currentMove].performed = true;
        SongSequence.Instance.Slots[currentMove].correct = isCorrect;

        if (isCorrect)
        {
            move.score *= SongSequence.Instance.Slots[currentMove].GetMultiplier();
            emotionFeed.ReactToMove(move);
            PosePerformer.Instance.HitPose(move.poseType);
        }
        PerformingEventsManager.Instance.Notify(PerformingEvent.MovePerformed);
    }

    private void PostTempo(Move move)
    {
        if (SongSequence.Instance.Slots[currentMove - 1].performed)
            return;

        bool isCorrect = TempoCounter.Instance.IsOnPostTempo;
        SongSequence.Instance.Slots[currentMove - 1].performed = true;
        SongSequence.Instance.Slots[currentMove - 1].correct = isCorrect;

        if (isCorrect)
        {
            move.score *= SongSequence.Instance.Slots[currentMove - 1].GetMultiplier();
            emotionFeed.ReactToMove(move);
            PosePerformer.Instance.HitPose(move.poseType);
        }
        PerformingEventsManager.Instance.Notify(PerformingEvent.MovePerformed);
    }
}
