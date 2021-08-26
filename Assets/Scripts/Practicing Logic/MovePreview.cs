using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovePreview : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moveTitle;
    [SerializeField] private Image background;
    [SerializeField] private Image highlight;
    [SerializeField] private MovesProperties properties;

    public void SetMoveInfo(MoveType moveType)
    {
        background.color = properties.ColorByMove[moveType];
        highlight.gameObject.SetActive(false);
    }

    public void UpdateMoveText(string moveName)
    {
        moveTitle.text = moveName;
    }

    public void ToggleHighlight(bool visible)
    {
        highlight.gameObject.SetActive(visible);
    }
}
