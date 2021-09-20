using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformSystem : MonoBehaviour, RequiredComponent
{
    public static PerformSystem Instance { get { return instance; } }
    private static PerformSystem instance;

    private Song song { get { return ProgressManager.Instance.CurrentLevel.BattleSong; } }
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

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.TempoEnded, NextMove);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.PlayerReadyToPerform, StartPerformingSystem);
    }

    private void StartPerformingSystem()
    {
        SoundManager.Instance.SetTrack(song.track);
        TempoCounter.Instance.SetTempo(song.tempo);
        PosePerformer.Instance.SetTempo(song.tempo);
        LoadSongToSelector();
        performState = PerformState.PickingSequence;
        StartPerforming();
        PerformingEventsManager.Instance.Notify(PerformingEvent.StartPerformance);
    }

    private void LoadSongToSelector()
    {
        SongSequence.Instance.ConfigureSongSequences(song);

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

        if (SongSequence.Instance.Slots[currentMove].buff != MoveBuff.None)
        {
            if (!SongSequence.Instance.Slots[currentMove].performed)
                PerformingEventsManager.Instance.Notify(PerformingEvent.TurnAdvanced);
        }

        if (!SongSequence.Instance.Slots[currentMove].performed)
        {
            SongSequence.Instance.Slots[currentMove].performed = true;
        }

        currentMove++;

        if (SongSequence.Instance.GetNextSequenceIndex() == currentMove && currentMove != SongSequence.Instance.Slots.Count)
            emotionFeed.DefineTargets(SongSequence.Instance.GetMovesForCurrentSequence());

        if (currentMove == SongSequence.Instance.Slots.Count)
        {
            currentMove--;
            performState = PerformState.PickingSequence;
            TempoCounter.Instance.StopTempoCount();
            PerformingEventsManager.Instance.Notify(PerformingEvent.PerformanceEnded);
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

    public void PerformedMove(PerformedMove move)
    {
        QualifyTempo(move, TempoCounter.Instance.IsOnPostTempo);
    }

    private void QualifyTempo(PerformedMove move, bool postTempo)
    {
        int moveIndex = postTempo ? currentMove - 1 : currentMove;
        if (SongSequence.Instance.Slots[moveIndex].performed || SongSequence.Instance.Slots[moveIndex].buff == MoveBuff.None)
            return;

        bool isOnPreTempo = TempoCounter.Instance.IsOnPreTempo && !postTempo;
        bool isOnPostTempo = TempoCounter.Instance.IsOnPostTempo && postTempo;

        bool isCorrect = isOnPreTempo || isOnPostTempo;
        SongSequence.Instance.Slots[moveIndex].performed = true;
        SongSequence.Instance.Slots[moveIndex].correct = isCorrect;

        if (isCorrect)
        {
            emotionFeed.ReactToMove(move);
            PosePerformer.Instance.HitPose(move.selectedMove.PoseType);
            DanceBattleManager.Instance.PlayerPerformedMove(move.selectedMove);
            PerformingEventsManager.Instance.Notify(PerformingEvent.TurnAdvanced);
            BattleSoundEffects.Instance.PlayEffect(move.moveType);
        }

        PerformingEventsManager.Instance.Notify(PerformingEvent.MovePerformed);
    }
}
