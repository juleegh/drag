using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class BoardAlgorithmsUtils
{
    private static int width { get { return WorkBoard.Instance.Width; } }
    private static int height { get { return WorkBoard.Instance.Height; } }

    public static void CheckLines(Dictionary<Vector2Int, BoardCell> board)
    {
        CheckFromTheTop(board);
        CheckFromTheRight(board);
        CheckFromTheBottom(board);
        CheckFromTheLeft(board);
    }

    private static void CheckFromTheTop(Dictionary<Vector2Int, BoardCell> board)
    {
        // UP TO CENTER
        for (int row = height - 1; row >= height / 2 - 1; row--)
        {
            bool full = true;
            for (int column = 0; column < width && full; column++)
            {
                if (board[new Vector2Int(column, row)].CellState != CellState.Set)
                    full = false;
            }

            if (full)
            {
                for (int column = 0; column < width; column++)
                {
                    board[new Vector2Int(column, row)].ChangeCellState(CellState.Empty);
                }

                for (int currentRow = row - 1; currentRow >= height / 2 - 1; currentRow--)
                {
                    for (int column = 0; column < width; column++)
                    {
                        board[new Vector2Int(column, currentRow + 1)].ChangeCellState(board[new Vector2Int(column, currentRow)].CellState);
                    }
                }
                return;
            }
        }
    }

    private static void CheckFromTheRight(Dictionary<Vector2Int, BoardCell> board)
    {
        // RIGHT TO CENTER
        for (int column = width - 1; column >= width / 2 - 1; column--)
        {
            bool full = true;
            for (int row = 0; row < height && full; row++)
            {
                if (board[new Vector2Int(column, row)].CellState != CellState.Set)
                    full = false;
            }

            if (full)
            {
                for (int row = 0; row < height; row++)
                {
                    board[new Vector2Int(column, row)].ChangeCellState(CellState.Empty);
                }

                for (int currentColumn = column - 1; currentColumn >= width / 2 - 1; currentColumn--)
                {
                    for (int row = 0; row < height; row++)
                    {
                        board[new Vector2Int(currentColumn + 1, row)].ChangeCellState(board[new Vector2Int(currentColumn, row)].CellState);
                    }
                }
                return;
            }
        }
    }

    private static void CheckFromTheBottom(Dictionary<Vector2Int, BoardCell> board)
    {
        // DOWN TO CENTER
        for (int row = 0; row <= height / 2; row++)
        {
            bool full = true;
            for (int column = 0; column < width && full; column++)
            {
                if (board[new Vector2Int(column, row)].CellState != CellState.Set)
                    full = false;
            }

            if (full)
            {
                for (int column = 0; column < width; column++)
                {
                    board[new Vector2Int(column, row)].ChangeCellState(CellState.Empty);
                }

                for (int currentRow = row + 1; currentRow <= height / 2; currentRow++)
                {
                    for (int column = 0; column < width; column++)
                    {
                        board[new Vector2Int(column, currentRow - 1)].ChangeCellState(board[new Vector2Int(column, currentRow)].CellState);
                    }
                }
                return;
            }
        }
    }

    private static void CheckFromTheLeft(Dictionary<Vector2Int, BoardCell> board)
    {
        // LEFT TO CENTER
        for (int column = 0; column <= width / 2; column++)
        {
            bool full = true;
            for (int row = 0; row < height && full; row++)
            {
                if (board[new Vector2Int(column, row)].CellState != CellState.Set)
                    full = false;
            }

            if (full)
            {
                for (int row = 0; row < height; row++)
                {
                    board[new Vector2Int(column, row)].ChangeCellState(CellState.Empty);
                }

                for (int currentColumn = column + 1; currentColumn <= width / 2; currentColumn++)
                {
                    for (int row = 0; row < height; row++)
                    {
                        board[new Vector2Int(currentColumn - 1, row)].ChangeCellState(board[new Vector2Int(currentColumn, row)].CellState);
                    }
                }
                return;
            }
        }
    }
}
