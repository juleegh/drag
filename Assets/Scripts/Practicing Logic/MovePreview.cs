using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovePreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveTitle;
    [SerializeField] private TextMeshProUGUI moveStamina;
    [SerializeField] private Image background;
    [SerializeField] private Image highlight;
    [SerializeField] private MovesProperties properties;

    public void SetMoveInfo(MoveType moveType)
    {
        background.color = properties.ColorByMove[moveType];
        highlight.gameObject.SetActive(false);
    }

    public void UpdateMoveText(string moveName, int stamina)
    {
        moveTitle.text = moveName;
        if (stamina > 0)
            moveStamina.text = "SP: " + stamina;
        else
            moveStamina.text = "";
    }

    public void ToggleHighlight(bool visible)
    {
        highlight.gameObject.SetActive(visible);
    }
}
