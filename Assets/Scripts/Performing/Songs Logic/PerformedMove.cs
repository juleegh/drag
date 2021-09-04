using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformedMove
{
    public DanceMove selectedMove;
    public MoveType moveType;
    public float score;
    public bool performed;

    public void AssignSelectedMove(DanceMove selected)
    {
        if (selected != null)
            selectedMove = selected;
        else
        {
            selectedMove = (DanceMove)ScriptableObject.CreateInstance("DanceMove");
            selectedMove.MakeEmpty();
        }

    }
}
