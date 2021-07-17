using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class OutfitOptionButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI outfitNameTxt;
    private string outfit;
    private Action<string> buttonCallback;

    public void Initialize(Action<string> callback, string outfitName)
    {
        outfit = outfitName;
        outfitNameTxt.text = outfit;
        buttonCallback = callback;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(ButtonClick);
    }

    private void ButtonClick()
    {
        buttonCallback(outfit);
    }
}
