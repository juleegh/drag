using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSequence : MonoBehaviour, IRequiredComponent
{
    public static SongSequence Instance { get { return instance; } }
    private static SongSequence instance;

    private List<MoveSlot> slots;
    public List<MoveSlot> Slots { get { return slots; } }
    private List<MoveSequence> songSequences;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        slots = new List<MoveSlot>();
    }

    public void ConfigureSongSequences(List<MoveSequence> sequences)
    {
        songSequences = sequences;
        for (int i = 0; i < sequences.Count; i++)
        {
            for (int j = 0; j < sequences[i].slots.Count; j++)
            {
                slots.Add(sequences[i].slots[j]);
            }
        }
        PerformingEventsManager.Instance.Notify(PerformingEvent.SequenceCreated);
    }

    void Update()
    {
        if (PerformSystem.Instance.PerformState != PerformState.Executing)
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
    }

    private void PlayerSelectedMove(MoveType moveType)
    {
        Move newMove = new Move();
        newMove.moveType = moveType;
        newMove.score = 200;

        slots[PerformSystem.Instance.CurrentMoveIndex].move = newMove;
        PerformSystem.Instance.PerformedMove(newMove);
    }
}
