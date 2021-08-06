using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;

public class OutfitStepManager : MonoBehaviour, RequiredComponent
{
    private static OutfitStepManager instance;
    public static OutfitStepManager Instance { get { return instance; } }

    [Serializable]
    public class TabButtons : SerializableDictionaryBase<OutfitStep, GameObject> { }

    [SerializeField] private TabButtons sections;
    [SerializeField] private Button cancelButton;

    private Dictionary<OutfitStep, DragTabButton> buttons;
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
        buttons = new Dictionary<OutfitStep, DragTabButton>();
        DragTabButton[] containerButtons = GetComponentsInChildren<DragTabButton>();
        foreach (DragTabButton button in containerButtons)
        {
            button.SetButtonClick((step) => { ButtonPressed(step); }, (OutfitStep)index);
            buttons.Add((OutfitStep)index, button);
            index++;
        }
        step = OutfitStep.Hair;
        ButtonPressed(OutfitStep.Hair);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(CancelOutfit);
    }

    public void ButtonPressed(OutfitStep pressedButton)
    {
        step = pressedButton;
        foreach (KeyValuePair<OutfitStep, GameObject> button in sections)
        {
            button.Value.SetActive(pressedButton == button.Key);
            buttons[button.Key].MarkAsSelected(pressedButton == button.Key);
        }
        OutfitEventsManager.Instance.Notify(OutfitEvent.OutfitStepChanged);
    }

    private void CancelOutfit()
    {
        GameEventsManager.Instance.Notify(GameEvent.OutfitCanceled);
        GlobalPlayerManager.Instance.GoToLobby();
    }

}
