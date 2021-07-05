using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SkinOptionButton : MonoBehaviour
{
    private Button button;
    private SkinType skinType;
    private Action<SkinType> CallBack;

    public void Initialize(SkinType bt, Action<SkinType> cb)
    {
        button = GetComponent<Button>();
        skinType = bt;
        CallBack = cb;
        button.onClick.AddListener(ButtonPressed);
    }

    private void ButtonPressed()
    {
        CallBack(skinType);
    }
}
