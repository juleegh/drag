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
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.ShiftedTempo, SetOptions);
    }

    private void ClearUI()
    {
        movePreviews[0].SetMoveInfo(MoveType.AType);
        movePreviews[1].SetMoveInfo(MoveType.BType);
        movePreviews[2].SetMoveInfo(MoveType.XType);
        movePreviews[3].SetMoveInfo(MoveType.YType);
        container.SetActive(false);
    }

    private void SetOptions()
    {
        container.SetActive(true);
        DanceMove[] currentOptions = PerformingChoreoController.Instance.GetOptionsForTempo();
        int i = 0;
        foreach (DanceMove move in currentOptions)
        {
            if (move == null)
                movePreviews[i].UpdateMoveText("", 0);
            else
                movePreviews[i].UpdateMoveText(move.Identifier, move.StaminaRequired);

            i++;
        }
    }
}