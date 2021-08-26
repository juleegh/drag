using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class MovesProperties : ScriptableObject
{
    [Serializable]
    public class MoveColorsDictionary : SerializableDictionaryBase<MoveType, Color> { }

    [Serializable]
    public class BuffsIconsDictionary : SerializableDictionaryBase<MoveBuff, Sprite> { }

    [SerializeField] private MoveColorsDictionary colorByMove;
    public MoveColorsDictionary ColorByMove { get { return colorByMove; } }

    [SerializeField] private BuffsIconsDictionary spriteByBuff;
    public BuffsIconsDictionary SpriteByBuff { get { return spriteByBuff; } }

    [SerializeField] private float acceptablePercentage;
    public float AcceptablePercentage { get { return acceptablePercentage; } }
    [SerializeField] private int movesBefore;
    public int MovesBefore { get { return movesBefore; } }
    [SerializeField] private int movesAhead;
    public int MovesAhead { get { return movesAhead; } }
}
