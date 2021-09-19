using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FileDataReader
{
    public static List<string[]> LoadGrid(TextAsset csvFile)
    {
        List<string[]> grid = getCSVGrid(csvFile.text);
        return grid;
        //DisplayGrid(grid);
    }

    static private List<string[]> getCSVGrid(string csvText)
    {
        //split the data on split line character
        string[] lines = csvText.Split("\n"[0]);

        // find the max number of columns
        int totalColumns = 0;
        for (int i = 0; i < lines.Length; i++)
        {
            string[] row = lines[i].Split(',');
            totalColumns = Mathf.Max(totalColumns, row.Length);
        }

        // creates new 2D string grid to output to
        List<string[]> outputGrid = new List<string[]>();
        for (int y = 1; y < lines.Length; y++)
        {
            string[] row = lines[y].Split(',');
            outputGrid.Add(row);
        }

        return outputGrid;
    }

    static private void DisplayGrid(string[,] grid)
    {
        string textOutput = "";
        for (int y = 0; y < grid.GetUpperBound(1); y++)
        {
            for (int x = 0; x < grid.GetUpperBound(0); x++)
            {

                textOutput += grid[x, y];
                textOutput += ",";
            }
            textOutput += "\n";
        }
        Debug.Log(textOutput);
    }
}
