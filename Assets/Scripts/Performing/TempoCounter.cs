using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoCounter : MonoBehaviour, IRequiredComponent
{
    public static TempoCounter Instance { get { return instance; } }
    private static TempoCounter instance;

    private float frequency;
    private bool isPlaying;
    private bool preBeatFrame;
    private bool postBeatFrame;

    public bool IsOnPreTempo { get { return preBeatFrame; } }
    public bool IsOnPostTempo { get { return postBeatFrame; } }
    public float TempoPercentage { get { return isPlaying ? tempoTime / frequency : 0f; } }
    private float tempoTime;

    public void ConfigureRequiredComponent()
    {
        instance = this;
    }

    public void SetTempo(float freq)
    {
        frequency = freq;
    }

    public void StartTempoCount()
    {
        tempoTime = 0f;
        isPlaying = true;
        StartCoroutine(QualifyTempo());
    }

    public void StopTempoCount()
    {
        isPlaying = false;
        StopAllCoroutines();
    }

    private void Update()
    {
        if (isPlaying)
            tempoTime += Time.deltaTime;
    }

    private IEnumerator QualifyTempo()
    {
        WaitForSeconds unaceptable = new WaitForSeconds(frequency * (1 - PerformSystem.Instance.MovesProperties.AcceptablePercentage));
        WaitForSeconds preAcceptable = new WaitForSeconds(frequency * PerformSystem.Instance.MovesProperties.AcceptablePercentage * 0.65f);
        WaitForSeconds postAcceptable = new WaitForSeconds(frequency * PerformSystem.Instance.MovesProperties.AcceptablePercentage * 0.35f);

        bool firstTime = true;
        while (isPlaying)
        {
            if (firstTime)
            {
                yield return postAcceptable;
                firstTime = false;
            }
            yield return unaceptable;
            preBeatFrame = true;
            yield return preAcceptable;
            PerformingEventsManager.Instance.Notify(PerformingEvent.TempoEnded);
            tempoTime = 0f;
            preBeatFrame = false;
            postBeatFrame = true;
            yield return postAcceptable;
            postBeatFrame = false;
        }
    }
}
