using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectorUI : MonoBehaviour, IRequiredComponent
{
    [SerializeField] private SimpleObjectPool pool;
    [SerializeField] private Transform container;

    private List<MoveUI> moves;
    private int currentMove;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.SlotAdded, AddedMove);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.SequenceCreated, CreateSlots);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.MovePerformed, PerformedMove);
    }

    public void CleanMoves()
    {
        if (moves == null)
            return;

        foreach (MoveUI move in moves)
        {
            pool.ReturnObject(move.gameObject);
        }
        moves.Clear();
    }

    private void CreateSlots()
    {
        List<MoveSlot> slots = MoveSequenceSelector.Instance.Slots;
        CleanMoves();

        if (moves == null)
            moves = new List<MoveUI>();

        for (int i = 0; i < slots.Count; i++)
        {
            GameObject move = pool.GetObject();
            MoveUI ui = move.GetComponent<MoveUI>();
            ui.MarkAsEmpty(slots[i].buff);
            ui.transform.SetParent(container);
            moves.Add(ui);
        }
    }

    private void Update()
    {
        int index = PerformSystem.Instance.CurrentMoveIndex;
        if (PerformSystem.Instance.PerformState == PerformState.Executing && index < moves.Count && !PerformSystem.Instance.CurrentSlot.performed)
        {
            moves[index].MarkProgress(TempoCounter.Instance.TempoPercentage);
        }
    }

    public void AddedMove()
    {
        int index = MoveSequenceSelector.Instance.SequenceMoves.Count - 1;
        Move move = MoveSequenceSelector.Instance.SequenceMoves[index];

        moves[index].MarkAsMove(move.moveType);
    }

    public void PerformedMove()
    {
        int index = PerformSystem.Instance.CurrentMoveIndex;
        bool correct = PerformSystem.Instance.CurrentSlot.correct;
        if (moves == null && index >= moves.Count)
            return;

        moves[index].MarkCompleted(correct);
    }
}
