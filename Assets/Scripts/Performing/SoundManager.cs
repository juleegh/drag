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

    public void StopTrack()
    {
        if (imUpset)
            return;
        audioSource.Pause();
    }

}
