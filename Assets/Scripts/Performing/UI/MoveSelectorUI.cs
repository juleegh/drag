using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveSelectorUI : MonoBehaviour, IRequiredComponent
{
    [SerializeField] private SimpleObjectPool pool;
    [SerializeField] private Transform container;
    [SerializeField] private GameObject indicator;
    [SerializeField] private float moveDistance = 60f;

    private List<MoveUI> moves;
    private int currentMove;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.SequenceCreated, CreateSlots);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.MovesShifted, ShiftMoves);
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
        List<MoveSlot> slots = SongSequence.Instance.Slots;
        CleanMoves();

        if (moves == null)
            moves = new List<MoveUI>();

        int amountOfMoves = PerformSystem.Instance.MovesProperties.MovesAhead + PerformSystem.Instance.MovesProperties.MovesBefore + 1;
        for (int i = 0; i < amountOfMoves; i++)
        {
            GameObject move = pool.GetObject();
            MoveUI ui = move.GetComponent<MoveUI>();
            if (i >= PerformSystem.Instance.MovesProperties.MovesBefore)
                ui.MarkAsBuff(slots[i - PerformSystem.Instance.MovesProperties.MovesBefore].buff);
            else
                ui.MarkAsEmpty();
            ui.transform.SetParent(container);
            float xPosition = ((float)(2 * i - amountOfMoves) / amountOfMoves) * (amountOfMoves * moveDistance / 2);
            ui.transform.position = transform.position + Vector3.right * xPosition;
            moves.Add(ui);

            if (i == PerformSystem.Instance.MovesProperties.MovesBefore)
                indicator.transform.position = ui.transform.position;
        }
    }

    private void RefreshMoves()
    {
        int amountOfMoves = PerformSystem.Instance.MovesProperties.MovesAhead + PerformSystem.Instance.MovesProperties.MovesBefore + 1;
        for (int i = PerformSystem.Instance.MovesProperties.MovesBefore; i < amountOfMoves; i++)
        {
            int index = PerformSystem.Instance.CurrentMoveIndex + i - PerformSystem.Instance.MovesProperties.MovesBefore;
            if (index >= 0)
            {
                if (index < SongSequence.Instance.Slots.Count)
                {
                    Move move = SongSequence.Instance.Slots[index].move;
                    moves[i].MarkAsBuff(SongSequence.Instance.Slots[index].buff);
                    if (move != null)
                        moves[i].MarkAsMove(move.moveType);
                }
                else
                    moves[i].MarkAsEmpty();

            }
        }
    }

    private void ShiftMoves()
    {
        GameObject nextMove = pool.GetObject();
        MoveUI ui = nextMove.GetComponent<MoveUI>();
        int index = PerformSystem.Instance.CurrentMoveIndex + PerformSystem.Instance.MovesProperties.MovesAhead + 1;

        if (index < SongSequence.Instance.Slots.Count)
            ui.MarkAsBuff(SongSequence.Instance.Slots[index].buff);
        else
            ui.MarkAsEmpty();

        ui.transform.SetParent(container);
        ui.transform.position = moves[moves.Count - 1].transform.position + Vector3.right * moveDistance;
        moves.Add(ui);

        for (int i = 0; i < moves.Count; i++)
        {
            moves[i].transform.DOMoveX(moves[i].transform.position.x - moveDistance, PerformSystem.Instance.Tempo).SetEase(Ease.Linear);
        }

        MoveUI old = moves[0];
        moves.Remove(old);
        pool.ReturnObject(old.gameObject);

        RefreshMoves();
    }

    public void PerformedMove()
    {
        moves[PerformSystem.Instance.MovesProperties.MovesBefore].MarkAsMove(PerformSystem.Instance.CurrentSlot.move.moveType);
        bool correct = PerformSystem.Instance.CurrentSlot.correct;
        moves[PerformSystem.Instance.MovesProperties.MovesBefore].MarkCompleted(correct);
    }
}
