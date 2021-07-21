using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BodyOptionButton : MonoBehaviour
{
    private Button button;
    private BodyMesh bodyType;
    private Action<BodyMesh> CallBack;

    public void Initialize(BodyMesh bt, Action<BodyMesh> cb)
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
