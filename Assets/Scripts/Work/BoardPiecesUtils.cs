using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BoardPiecesUtils
{
    public static Vector2Int[] GetPositionsByPieceType(BoardPieceType pieceType)
    {
        Vector2Int[] positions = new Vector2Int[1];

        switch (pieceType)
        {

            case BoardPieceType.Single_Square:
                positions = new Vector2Int[1];
                positions[0] = new Vector2Int(0, 0);
                break;

            case BoardPieceType.Double_Square:
                positions = new Vector2Int[4];
                positions[0] = new Vector2Int(0, 0);
                positions[1] = new Vector2Int(1, 0);
                positions[2] = new Vector2Int(0, 1);
                positions[3] = new Vector2Int(1, 1);
                break;

            case BoardPieceType.Two_Cells:
                positions = new Vector2Int[2];
                positions[0] = new Vector2Int(0, 0);
                positions[1] = new Vector2Int(1, 0);
                break;

            case BoardPieceType.Corner:
                positions = new Vector2Int[3];
                positions[0] = new Vector2Int(0, 0);
                positions[1] = new Vector2Int(1, 0);
                positions[2] = new Vector2Int(1, 1);
                break;
        }

        return positions;
    }

    public static BoardPieceType GetRandomPiece()
    {
        List<BoardPieceType> list = new List<BoardPieceType>();
        list.Add(BoardPieceType.Single_Square);
        list.Add(BoardPieceType.Double_Square);
        list.Add(BoardPieceType.Corner);
        list.Add(BoardPieceType.Two_Cells);

        return list[Random.Range(0, list.Count)];
    }
}
