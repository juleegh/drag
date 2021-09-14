using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour, RequiredComponent
{
    public static SoundManager Instance { get { return instance; } }
    private static SoundManager instance;

    [SerializeField] private AudioSource audioSource;
    private bool imUpset = false;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.CurrentTempoStarted, StopTrack);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.TempoEnded, ContinueTrack);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.MovePerformed, ContinueTrack);
    }

    public void SetTrack(AudioClip clip)
    {
        audioSource.clip = clip;
    }

    public void StartTrack()
    {
        if (imUpset)
            return;
        audioSource.Play();
    }

    private void ContinueTrack()
    {
        audioSource.UnPause();
    }

    public void StopTrack()
    {
        if (imUpset)
            return;
        audioSource.Pause();
    }

}
