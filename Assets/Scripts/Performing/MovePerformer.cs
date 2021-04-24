using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePerformer : MonoBehaviour
{
    void Update()
    {
        if (PerformSystem.Instance.PerformState != PerformState.Executing)
            return;

        if (Input.GetKeyDown(MovesInputManager.Instance.A))
            PerformMove(MoveType.AType);
        else if (Input.GetKeyDown(MovesInputManager.Instance.B))
            PerformMove(MoveType.BType);
        else if (Input.GetKeyDown(MovesInputManager.Instance.X))
            PerformMove(MoveType.XType);
        else if (Input.GetKeyDown(MovesInputManager.Instance.Y))
            PerformMove(MoveType.YType);

    }

    private void PerformMove(MoveType moveType)
    {
        Move move = new Move();
        move.moveType = moveType;
        move.score = 50;
        PerformSystem.Instance.PerformedMove(move);
    }
}
