using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;

public class TabSelector : MonoBehaviour
{
    [Serializable]
    public class TabButtons : SerializableDictionaryBase<GameObject, TabButton> { }

    [SerializeField] private TabButtons buttons;

    private void Awake()
    {
        bool first = true;
        foreach (TabButton button in buttons.Values)
        {
            button.SetButtonClick((button) => { ButtonPressed(button); });
            if (first)
            {
                first = false;
                ButtonPressed(button);
            }
        }
    }

    public void ButtonPressed(TabButton pressedButton)
    {
        foreach (KeyValuePair<GameObject, TabButton> button in buttons)
        {
            button.Key.SetActive(pressedButton == button.Value);
        }
    }

}
