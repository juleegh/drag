using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCellUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fill;

    public void ChangeFill(CellState newState)
    {
        fill.color = newState == CellState.Empty ? Color.black : Color.white;
    }
}
