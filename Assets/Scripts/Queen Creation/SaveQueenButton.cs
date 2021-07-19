using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveQueenButton : MonoBehaviour
{
    [SerializeField] private Button readyButton;
    [SerializeField] private TMP_InputField nameField;

    void Start()
    {
        readyButton.onClick.AddListener(TryToSave);
    }

    private void TryToSave()
    {
        if (nameField.text == "")
            return;
        else
        {
            BodyTypePersonalization.Instance.TryToSave(nameField.text);
        }
    }
}
