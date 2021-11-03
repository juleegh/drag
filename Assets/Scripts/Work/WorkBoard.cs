using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBoard : MonoBehaviour
{
    private static WorkBoard instance;
    public static WorkBoard Instance { get { return instance; } }

    [SerializeField] private int width;
    [SerializeField] private int height;

    public int Width { get { return width; } }
    public int Height { get { return height; } }

    private Dictionary<BoardPosition, BoardCell> board;

    void Awake()
    {
        instance = this;
        InitializeBoard();
    }

    void InitializeBoard()
    {
        board = new Dictionary<BoardPosition, BoardCell>();

        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
            {
                BoardCell newCell = new BoardCell();
                board.Add(BoardPiecesUtils.GetPosition(column, row), newCell);
            }
        }
    }

    public BoardCell GetInPosition(BoardPosition position)
    {
        return board[position];
    }

    public BoardCell GetInPosition(int x, int y)
    {
        BoardPosition position = new BoardPosition(x, y);
        return board[position];
    }
}
