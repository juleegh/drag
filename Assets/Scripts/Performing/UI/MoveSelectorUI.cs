using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSelectorUI : MonoBehaviour
{
    [SerializeField] private SimpleObjectPool pool;
    [SerializeField] private Transform container;

    private List<MoveUI> moves;
    private int currentMove;

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

    public void CreateSlots(List<MoveSlot> slots)
    {
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
            moves[index].MarkProgress(PerformSystem.Instance.TempoMovePercentage);
        }
    }

    public void AddedMove(int index, Move move)
    {
        if (moves == null && index >= moves.Count)
            return;

        moves[index].MarkAsMove(move.moveType);
    }

    public void PerformedMove(int index, bool correct)
    {
        if (moves == null && index >= moves.Count)
            return;

        moves[index].MarkCompleted(correct);
    }
}
