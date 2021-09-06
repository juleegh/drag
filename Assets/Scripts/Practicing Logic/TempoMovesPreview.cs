using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TempoMovesPreview : MonoBehaviour
{
    [SerializeField] private MovePreview[] movesPreviews;
    [SerializeField] private Image buffType;
    [SerializeField] private TextMeshProUGUI tempoID;
    [SerializeField] private MovesProperties properties;
    [SerializeField] private GameObject highlight;

    public void SetBuffInfo(MoveBuff buff, int tempo)
    {
        buffType.sprite = properties.SpriteByBuff[buff];
        tempoID.text = tempo.ToString();
        movesPreviews[0].SetMoveInfo(MoveType.Score);
        movesPreviews[1].SetMoveInfo(MoveType.Defense);
        movesPreviews[2].SetMoveInfo(MoveType.Attack);
        //movesPreviews[3].SetMoveInfo(MoveType.YType);
    }

    public void FillDanceMoves(DanceMove[] danceMoves)
    {
        for (int i = 0; i < movesPreviews.Length; i++)
        {
            string moveName = danceMoves[i] != null ? danceMoves[i].Identifier : "";
            int moveStamina = danceMoves[i] != null ? danceMoves[i].StaminaRequired : 0;
            movesPreviews[i].UpdateMoveText(moveName, moveStamina);
        }
    }

    public void ToggleSelected(bool selected)
    {
        highlight.SetActive(selected);
    }

    public void ToggleSelectedMove(int selectedMove)
    {
        for (int i = 0; i < movesPreviews.Length; i++)
        {
            movesPreviews[i].ToggleHighlight(selectedMove == i);
        }
    }
}
