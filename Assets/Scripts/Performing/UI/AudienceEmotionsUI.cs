using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class AudienceEmotionsUI : MonoBehaviour, RequiredComponent
{
    [Serializable]
    public class AudienceBarsDictionary : SerializableDictionaryBase<MoveType, EmotionBar> { }

    [SerializeField] private AudienceBarsDictionary bars;

    public void ConfigureRequiredComponent()
    {
        foreach (KeyValuePair<MoveType, EmotionBar> bar in bars)
        {
            bar.Value.SetEmotion(bar.Key, PerformSystem.Instance.MovesProperties.ColorByMove[bar.Key]);
        }
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.CreatedAudienceEmotions, SetSequenceEmotions);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.MovePerformed, SetEmotionProgress);

        foreach (KeyValuePair<MoveType, EmotionBar> emotion in bars)
        {
            bars[emotion.Key].SetExpected(0);
            bars[emotion.Key].SetFilled(0);
        }
    }

    public void SetSequenceEmotions()
    {
        foreach (KeyValuePair<MoveType, float> emotion in PerformSystem.Instance.EmotionFeed.TargetEmotions)
        {
            bars[emotion.Key].SetExpected(emotion.Value / SongSequence.Instance.GetTotalScore());
        }
    }

    private void SetEmotionProgress()
    {
        MoveType emotion = SongSequence.Instance.Slots[PerformSystem.Instance.CurrentMoveIndex].move.moveType;
        float value = PerformSystem.Instance.EmotionFeed.CurrentEmotions[emotion];
        bars[emotion].SetFilled(value / SongSequence.Instance.GetTotalScore());
    }
}
