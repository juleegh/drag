using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PerformSystem : MonoBehaviour, IRequiredComponent
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
    public MoveSlot CurrentSlot { get { return MoveSequenceSelector.Instance.Slots[currentMove]; } }

    public PerformState PerformState { get { return performState; } }
    private PerformState performState;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.WaitingForSequenceInput, StartPerforming);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.TempoEnded, NextMove);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, StartPerformingSystem);
    }

    private void StartPerformingSystem()
    {
        TempoCounter.Instance.SetTempo(song.tempo);
        LoadSongToSelector();
        emotionFeed = new EmotionFeed();
        emotionFeed.GenerateRandom();
        performState = PerformState.PickingSequence;
        PerformingEventsManager.Instance.Notify(PerformingEvent.WaitingForSequenceCreation);
    }

    private void LoadSongToSelector()
    {
        SongLoader loader = new SongLoader();
        MoveSequenceSelector.Instance.ConfigureSongSequences(loader.LoadSong(song));
    }

    private void StartPerforming()
    {
        currentMove = 0;
        StartCoroutine(TinyWait());
    }

    private IEnumerator TinyWait()
    {
        yield return new WaitForSeconds(0.5f);
        performState = PerformState.Executing;
        TempoCounter.Instance.StartTempoCount();
    }

    public void NextMove()
    {
        if (currentMove >= MoveSequenceSelector.Instance.Slots.Count - 1)
        {
            Sequence seq = DOTween.Sequence();
            seq.Pause();
            seq.AppendInterval(3f);
            seq.OnComplete(Create);
            performState = PerformState.PickingSequence;
            TempoCounter.Instance.StopTempoCount();
            //sequence.NewSequence();
            seq.PlayForward();
            return;
        }

        if (!MoveSequenceSelector.Instance.Slots[currentMove].performed)
        {
            MoveSequenceSelector.Instance.Slots[currentMove].performed = true;
        }

        currentMove++;
    }

    public void PerformedMove(Move move)
    {
        if (MoveSequenceSelector.Instance.Slots[currentMove].performed)
            return;


        bool isCorrect = TempoCounter.Instance.IsOnTempo && move.moveType == MoveSequenceSelector.Instance.SequenceMoves[currentMove].moveType;
        MoveSequenceSelector.Instance.Slots[currentMove].performed = true;
        MoveSequenceSelector.Instance.Slots[currentMove].correct = isCorrect;

        if (isCorrect)
        {
            move.score *= MoveSequenceSelector.Instance.Slots[currentMove].GetMultiplier();
            move.score = move.score / 100f;
            emotionFeed.ReactToMove(move);
        }
        PerformingEventsManager.Instance.Notify(PerformingEvent.MovePerformed);
    }

    private void Create()
    {
        emotionFeed.GenerateRandom();
        PerformingEventsManager.Instance.Notify(PerformingEvent.WaitingForSequenceCreation);
    }
}
