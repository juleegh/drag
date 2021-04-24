using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PerformSystem : MonoBehaviour
{
    public static PerformSystem Instance { get { return instance; } }
    private static PerformSystem instance;

    [SerializeField] private Song song;
    //[SerializeField] private Audience audience;
    [SerializeField] private EmotionFeed emotionFeed;
    [SerializeField] private TempoCounter tempo;
    [SerializeField] private MovePerformer performer;
    [SerializeField] private MoveSequenceSelector sequence;
    [SerializeField] private MoveSelectorUI ui;
    [SerializeField] private AudienceEmotionsUI audienceUi;
    [SerializeField] private MovesProperties movesProperties;

    public MovesProperties MovesProperties { get { return movesProperties; } }

    private MoveType moveMultType;
    private float moveTypeMult;

    private float engagement;
    private float happiness;
    private int currentMove;
    public int CurrentMoveIndex { get { return currentMove; } }
    public float TempoMovePercentage { get { return performState == PerformState.Executing ? tempo.TempoPercentage : 0f; } }
    public MoveSlot CurrentSlot { get { return sequence.Slots[currentMove]; } }
    public PerformState PerformState { get { return performState; } }

    private PerformState performState;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        tempo.SetTempo(song.tempo);
        performState = PerformState.PickingSequence;
        emotionFeed = new EmotionFeed();
        emotionFeed.GenerateRandom();
        audienceUi.SetSequenceEmotions(emotionFeed.EmotionsFeed);
        emotionFeed.Clean();
        performer.isPerforming = false;
        sequence.NewSequence();
        ui.CreateSlots(sequence.Slots);
    }

    public void StartPerforming()
    {
        emotionFeed.Clean();
        currentMove = 0;
        StartCoroutine(TinyWait());
    }

    private IEnumerator TinyWait()
    {
        performState = PerformState.Executing;
        yield return new WaitForSeconds(0.5f);
        tempo.StartTempoCount();
        performer.isPerforming = true;
    }

    public void NextMove()
    {
        if (currentMove >= sequence.Slots.Count - 1)
        {
            Sequence seq = DOTween.Sequence();
            seq.Pause();
            seq.AppendInterval(3f);
            seq.OnComplete(Create);
            performState = PerformState.PickingSequence;
            performer.isPerforming = false;
            tempo.StopTempoCount();
            sequence.NewSequence();
            seq.PlayForward();
            return;
        }

        if (!sequence.Slots[currentMove].performed)
        {
            sequence.Slots[currentMove].performed = true;
        }

        currentMove++;
    }

    public void PerformedMove(Move move)
    {
        if (sequence.Slots[currentMove].performed)
            return;

        sequence.Slots[currentMove].performed = true;

        bool isCorrect = tempo.IsOnTempo && move.moveType == sequence.SequenceMoves[currentMove].moveType;
        ui.PerformedMove(currentMove, isCorrect);

        if (isCorrect)
        {
            move.score *= sequence.Slots[currentMove].GetMultiplier();
            move.score = move.score / 100f;
            emotionFeed.ReactToMove(move);
            audienceUi.SetEmotionProgress(move.moveType, emotionFeed.EmotionsFeed[move.moveType]);
        }
    }

    private void Create()
    {
        ui.CreateSlots(sequence.Slots);
        emotionFeed.GenerateRandom();
        audienceUi.SetSequenceEmotions(emotionFeed.EmotionsFeed);
        emotionFeed.Clean();
    }
}
