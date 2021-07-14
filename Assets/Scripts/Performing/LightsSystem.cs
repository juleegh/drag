using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsSystem : MonoBehaviour, IRequiredComponent
{
    private static LightsSystem instance;
    public static LightsSystem Instance { get { return instance; } }

    [SerializeField] private LightController[] tempoLights;
    [SerializeField] private LightController characterSpotLight;
    private int currentTempoLight;
    private Transform faceBone;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, SetupFaceBone);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.TempoEnded, MarkTempo);
    }

    private void SetupFaceBone()
    {
        if (GlobalPlayerManager.Instance != null)
            faceBone = GlobalPlayerManager.Instance.FaceBone;
    }

    private void Update()
    {
        characterSpotLight.transform.LookAt(faceBone);
    }

    private void MarkTempo()
    {
        float fadeTime = 0.1f;
        tempoLights[currentTempoLight].FadeOut(fadeTime);
        currentTempoLight++;
        if (currentTempoLight == tempoLights.Length)
            currentTempoLight = 0;
        tempoLights[currentTempoLight].FadeIn(fadeTime);
    }
}
