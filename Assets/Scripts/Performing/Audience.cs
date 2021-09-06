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
            case MoveType.Score:
                return moveAReaction;
            case MoveType.Defense:
                return moveBReaction;
            case MoveType.Attack:
                return moveXReaction;
        }

        return 0f;
    }
}
