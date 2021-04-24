using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequenceSelector : MonoBehaviour
{
    [SerializeField] private PerformSystem performSystem;
    [SerializeField] private MoveSelectorUI ui;

    public bool isRecording;
    private List<Move> sequenceMoves;
    private List<MoveSlot> slots;
    public List<MoveSlot> Slots { get { return slots; } }
    public List<Move> SequenceMoves { get { return sequenceMoves; } }
    void Start()
    {
        slots = new List<MoveSlot>();
        sequenceMoves = new List<Move>();
    }

    public void NewSequence()
    {
        slots.Clear();
        sequenceMoves.Clear();

        int slotCount = Random.Range(4, 8);
        for (int i = 0; i < slotCount; i++)
        {
            MoveSlot slot = new MoveSlot();
            slot.SelectRandomBuff();
            slots.Add(slot);
        }
        isRecording = true;
    }

    void Update()
    {
        if (!isRecording || sequenceMoves.Count == slots.Count)
            return;

        if (Input.GetKeyDown(MovesInputManager.Instance.A))
        {
            Move move = new Move();
            move.moveType = MoveType.AType;
            move.score = 200;
            sequenceMoves.Add(move);
            ui.AddedMove(sequenceMoves.Count - 1, move);
        }
        else if (Input.GetKeyDown(MovesInputManager.Instance.B))
        {
            Move move = new Move();
            move.moveType = MoveType.BType;
            move.score = 200;
            sequenceMoves.Add(move);
            ui.AddedMove(sequenceMoves.Count - 1, move);
        }
        else if (Input.GetKeyDown(MovesInputManager.Instance.X))
        {
            Move move = new Move();
            move.moveType = MoveType.XType;
            move.score = 200;
            sequenceMoves.Add(move);
            ui.AddedMove(sequenceMoves.Count - 1, move);
        }
        else if (Input.GetKeyDown(MovesInputManager.Instance.Y))
        {
            Move move = new Move();
            move.moveType = MoveType.YType;
            move.score = 200;
            sequenceMoves.Add(move);
            ui.AddedMove(sequenceMoves.Count - 1, move);
        }

        if (sequenceMoves.Count == slots.Count)
        {
            isRecording = false;
            performSystem.StartPerforming();
        }
    }
}
