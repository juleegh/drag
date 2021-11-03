using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardCell
{
    private CellState cellState;
    public CellState CellState { get { return cellState; } }

    public BoardCell()
    {
        cellState = CellState.Empty;
    }

    public void ChangeCellState(CellState next)
    {
        cellState = next;
    }

}
