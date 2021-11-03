using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece
{
    private Vector2Int[] cells;
    private BoardPieceType pieceType;

    public Vector2Int[] Cells { get { return cells; } }

    public BoardPiece(BoardPieceType theType)
    {
        pieceType = theType;
        cells = BoardPiecesUtils.GetPositionsByPieceType(theType);
    }

    public bool TryToMove(int x, int y)
    {
        foreach (Vector2Int position in cells)
        {
            if (position.x + x >= WorkBoard.Instance.Width || position.x + x < 0)
                return false;
            else if (position.y + y >= WorkBoard.Instance.Height || position.y + y < 0)
                return false;

            if (WorkBoard.Instance.GetInPosition(position.x + x, position.y + y).CellState == CellState.Set)
                return false;
        }

        return true;
    }

    public bool TryToRotate(int x, int y)
    {
        int[] horizontals = new int[cells.Length];
        int[] verticals = new int[cells.Length];

        for (int i = 0; i < cells.Length; i++)
        {
            horizontals[i] = 1 - cells[i].y;
            verticals[i] = cells[i].x;

            if (horizontals[i] + x >= WorkBoard.Instance.Width || horizontals[i] + x < 0)
                return false;
            if (verticals[i] + y >= WorkBoard.Instance.Height || verticals[i] + y < 0)
                return false;

            if (WorkBoard.Instance.GetInPosition(horizontals[i] + x, verticals[i] + y).CellState == CellState.Set)
                return false;
        }

        for (int i = 0; i < cells.Length; i++)
        {
            cells[i] = new Vector2Int(horizontals[i], verticals[i]);
        }

        return true;
    }
}
