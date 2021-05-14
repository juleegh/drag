using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

[CreateAssetMenu(fileName = "Body Poses Settings")]
public class BodyPoseSettings : ScriptableObject
{
    [Serializable]
    public class PosesDictionary : SerializableDictionaryBase<PoseType, string> { }

    [SerializeField] private PosesDictionary poses;
    public PosesDictionary Poses { get { return poses; } }
}
