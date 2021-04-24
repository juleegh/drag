using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoCounter : MonoBehaviour
{
    [SerializeField] private float acceptablePercentage;

    private float frequency;
    private bool isPlaying;
    private bool beatFrame;

    public bool IsOnTempo { get { return beatFrame; } }
    public float TempoPercentage { get { return tempoTime / frequency; } }
    private float tempoTime;

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
        WaitForSeconds unacceptable = new WaitForSeconds(frequency * (1 - acceptablePercentage));
        WaitForSeconds acceptable = new WaitForSeconds(frequency * acceptablePercentage);
        while (isPlaying)
        {
            yield return unacceptable;
            beatFrame = true;
            yield return acceptable;
            beatFrame = false;
            tempoTime = 0f;
            PerformSystem.Instance.NextMove();
        }
    }
}
