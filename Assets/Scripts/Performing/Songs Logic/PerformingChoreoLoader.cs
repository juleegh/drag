using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformingChoreoLoader : MonoBehaviour, RequiredComponent
{
    private Choreography choreography;
    public Choreography Choreography { get { return choreography; } }

    private static PerformingChoreoLoader instance;
    public static PerformingChoreoLoader Instance { get { return instance; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, LoadLevelChoreography);
    }

    private void LoadLevelChoreography()
    {
        choreography = new Choreography();
        choreography.LoadChoreo(ProgressManager.Instance.BossLevel);
    }
}