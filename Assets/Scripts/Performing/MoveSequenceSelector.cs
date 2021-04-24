using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceSelector : MonoBehaviour, IRequiredComponent
{
    public static MoveSequenceSelector Instance { get { return instance; } }
    private static MoveSequenceSelector instance;

    private List<Move> sequenceMoves;
    private List<MoveSlot> slots;
    public List<MoveSlot> Slots { get { return slots; } }
    public List<Move> SequenceMoves { get { return sequenceMoves; } }
    private List<MoveSequence> songSequences;
    private int currentSequence;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        slots = new List<MoveSlot>();
        sequenceMoves = new List<Move>();
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.WaitingForSequenceCreation, NextSequence);
    }

    public void ConfigureSongSequences(List<MoveSequence> sequences)
    {
        songSequences = sequences;
        currentSequence = -1;
    }

    private void NextSequence()
    {
        currentSequence++;
        sequenceMoves.Clear();
        slots = songSequences[currentSequence].slots;
        PerformingEventsManager.Instance.Notify(PerformingEvent.SequenceCreated);
    }

    void Update()
    {
        if (PerformSystem.Instance.PerformState != PerformState.PickingSequence || sequenceMoves.Count == slots.Count)
            return;

        if (Input.GetKeyDown(MovesInputManager.Instance.A))
        {
            PlayerSelectedMove(MoveType.AType);
        }
        else if (Input.GetKeyDown(MovesInputManager.Instance.B))
        {
            PlayerSelectedMove(MoveType.BType);
        }
        else if (Input.GetKeyDown(MovesInputManager.Instance.X))
        {
            PlayerSelectedMove(MoveType.XType);
        }
        else if (Input.GetKeyDown(MovesInputManager.Instance.Y))
        {
            PlayerSelectedMove(MoveType.YType);
        }

        if (sequenceMoves.Count == slots.Count)
        {
            PerformingEventsManager.Instance.Notify(PerformingEvent.WaitingForSequenceInput);
        }
    }

    private void PlayerSelectedMove(MoveType moveType)
    {
        Move move = new Move();
        move.moveType = moveType;
        move.score = 200;

        sequenceMoves.Add(move);
        PerformingEventsManager.Instance.Notify(PerformingEvent.SlotAdded);
    }
}
