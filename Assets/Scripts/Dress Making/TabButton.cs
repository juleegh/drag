using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TabButton : MonoBehaviour
{
    [SerializeField] private Button button;
    private Action<OutfitStep> linkedAction;
    OutfitStep step;

    public void SetButtonClick(Action<OutfitStep> theAction, OutfitStep outfitStep)
    {
        step = outfitStep;
        linkedAction = theAction;
        button.onClick.AddListener(ButtonPressed);
    }

    private void ButtonPressed()
    {
        linkedAction(step);
    }
}
