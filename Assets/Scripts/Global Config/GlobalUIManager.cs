using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public enum UIGlobalType
{
    Money,
    Clock,
    Experience,
}

public class GlobalUIManager : MonoBehaviour
{
    [Serializable]
    public class UIValidationsDictionary : SerializableDictionaryBase<GameFunctions, UIFunctionsDictionary> { }

    [Serializable]
    public class UIFunctionsDictionary : SerializableDictionaryBase<UIGlobalType, bool> { }

    [SerializeField] private UIValidationsDictionary uiPresets;

    public void ReloadUI(GameFunctions nextScene)
    {
        if (!uiPresets.ContainsKey(nextScene))
            return;

        foreach (KeyValuePair<UIGlobalType, bool> preset in uiPresets[nextScene])
        {
            GameEvent toggleEvent = GameEvent.HideMoney;
            switch (preset.Key)
            {
                case UIGlobalType.Money:
                    toggleEvent = preset.Value ? GameEvent.ShowMoney : GameEvent.HideMoney;
                    break;
                case UIGlobalType.Clock:
                    toggleEvent = preset.Value ? GameEvent.ShowHour : GameEvent.HideHour;
                    break;
                case UIGlobalType.Experience:
                    toggleEvent = preset.Value ? GameEvent.ShowExperience : GameEvent.HideExperience;
                    break;
            }
            GameEventsManager.Instance.Notify(toggleEvent);
        }
    }
}