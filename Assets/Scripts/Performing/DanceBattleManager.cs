using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DanceBattleManager : MonoBehaviour, RequiredComponent
{
    private static DanceBattleManager instance;
    public static DanceBattleManager Instance { get { return instance; } }

    private PerformanceStatus player;
    private PerformanceStatus opponent;

    public PerformanceStatus Player { get { return player; } }
    public PerformanceStatus Opponent { get { return opponent; } }

    private bool isPlayerTurn;
    public bool IsPlayerTurn { get { return isPlayerTurn; } }

    private PerformanceStatus CurrentPlayer { get { return isPlayerTurn ? player : opponent; } }
    private PerformanceStatus OppositePlayer { get { return !isPlayerTurn ? player : opponent; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, CreateBattleInfo);
    }

    private void CreateBattleInfo()
    {
        player = new PerformanceStatus();
        opponent = new PerformanceStatus();
        isPlayerTurn = true;
        //isPlayerTurn = Random.Range(0, 9) % 2 == 0;
    }

    public void PlayerPerformedMove(DanceMove danceMove)
    {
        if (danceMove as ScoreMove != null)
        {
            ScoreMove buffMove = danceMove as ScoreMove;
            CurrentPlayer.IncreaseScore(danceMove.Score * CurrentPlayer.Multiplier);
            CurrentPlayer.ResetMultiplier();
        }

        if (danceMove as BuffMove != null)
        {
            BuffMove buffMove = danceMove as BuffMove;
            CurrentPlayer.ModifyMultiplier(buffMove.DefenseBuff);
            CurrentPlayer.IncreaseScore(danceMove.Score);
        }

        if (danceMove as SabotageMove != null)
        {
            SabotageMove buffMove = danceMove as SabotageMove;
            OppositePlayer.ModifyMultiplier(buffMove.AttackBuff);
            CurrentPlayer.IncreaseScore(danceMove.Score);
        }
    }

}