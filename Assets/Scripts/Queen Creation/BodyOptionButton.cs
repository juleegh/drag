using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BodyOptionButton : MonoBehaviour
{
    private Button button;
    private BodyType bodyType;
    private Action<BodyType> CallBack;

    public void Initialize(BodyType bt, Action<BodyType> cb)
    {
        button = GetComponent<Button>();
        bodyType = bt;
        CallBack = cb;
        button.onClick.AddListener(ButtonPressed);
    }

    private void ButtonPressed()
    {
        CallBack(bodyType);
    }
}
