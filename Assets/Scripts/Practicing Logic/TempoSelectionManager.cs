using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoSelectionManager : MonoBehaviour, RequiredComponent
{
    [SerializeField] private ChoreographyPreview choreoPreview;

    private int topTempo;
    private int selectedTempo;

    private Song Song { get { return ProgressManager.Instance.CurrentLevel.BattleSong; } }
    private Choreography Choreography { get { return ChoreographyEditor.Instance.Choreography; } }

    public void ConfigureRequiredComponent()
    {
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.NavigatedNextTempo, ScrollNext);
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.NavigatedPreviousTempo, ScrollPrevious);
    }

    private void ScrollNext()
    {
        if (selectedTempo < choreoPreview.TemposOnScreen - 1)
        {
            selectedTempo++;
            choreoPreview.RefreshView(topTempo, selectedTempo);
        }
        else if (topTempo + choreoPreview.TemposOnScreen + 1 < Choreography.MovesPerTime.Count)
        {
            topTempo++;
            choreoPreview.RefreshView(topTempo, selectedTempo);
        }
    }

    private void ScrollPrevious()
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
}
