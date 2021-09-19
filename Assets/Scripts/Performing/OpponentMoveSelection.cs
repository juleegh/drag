using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentMoveSelection : MonoBehaviour, RequiredComponent
{
    private static OpponentMoveSelection instance;
    public static OpponentMoveSelection Instance { get { return instance; } }

    private bool willPlayMove;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.StartPerformance, PerformanceStarted);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.TurnAdvanced, CheckNextTempo);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.CurrentTempoStarted, TryMove);

    }

    private void PerformanceStarted()
    {
        willPlayMove = !DanceBattleManager.Instance.PlayerGoesFirst;
    }

    private void CheckNextTempo()
    {
        willPlayMove = !DanceBattleManager.Instance.IsPlayerTurn;
    }

    private void TryMove()
    {
        if (willPlayMove)
        {
            MoveType moveType = DecideNextMove();
            DanceMove selectedMove = ProgressManager.Instance.CurrentLevel.BossChoreo.GetResponseFromTempo(PerformSystem.Instance.CurrentMoveIndex, moveType);

            if (selectedMove == null)
                selectedMove = DanceMovesManager.Instance.GetRandomFromType(moveType);

            SongSequence.Instance.OpponentPlayMove(moveType, selectedMove);
        }
    }

    private MoveType DecideNextMove()
    {
        if (DanceBattleManager.Instance.Player.Multiplier > DanceBattleManager.Instance.Opponent.Multiplier)
            return MoveType.Attack;
        else if (DanceBattleManager.Instance.Opponent.Multiplier <= 1)
            return MoveType.Defense;
        else
            return PerformanceConversions.ConvertMoveTypeFromIndex(Random.Range(0, PerformanceConversions.MoveTypesQuantity));
    }
}
