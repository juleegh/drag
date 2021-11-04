using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkingInput : MonoBehaviour
{
    private void Update()
    {
        int deltaX = 0;
        int deltaY = 0;

        if (Input.GetKey(KeyCode.LeftArrow))
            deltaX = -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            deltaX = 1;
        else if (Input.GetKey(KeyCode.DownArrow))
            deltaY = -1;
        else if (Input.GetKey(KeyCode.UpArrow))
            deltaY = 1;
        else if (Input.GetKeyDown(KeyCode.Space))
            WorkBoard.Instance.SpawnNextPiece();
        else if (Input.GetKeyDown(KeyCode.R))
            WorkBoard.Instance.RotateCurrentPiece();

        if (deltaX != 0 || deltaY != 0)
        {
            WorkBoard.Instance.TryToMove(deltaX, deltaY);
        }
    }
}
