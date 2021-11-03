using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardPiecesUtils
{
    public static List<BoardPosition> GetPositionsByPieceType(BoardPieceType pieceType)
    {
        List<BoardPosition> positions = new List<BoardPosition>();

        switch (pieceType)
        {
            case BoardPieceType.Single_Square:
                positions.Add(GetPosition(0, 0));
                break;

            case BoardPieceType.Double_Square:
                positions.Add(GetPosition(0, 0));
                positions.Add(GetPosition(1, 0));
                positions.Add(GetPosition(0, 1));
                positions.Add(GetPosition(1, 1));
                break;

            case BoardPieceType.Two_Cells:
                positions.Add(GetPosition(0, 0));
                positions.Add(GetPosition(1, 0));
                break;

            case BoardPieceType.Corner:
                positions.Add(GetPosition(0, 0));
                positions.Add(GetPosition(1, 0));
                positions.Add(GetPosition(1, 1));
                break;
        }

        return positions;
    }

    public static BoardPosition GetPosition(int x, int y)
    {
        return new BoardPosition(x, y);
    }
}
