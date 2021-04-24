using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TempoIndicator : MonoBehaviour
{
    [SerializeField] private Image indicator;
    [SerializeField] private float delayHit;

    public float DelayHit { get { return delayHit; } }
    private Color color;
    private Color original;
    float currentAlpha = 0f;

    void Awake()
    {
        color = indicator.color;
        original = indicator.color;
        SetAlphas();
    }

    public void MarkTempo()
    {
        StopCoroutine(AnimMarkTempo());
        StartCoroutine(AnimMarkTempo());
    }

    public void MarkCorrect()
    {
        color = Color.green;
        SetAlphas();
    }

    public void MarkWrong()
    {
        color = Color.red;
        SetAlphas();
    }

    private void SetAlphas()
    {
        original.a = currentAlpha;
        color.a = currentAlpha;
    }

    private IEnumerator AnimMarkTempo()
    {
        float numberOfSteps = 10;
        float step = 1 / numberOfSteps;
        WaitForSeconds delay = new WaitForSeconds(delayHit / numberOfSteps);
        for (currentAlpha = step; currentAlpha <= 1; currentAlpha += step)
        {
            color.a = currentAlpha;
            indicator.color = color;
            yield return delay;
        }

        for (currentAlpha = 1 - step; currentAlpha >= 0; currentAlpha -= step)
        {
            color.a = currentAlpha;
            indicator.color = color;
            yield return delay;
        }
        color = original;
        SetAlphas();
    }
}
