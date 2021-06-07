using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TabButton : MonoBehaviour
{
    [SerializeField] private Button button;
    private Action<TabButton> linkedAction;

    public void SetButtonClick(Action<TabButton> theAction)
    {
        linkedAction = theAction;
        button.onClick.AddListener(ButtonPressed);
    }

    private void ButtonPressed()
    {
        linkedAction(this);
    }
}
