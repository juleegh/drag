using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public MoveSlot CurrentSlot { get { return SongSequence.Instance.Slots[currentMove]; } }

    public PerformState PerformState { get { return performState; } }
    private PerformState performState;
    float delay = 0;
    float count = 0;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.TempoEnded, NextMove);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.WaitingForSequenceInput, StartPerforming);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, StartPerformingSystem);
    }

    private void Update()
    {
        return;

        if (count > 0)
            delay += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            count += 1.0f;
            if (count > 1)
                Debug.LogError((float)delay / (count - 1));
        }
    }

    private void StartPerformingSystem()
    {
        SoundManager.Instance.SetTrack(song.track);
        TempoCounter.Instance.SetTempo(song.tempo);
        LoadSongToSelector();
        emotionFeed = new EmotionFeed();
        emotionFeed.GenerateRandom();
        performState = PerformState.PickingSequence;
        StartPerforming();
    }

    private void LoadSongToSelector()
    {
        SongLoader loader = new SongLoader();
        SongSequence.Instance.ConfigureSongSequences(loader.LoadSong(song));
    }

    private void StartPerforming()
    {
        currentMove = 0;
        SoundManager.Instance.StartTrack();
        StartCoroutine(TinyWait());
    }

    private void NextMove()
    {
        if (!SongSequence.Instance.Slots[currentMove].performed)
        {
            SongSequence.Instance.Slots[currentMove].performed = true;
        }

        currentMove++;
        if (currentMove == SongSequence.Instance.Slots.Count)
        {
            count--;
            performState = PerformState.PickingSequence;
            TempoCounter.Instance.StopTempoCount();
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
        if (SongSequence.Instance.Slots[currentMove].performed)
            return;

        bool isCorrect = TempoCounter.Instance.IsOnTempo;
        SongSequence.Instance.Slots[currentMove].performed = true;
        SongSequence.Instance.Slots[currentMove].correct = isCorrect;

        if (isCorrect)
        {
            move.score *= SongSequence.Instance.Slots[currentMove].GetMultiplier();
            move.score = move.score / 100f;
            emotionFeed.ReactToMove(move);
        }
        PerformingEventsManager.Instance.Notify(PerformingEvent.MovePerformed);
    }
}
