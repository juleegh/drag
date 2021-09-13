using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TempoOptionsPreview : MonoBehaviour, RequiredComponent
{
    [SerializeField] MovePreview[] movePreviews;
    [SerializeField] GameObject container;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, ClearUI);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.StartPerformance, SetOptions);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.TurnAdvanced, SetOptions);
    }

    private void ClearUI()
    {
        movePreviews[0].SetMoveInfo(MoveType.Score);
        movePreviews[1].SetMoveInfo(MoveType.Defense);
        movePreviews[2].SetMoveInfo(MoveType.Attack);
        //movePreviews[3].SetMoveInfo(MoveType.YType);
        container.SetActive(false);
    }

    private void SetOptions()
    {
        container.SetActive(DanceBattleManager.Instance.IsPlayerTurn);

        if (!DanceBattleManager.Instance.IsPlayerTurn)
            return;

        DanceMove[] currentOptions = PerformingChoreoController.Instance.GetOptionsForTempo();
        int i = 0;
        foreach (DanceMove move in currentOptions)
        {
            if (move == null)
                movePreviews[i].UpdateMoveText("", 0);
            else
                movePreviews[i].UpdateMoveText(move.Identifier, 0);

            i++;
        }
    }
}