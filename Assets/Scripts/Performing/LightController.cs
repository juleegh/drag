using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light light;

    public void FadeIn(float time)
    {
        light.DOIntensity(5f, time);
    }

    public void FadeOut(float time)
    {
        light.DOIntensity(0f, time);
    }
}
