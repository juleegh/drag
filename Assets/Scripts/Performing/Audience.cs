using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audience : ScriptableObject
{
    [SerializeField] private float moveAReaction;
    [SerializeField] private float moveBReaction;
    [SerializeField] private float moveXReaction;
    [SerializeField] private float moveYReaction;

    public float GetReaction(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.AType:
                return moveAReaction;
            case MoveType.BType:
                return moveBReaction;
            case MoveType.XType:
                return moveXReaction;
            case MoveType.YType:
                return moveYReaction;
        }

        return 0f;
    }
}
