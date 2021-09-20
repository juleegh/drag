using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI characterName;
    [SerializeField] private TextMeshProUGUI characterDialog;
    [SerializeField] private GameObject container;

    [SerializeField] private GameObject questionContainer;
    [SerializeField] private DialogQuestionVisuals questionVisuals;

    public void ToggleView(bool visible)
    {
        container.SetActive(visible);
    }

    public void SetContent(string theName, string theContent)
    {
        characterDialog.gameObject.SetActive(true);
        questionContainer.SetActive(false);

        characterName.text = theName;
        characterDialog.text = theContent;
    }

    public void SetContent(string theName, string question, string[] theContent)
    {
        characterDialog.gameObject.SetActive(false);
        questionContainer.SetActive(true);

        characterName.text = theName;
        questionVisuals.FillInfo(question, theContent[0], theContent[1]);
    }

    public void ToggleSelected(int selected)
    {
        questionVisuals.SetSelected(selected);
    }
}
