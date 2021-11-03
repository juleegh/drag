using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBoardUI : MonoBehaviour
{
    private static WorkBoardUI instance;
    public static WorkBoardUI Instance { get { return instance; } }

    [SerializeField] private Transform cellsContainer;
    [SerializeField] private GameObject boardCellPrefab;

    [SerializeField] private int widthSpacing;
    [SerializeField] private int heightSpacing;

    private Dictionary<Vector2Int, BoardCellUI> cells;

    void Awake()
    {
        instance = this;
    }

    public void CreateBoard()
    {
        cells = new Dictionary<Vector2Int, BoardCellUI>();

        for (int row = 0; row < WorkBoard.Instance.Height; row++)
        {
            for (int column = 0; column < WorkBoard.Instance.Width; column++)
            {
                BoardCellUI boardCell = Instantiate(boardCellPrefab).GetComponent<BoardCellUI>();
                boardCell.transform.SetParent(cellsContainer);
                boardCell.ChangeFill(WorkBoard.Instance.GetInPosition(column, row).CellState);
                boardCell.transform.localPosition = new Vector3(column * widthSpacing, row * widthSpacing, 0f);
                cells.Add(new Vector2Int(column, row), boardCell);
            }
        }
    }

    public void Refresh()
    {
        for (int row = 0; row < WorkBoard.Instance.Height; row++)
        {
            for (int column = 0; column < WorkBoard.Instance.Width; column++)
            {
                BoardCellUI boardCell = cells[new Vector2Int(column, row)];
                boardCell.ChangeFill(WorkBoard.Instance.GetInPosition(column, row).CellState);
            }
        }
    }
}
