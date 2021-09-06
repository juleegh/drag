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

    private List<DanceMove> scoreMoves;
    private List<DanceMove> attackMoves;
    private List<DanceMove> defenseMoves;

    public List<DanceMove> ScoreMoves { get { return scoreMoves; } }
    public List<DanceMove> AttackMoves { get { return attackMoves; } }
    public List<DanceMove> DefenseMoves { get { return defenseMoves; } }

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

        scoreMoves = new List<DanceMove>();
        attackMoves = new List<DanceMove>();
        defenseMoves = new List<DanceMove>();

        foreach (DanceMove danceMove in danceMoves)
        {
            danceMovesList.Add(danceMove.Identifier, danceMove);

            if (danceMove as AttackMove != null)
                attackMoves.Add(danceMove);
            if (danceMove as DefenseMove != null)
                defenseMoves.Add(danceMove);
            if (danceMove as ScoreMove != null)
                scoreMoves.Add(danceMove);
        }
    }

    public List<DanceMove> GetListFromType(MoveType moveType)
    {
        switch (moveType)
        {
            case MoveType.Score:
                return scoreMoves;
            case MoveType.Defense:
                return defenseMoves;
            case MoveType.Attack:
                return attackMoves;
        }

        return new List<DanceMove>();
    }
}