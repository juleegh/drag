using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardPiece
{
    private List<BoardPosition> cells;
    private BoardPieceType pieceType;

    public BoardPiece(BoardPieceType theType)
    {
        pieceType = theType;
        cells = BoardPiecesUtils.GetPositionsByPieceType(theType);
    }
}
