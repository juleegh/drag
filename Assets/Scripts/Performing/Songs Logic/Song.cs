using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Song : ScriptableObject
{
    public string songName;
    public float tempo;
    public float duration;
    public AudioClip track;
    public float initialDelay;
    public List<string> sections;

    private Dictionary<int, MoveBuff> songBuffs;
    [SerializeField] private int movesQuantity;
    public int MovesQuantity { get { return movesQuantity; } }

    [ContextMenu("Load Song Info")]
    public void PlayableTempos()
    {
        songBuffs = new Dictionary<int, MoveBuff>();
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
