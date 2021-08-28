using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SongSequence : MonoBehaviour, RequiredComponent
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

    public void ConfigureSongSequences(Song song)
    {
        songSequences = song.GetSequences();
        sequenceIndexes = new List<int>();
        for (int i = 0; i < songSequences.Count; i++)
        {
            sequenceIndexes.Add(slots.Count);
            for (int j = 0; j < songSequences[i].slots.Count; j++)
            {
                slots.Add(songSequences[i].slots[j]);
            }
            sequenceIndexes.Add(slots.Count);
        }
        PerformingEventsManager.Instance.Notify(PerformingEvent.SequenceCreated);
    }

    public List<MoveSlot> GetMovesForCurrentSequence()
    {
        int start = 0;
        int finish = 0;
        for (int i = 1; i < sequenceIndexes.Count; i++)
        {
            if (sequenceIndexes[i - 1] <= PerformSystem.Instance.CurrentMoveIndex)
            {
                start = sequenceIndexes[i - 1];
                finish = sequenceIndexes[i];
            }
        }

        List<MoveSlot> nextSequence = new List<MoveSlot>();

        for (int j = start; j < finish; j++)
        {
            nextSequence.Add(slots[j]);
            //if (slots[j].GetMultiplier() != 0)
            //Debug.LogError(j + " - " + slots[j].GetMultiplier());
        }

        return nextSequence;
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

        for (int i = 0; i <= sequenceIndexes.Count && index == 0; i++)
        {
            if (sequenceIndexes[i] >= currentMove)
                index = sequenceIndexes[i];
        }
        return index;
    }

    void Update()
    {
        if (instance == null || PerformSystem.Instance.PerformState != PerformState.Executing)
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
        PerformedMove newMove = new PerformedMove();
        newMove.moveType = moveType;
        newMove.score = 200;
        newMove.AssignSelectedMove(GetMoveFromChoreography(moveType));

        slots[PerformSystem.Instance.CurrentMoveIndex].move = newMove;
        PerformSystem.Instance.PerformedMove(newMove);
    }

    private DanceMove GetMoveFromChoreography(MoveType moveType)
    {
        return PerformingChoreoLoader.Instance.Choreography.GetMoveInTempoByType(PerformSystem.Instance.CurrentMoveIndex, moveType);
    }
}
