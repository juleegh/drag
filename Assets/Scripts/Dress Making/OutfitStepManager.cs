using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;

public class OutfitStepManager : MonoBehaviour, IRequiredComponent
{
    private static OutfitStepManager instance;
    public static OutfitStepManager Instance { get { return instance; } }

    [Serializable]
    public class TabButtons : SerializableDictionaryBase<OutfitStep, GameObject> { }

    [SerializeField] private TabButtons sections;

    private OutfitStep step;
    public OutfitStep CurrentOutfitStep { get { return step; } }

    public void ConfigureRequiredComponent()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, LoadUI);
    }

    private void LoadUI()
    {
        int index = 0;
        TabButton[] buttons = GetComponentsInChildren<TabButton>();
        foreach (TabButton button in buttons)
        {
            button.SetButtonClick((step) => { ButtonPressed(step); }, (OutfitStep)index);
            index++;
        }
        step = OutfitStep.Hair;
        ButtonPressed(OutfitStep.Hair);
    }

    public void ButtonPressed(OutfitStep pressedButton)
    {
        step = pressedButton;
        foreach (KeyValuePair<OutfitStep, GameObject> button in sections)
        {
            button.Value.SetActive(pressedButton == button.Key);
        }
        OutfitEventsManager.Instance.Notify(OutfitEvent.OutfitStepChanged);
    }

}
