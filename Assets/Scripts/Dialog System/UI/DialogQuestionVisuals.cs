using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogQuestionVisuals : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI question;
    [SerializeField] private TextMeshProUGUI firstOption;
    [SerializeField] private TextMeshProUGUI secondOption;

    [SerializeField] private Image firstSelection;
    [SerializeField] private Image secondSelection;

    [SerializeField] private Color transparent;
    [SerializeField] private Color selected;

    public void FillInfo(string q, string a1, string a2)
    {
        question.text = q;
        firstOption.text = a1;
        secondOption.text = a2;
    }

    public void SetSelected(int index)
    {
        firstSelection.color = index == 0 ? selected : transparent;
        secondSelection.color = index == 1 ? selected : transparent;
    }
}
