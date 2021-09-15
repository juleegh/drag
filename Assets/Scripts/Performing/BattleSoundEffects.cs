using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;

public class BattleSoundEffects : MonoBehaviour
{
    public static BattleSoundEffects Instance { get { return instance; } }
    private static BattleSoundEffects instance;

    [SerializeField] private AudioSource audioSource;

    [Serializable]
    public class AudioClipsDictionary : SerializableDictionaryBase<MoveType, AudioClip> { }

    [SerializeField] private AudioClipsDictionary audioClips;

    void Awake()
    {
        instance = this;
    }

    public void PlayEffect(MoveType moveType)
    {
        audioSource.PlayOneShot(audioClips[moveType]);
    }

}
