using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CellTempoCounter : MonoBehaviour
{
    private bool isOn;
    private float blingTime;

    [SerializeField] private Color highlited;
    [SerializeField] private Color obscured;
    [SerializeField] private SpriteRenderer image;

    public void SetupStart(bool start)
    {
        isOn = start;
    }

    public void SetupBlingTime(float time)
    {
        blingTime = time;
    }

    public void Toggle()
    {
        if (isOn)
            TurnOff();
        else
            Twinkle();
    }

    private void Twinkle()
    {
        image.DOColor(highlited, blingTime);
        isOn = true;
    }

    private void TurnOff()
    {
        image.DOColor(obscured, blingTime);
        isOn = false;
    }

}
