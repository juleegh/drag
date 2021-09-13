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
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.BuffPassed, TryMove);

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
            SongSequence.Instance.OpponentPlayRandomMove();
    }
}
