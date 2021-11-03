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

    private Dictionary<Vector2Int, BoardCell> board;
    private BoardPiece currentPiece;
    private Vector2Int currentPosition;

    void Awake()
    {
        instance = this;
        InitializeBoard();
    }

    void Start()
    {
        WorkBoardUI.Instance.CreateBoard();
        SpawnNextPiece();
    }

    void InitializeBoard()
    {
        board = new Dictionary<Vector2Int, BoardCell>();

        for (int row = 0; row < height; row++)
        {
            for (int column = 0; column < width; column++)
            {
                BoardCell newCell = new BoardCell();
                board.Add(new Vector2Int(column, row), newCell);
            }
        }

    }

    public BoardCell GetInPosition(Vector2Int position)
    {
        return board[position];
    }

    public BoardCell GetInPosition(int x, int y)
    {
        Vector2Int position = new Vector2Int(x, y);
        return board[position];
    }

    private void SpawnNextPiece()
    {
        if (currentPiece != null)
            ChangeCurrentPieceState(CellState.Set);

        currentPosition = new Vector2Int(width / 2, height / 2);
        currentPiece = new BoardPiece(GetRandomPiece());

        ChangeCurrentPieceState(CellState.Moving);
        WorkBoardUI.Instance.Refresh();
    }

    private void RotateCurrentPiece()
    {
        ChangeCurrentPieceState(CellState.Empty);
        currentPiece.TryToRotate(currentPosition.x, currentPosition.y);
        ChangeCurrentPieceState(CellState.Moving);
        WorkBoardUI.Instance.Refresh();
    }

    private void Update()
    {
        if (currentPiece != null)
        {
            int deltaX = 0;
            int deltaY = 0;

            if (Input.GetKeyDown(KeyCode.LeftArrow))
                deltaX = -1;
            else if (Input.GetKeyDown(KeyCode.RightArrow))
                deltaX = 1;
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                deltaY = -1;
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                deltaY = 1;
            else if (Input.GetKeyDown(KeyCode.Space))
                SpawnNextPiece();
            else if (Input.GetKeyDown(KeyCode.R))
                RotateCurrentPiece();

            if (deltaX != 0 || deltaY != 0)
            {
                bool result = currentPiece.TryToMove(currentPosition.x + deltaX, currentPosition.y + deltaY);
                if (result)
                {
                    ChangeCurrentPieceState(CellState.Empty);
                    currentPosition += new Vector2Int(deltaX, deltaY);
                    ChangeCurrentPieceState(CellState.Moving);
                    WorkBoardUI.Instance.Refresh();
                }
            }
        }
    }

    private void ChangeCurrentPieceState(CellState newState)
    {
        foreach (Vector2Int position in currentPiece.Cells)
        {
            board[currentPosition + position].ChangeCellState(newState);
        }
    }

    private BoardPieceType GetRandomPiece()
    {
        List<BoardPieceType> list = new List<BoardPieceType>();
        list.Add(BoardPieceType.Single_Square);
        list.Add(BoardPieceType.Double_Square);
        list.Add(BoardPieceType.Corner);
        list.Add(BoardPieceType.Two_Cells);

        return list[Random.Range(0, list.Count)];
    }
}
