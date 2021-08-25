using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceMovesManager : MonoBehaviour, GlobalComponent
{
    private static DanceMovesManager instance;
    public static DanceMovesManager Instance { get { return instance; } }

    [SerializeField] private List<DanceMove> danceMoves;

    private Dictionary<string, DanceMove> danceMovesList;
    public Dictionary<string, DanceMove> DanceMovesList { get { return danceMovesList; } }

    public void ConfigureRequiredComponent()
    {
        if (instance == null)
        {
            instance = this;
            LoadDanceMoves();
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void LoadDanceMoves()
    {
        danceMovesList = new Dictionary<string, DanceMove>();
        foreach (DanceMove danceMove in danceMoves)
        {
            danceMovesList.Add(danceMove.Identifier, danceMove);
        }
    }
}