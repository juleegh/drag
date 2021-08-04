using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DragTabButton : MonoBehaviour
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

    public void MarkAsSelected(bool selected)
    {
        button.image.color = selected ? Color.green : Color.white;
        button.transform.localScale = selected ? Vector3.one * 1.1f : Vector3.one;
    }
}
