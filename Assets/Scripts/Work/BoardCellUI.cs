using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCellUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fill;

    public void ChangeFill(CellState newState)
    {
        switch (newState)
        {
            case CellState.Empty:
                fill.color = Color.black;
                break;
            case CellState.Moving:
                fill.color = Color.green;
                break;
            case CellState.Set:
                fill.color = Color.white;
                break;
        }
    }
}
