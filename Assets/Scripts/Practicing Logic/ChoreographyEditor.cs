using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoreographyEditor : MonoBehaviour, RequiredComponent
{
    private static ChoreographyEditor instance;
    public static ChoreographyEditor Instance { get { return instance; } }

    private Choreography choreography;
    public Choreography Choreography { get { return choreography; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.DependenciesLoaded, LoadLevelChoreography);
    }

    private void LoadLevelChoreography()
    {
        choreography = new Choreography();
        choreography.LoadChoreo(ProgressManager.Instance.BossLevel);
        PracticeEventsManager.Instance.Notify(PracticeEvents.ChoreographyLoaded);
    }
}
