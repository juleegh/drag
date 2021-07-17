using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveOutfitUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputName;
    [SerializeField] private Button saveButton;

    void Awake()
    {
        saveButton.onClick.AddListener(TrySaveOutfit);
    }

    private void TrySaveOutfit()
    {
        if (!ValidName())
            return;
        else
        {
            SaveInfo();
        }
    }

    private bool ValidName()
    {
        if (inputName.text == "")
            return false;
        else if (inputName.text.Contains(" "))
            return false;
        return true;
    }

    private void SaveInfo()
    {
        GameDataManager.Instance.Save(inputName.text);
        GlobalPlayerManager.Instance.GoToLobby();
    }

}
