using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveTypePreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveTitle;
    [SerializeField] private TextMeshProUGUI staminaCounter;
    [SerializeField] private GameObject highlight;

    public void UpdateMoveInfo(string title, int stamina)
    {
        moveTitle.text = title;
        staminaCounter.text = "SP: " + stamina;
    }

    public void SetEmpty()
    {
        moveTitle.text = "";
        staminaCounter.text = "";
        ToggleSelected(false);
    }

    public void ToggleSelected(bool visible)
    {
        highlight.SetActive(visible);
    }
}
