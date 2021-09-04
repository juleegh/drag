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
    [SerializeField] private KeyCode y;

    public KeyCode A { get { return a; } }
    public KeyCode B { get { return b; } }
    public KeyCode X { get { return x; } }
    public KeyCode Y { get { return y; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
    }

    public KeyCode GetKeyFromMoveType(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.AType:
                return a;
            case MoveType.BType:
                return b;
            case MoveType.XType:
                return x;
            case MoveType.YType:
                return y;
        }

        return KeyCode.None;
    }

    public string GetNameFromMoveType(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.AType:
                return "A";
            case MoveType.BType:
                return "B";
            case MoveType.XType:
                return "X";
            case MoveType.YType:
                return "Y";
        }

        return " ";
    }
}
