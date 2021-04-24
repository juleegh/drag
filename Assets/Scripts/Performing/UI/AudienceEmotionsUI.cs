using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class AudienceEmotionsUI : MonoBehaviour
{
    [Serializable]
    public class AudienceBarsDictionary : SerializableDictionaryBase<MoveType, EmotionBar> { }

    [SerializeField] private AudienceBarsDictionary bars;

    void Start()
    {
        foreach (KeyValuePair<MoveType, EmotionBar> bar in bars)
        {
            bar.Value.SetEmotion(bar.Key, PerformSystem.Instance.MovesProperties.ColorByMove[bar.Key]);
        }
    }

    public void SetSequenceEmotions(Dictionary<MoveType, float> emotions)
    {
        foreach (KeyValuePair<MoveType, float> emotion in emotions)
        {
            bars[emotion.Key].SetExpected(emotion.Value);
            bars[emotion.Key].SetFilled(0);
        }
    }

    public void SetEmotionProgress(MoveType emotion, float value)
    {
        bars[emotion].SetFilled(value);
    }
}
