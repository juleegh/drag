using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSequence
{
    public List<MoveSlot> slots;

    public void DebugSequence()
    {
        string sequence = "";
        for (int i = 0; i < slots.Count; i++)
        {
            sequence += slots[i].buff;
        }
        Debug.LogError(sequence);
    }
}
