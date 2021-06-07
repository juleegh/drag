using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(fileName = "Skin Color Settings")]
public class SkinColorSettings : ScriptableObject
{
    [Serializable]
    public class ColorDictionary : SerializableDictionaryBase<ClothesColor, Texture> { }

    [Serializable]
    public class SkinsDictionary : SerializableDictionaryBase<SkinType, ColorDictionary> { }

    [SerializeField] private SkinsDictionary skinColors;
    public SkinsDictionary SkinColors { get { return skinColors; } }
}
