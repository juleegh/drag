using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterDialog;
    [SerializeField] private GameObject container;

    public void ToggleView(bool visible)
    {
        container.SetActive(visible);
    }

    public void SetContent(string theName, string theContent)
    {
        characterName.text = theName;
        characterDialog.text = theContent;
    }

    public void SetContent(string theName, string question, string[] theContent)
    {
        characterName.text = theName;
        string content = question + "\n";
        foreach (string line in theContent)
            content += line + "\n";
        characterDialog.text = content;
    }
}
