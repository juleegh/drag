using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class SongLoader
{
    public List<MoveSequence> LoadSong(Song song)
    {
        List<MoveSequence> sections = new List<MoveSequence>();

        for (int i = 0; i < song.sections.Count; i++)
        {
            MoveSequence sequence = new MoveSequence();
            sequence.slots = RecoverSequence(song.sections[i]);
            sections.Add(sequence);
        }

        return sections;
    }

    private List<MoveSlot> RecoverSequence(string chain)
    {
        List<MoveSlot> sequence = new List<MoveSlot>();
        string[] splitArray = chain.Split(char.Parse(","));

        foreach (string slot in splitArray)
        {
            MoveSlot newSlot = new MoveSlot();
            if (slot == "N")
                newSlot.buff = MoveBuff.None;
            if (slot == "R")
                newSlot.buff = MoveBuff.Regular;
            if (slot == "D")
                newSlot.buff = MoveBuff.Double;
            if (slot == "H")
                newSlot.buff = MoveBuff.Half;

            sequence.Add(newSlot);
        }
        return sequence;
    }
}
