using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesInputManager : MonoBehaviour, RequiredComponent
{
    public static MovesInputManager Instance { get { return instance; } }
    private static MovesInputManager instance;

    [SerializeField] private KeyCode a;
    [SerializeField] private KeyCode b;
    [SerializeField] private KeyCode x;

    public KeyCode A { get { return a; } }
    public KeyCode B { get { return b; } }
    public KeyCode X { get { return x; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
    }

    public KeyCode GetKeyFromMoveType(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.Score:
                return a;
            case MoveType.Defense:
                return b;
            case MoveType.Attack:
                return x;
        }

        return KeyCode.None;
    }

    public string GetNameFromMoveType(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.Score:
                return "A";
            case MoveType.Defense:
                return "B";
            case MoveType.Attack:
                return "X";
        }

        return " ";
    }
}
