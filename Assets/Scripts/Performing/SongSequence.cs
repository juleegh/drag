using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSequence : MonoBehaviour, IRequiredComponent
{
    public static SongSequence Instance { get { return instance; } }
    private static SongSequence instance;

    private List<int> sequenceIndexes;
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
        sequenceIndexes = new List<int>();
        for (int i = 0; i < sequences.Count; i++)
        {
            sequenceIndexes.Add(slots.Count);
            for (int j = 0; j < sequences[i].slots.Count; j++)
            {
                slots.Add(sequences[i].slots[j]);
            }
        }
        PerformingEventsManager.Instance.Notify(PerformingEvent.SequenceCreated);
    }

    public float GetScoreForCurrentSequence()
    {
        float score = 0;
        int index = GetNextSequenceIndex();

        for (int j = 0; j < index; j++)
        {
            score += 200 * slots[j].GetMultiplier();
        }

        return score;
    }

    public float GetTotalScore()
    {
        float score = 0;

        for (int j = 0; j < slots.Count; j++)
        {
            score += 200 * slots[j].GetMultiplier();
        }

        return score;
    }

    public int GetNextSequenceIndex()
    {
        int currentMove = PerformSystem.Instance.CurrentMoveIndex;
        int index = 0;

        for (int i = 0; i < sequenceIndexes.Count && index == 0; i++)
        {
            if (sequenceIndexes[i] >= currentMove)
                index = sequenceIndexes[i];
        }
        return index;
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
