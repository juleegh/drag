using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoSelectionManager : MonoBehaviour, RequiredComponent
{
    enum Selection
    {
        None,
        Tempo,
        Move,
    }

    [SerializeField] private ChoreographyPreview choreoPreview;
    [SerializeField] private MoveTypeListPreview moveTypeListPreview;

    private int topTempo;
    private int selectedTempo;
    private int selectedMovement;
    private int topMovementType;
    private int selectedMovementType;
    private Selection currentlySelected;


    private Song Song { get { return ProgressManager.Instance.CurrentLevel.BattleSong; } }
    private Choreography Choreography { get { return ChoreographyEditor.Instance.Choreography; } }
    private MoveType MoveTypeSelected { get { return PerformanceConversions.ConvertMoveTypeFromIndex(selectedMovement); } }
    private DanceMove DanceMoveSelected { get { return DanceMovesManager.Instance.GetListFromType(MoveTypeSelected)[topMovementType + selectedMovementType]; } }

    public void ConfigureRequiredComponent()
    {
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.NavigatedNextTempo, ScrollNext);
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.NavigatedPreviousTempo, ScrollPrevious);
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.NavigatedForwardTempo, NavigateForward);
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.NavigatedBackTempo, NavigateBack);
        currentlySelected = Selection.None;
    }

    private void ScrollNext()
    {
        if (currentlySelected == Selection.None)
        {
            if (selectedTempo < choreoPreview.TemposOnScreen - 1)
            {
                selectedTempo++;
                choreoPreview.RefreshView(topTempo, selectedTempo);
            }
            else if (topTempo + choreoPreview.TemposOnScreen + 1 <= Choreography.MovesPerTime.Count)
            {
                topTempo++;
                choreoPreview.RefreshView(topTempo, selectedTempo);
            }
        }
        else if (currentlySelected == Selection.Tempo)
        {
            if (selectedMovement < 2)
            {
                selectedMovement++;
                choreoPreview.RefreshTempoView(selectedTempo, selectedMovement);
            }
        }
        else if (currentlySelected == Selection.Move)
        {
            if (selectedMovementType < moveTypeListPreview.MovesOnScreen - 1 && selectedMovementType < DanceMovesManager.Instance.GetListFromType(MoveTypeSelected).Count - 1)
            {
                selectedMovementType++;
                moveTypeListPreview.RefreshView(topMovementType, selectedMovementType, MoveTypeSelected);
                ChoreographyEditor.Instance.PreviewMove(DanceMoveSelected);
            }
            else if (topMovementType + moveTypeListPreview.MovesOnScreen + 1 <= DanceMovesManager.Instance.GetListFromType(MoveTypeSelected).Count)
            {
                topMovementType++;
                moveTypeListPreview.RefreshView(topMovementType, selectedMovementType, MoveTypeSelected);
                ChoreographyEditor.Instance.PreviewMove(DanceMoveSelected);
            }
        }
    }

    private void ScrollPrevious()
    {
        if (currentlySelected == Selection.None)
        {
            if (selectedTempo > 0)
            {
                selectedTempo--;
                choreoPreview.RefreshView(topTempo, selectedTempo);
            }
            else if (topTempo > 0)
            {
                topTempo--;
                choreoPreview.RefreshView(topTempo, selectedTempo);
            }
        }
        else if (currentlySelected == Selection.Tempo)
        {
            if (selectedMovement > 0)
            {
                selectedMovement--;
                choreoPreview.RefreshTempoView(selectedTempo, selectedMovement);
            }
        }
        else if (currentlySelected == Selection.Move)
        {
            if (selectedMovementType > 0)
            {
                selectedMovementType--;
                moveTypeListPreview.RefreshView(topMovementType, selectedMovementType, MoveTypeSelected);
                ChoreographyEditor.Instance.PreviewMove(DanceMoveSelected);
            }
            else if (topMovementType > 0)
            {
                topMovementType--;
                moveTypeListPreview.RefreshView(topMovementType, selectedMovementType, MoveTypeSelected);
                ChoreographyEditor.Instance.PreviewMove(DanceMoveSelected);
            }
        }
    }

    private void NavigateForward()
    {
        switch (currentlySelected)
        {
            case Selection.None:
                currentlySelected = Selection.Tempo;
                selectedMovement = 0;
                choreoPreview.RefreshTempoView(selectedTempo, selectedMovement);
                return;
            case Selection.Tempo:
                currentlySelected = Selection.Move;
                moveTypeListPreview.ShowList(true);
                ChoreographyEditor.Instance.PreviewMove(DanceMoveSelected);
                moveTypeListPreview.RefreshView(topMovementType, selectedMovementType, MoveTypeSelected);
                return;
            case Selection.Move:
                ChoreographyEditor.Instance.SaveMoveToTempo(topTempo + selectedTempo, selectedMovement, DanceMoveSelected);
                choreoPreview.RefreshView(topTempo, selectedTempo);
                choreoPreview.RefreshTempoView(selectedTempo, selectedMovement);
                currentlySelected = Selection.Tempo;
                topMovementType = 0;
                selectedMovementType = 0;
                moveTypeListPreview.ShowList(false);
                return;
        }
    }

    private void NavigateBack()
    {
        switch (currentlySelected)
        {
            case Selection.Tempo:
                currentlySelected = Selection.None;
                choreoPreview.RefreshTempoView(selectedTempo, -1);
                return;
            case Selection.Move:
                topMovementType = 0;
                selectedMovementType = 0;
                currentlySelected = Selection.Tempo;
                moveTypeListPreview.ShowList(false);
                ChoreographyEditor.Instance.CancelPreview();
                return;
        }
    }
}
