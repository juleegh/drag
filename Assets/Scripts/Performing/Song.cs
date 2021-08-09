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
}
