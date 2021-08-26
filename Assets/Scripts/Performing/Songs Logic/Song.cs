using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;

public class Song : ScriptableObject
{
    public string songName;
    public float tempo;
    public float duration;
    public AudioClip track;
    public float initialDelay;
    public List<string> sections;

    [Serializable]
    public class TempoBuffs : SerializableDictionaryBase<int, MoveBuff> { }

    [SerializeField] private TempoBuffs songBuffs;
    [SerializeField] private int movesQuantity;

    public int MovesQuantity { get { return movesQuantity; } }
    public TempoBuffs SongBuffs { get { return songBuffs; } }

    [ContextMenu("Load Song Info")]
    public void PlayableTempos()
    {
        songBuffs = new TempoBuffs();
        int tempo = 0;

        foreach (string section in sections)
        {
            string[] splitArray = section.Split(char.Parse(","));

            foreach (string slot in splitArray)
            {
                MoveSlot newSlot = new MoveSlot();
                if (slot == "N")
                {
                    // Do nothing, we do not care
                }
                if (slot == "R")
                {
                    songBuffs.Add(tempo, MoveBuff.Regular);
                }
                if (slot == "D")
                {
                    songBuffs.Add(tempo, MoveBuff.Double);
                }
                if (slot == "H")
                {
                    songBuffs.Add(tempo, MoveBuff.Half);
                }
                tempo++;
            }
        }
        movesQuantity = songBuffs.Count;
    }
}
