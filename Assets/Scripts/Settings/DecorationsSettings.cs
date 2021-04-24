using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(fileName = "Decorations Settings")]
public class DecorationsSettings : ScriptableObject
{
    [Serializable]
    public class DecorationsDictionary : SerializableDictionaryBase<string, DecorationSetting> { }

    [SerializeField] private DecorationsDictionary decorations;
    public DecorationsDictionary Decorations { get { return decorations; } }
}


