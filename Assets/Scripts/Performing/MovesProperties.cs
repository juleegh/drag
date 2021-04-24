using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(fileName = "Moves Properties")]
public class MovesProperties : ScriptableObject
{
    [Serializable]
    public class MoveColorsDictionary : SerializableDictionaryBase<MoveType, Color> { }

    [SerializeField] private MoveColorsDictionary colorByMove;
    public MoveColorsDictionary ColorByMove { get { return colorByMove; } }
}
