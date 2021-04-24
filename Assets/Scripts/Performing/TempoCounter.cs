using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoCounter : MonoBehaviour, IRequiredComponent
{
    public static TempoCounter Instance { get { return instance; } }
    private static TempoCounter instance;

    private float frequency;
    private bool isPlaying;
    private bool beatFrame;

    public bool IsOnTempo { get { return beatFrame; } }
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
        beatFrame = false;
        WaitForSeconds unacceptable = new WaitForSeconds(frequency * (1 - PerformSystem.Instance.MovesProperties.AcceptablePercentage));
        WaitForSeconds acceptable = new WaitForSeconds(frequency * PerformSystem.Instance.MovesProperties.AcceptablePercentage);
        while (isPlaying)
        {
            yield return unacceptable;
            beatFrame = true;
            yield return acceptable;
            beatFrame = false;
            tempoTime = 0f;
            PerformingEventsManager.Instance.Notify(PerformingEvent.TempoEnded);
        }
    }
}
