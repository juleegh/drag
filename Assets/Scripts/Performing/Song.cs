using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Song")]
public class Song : ScriptableObject
{
    public string songName;
    public float tempo;
    public float duration;
    public AudioClip track;
}
