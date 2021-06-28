using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(fileName = "Wigs Config")]
public class WigsConfig : ScriptableObject
{
    [Serializable]
    public class WigsDictionary : SerializableDictionaryBase<WigType, WigConfig> { }

    [SerializeField] private WigsDictionary wigs;
    public WigsDictionary Wigs { get { return wigs; } }
}
