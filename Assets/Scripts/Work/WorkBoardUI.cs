using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkBoardUI : MonoBehaviour
{
    [SerializeField] private Transform cellsContainer;
    [SerializeField] private GameObject boardCellPrefab;

    [SerializeField] private int widthSpacing;
    [SerializeField] private int heightSpacing;

    void Start()
    {
        CreateBoard();
    }

    private void CreateBoard()
    {
        for (int row = 0; row < WorkBoard.Instance.Height; row++)
        {
            for (int column = 0; column < WorkBoard.Instance.Width; column++)
            {
                BoardCellUI boardCell = Instantiate(boardCellPrefab).GetComponent<BoardCellUI>();
                boardCell.transform.SetParent(cellsContainer);
                boardCell.ChangeFill(WorkBoard.Instance.GetInPosition(column, row).CellState);
                boardCell.transform.localPosition = new Vector3(column * widthSpacing, row * widthSpacing, 0f);
            }
        }
    }
}
