using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightController : MonoBehaviour
{
    [SerializeField] private Light connectedLight;

    public void FadeIn(float time)
    {
        connectedLight.DOIntensity(1f, time);
    }

    public void FadeOut(float time)
    {
        connectedLight.DOIntensity(0f, time);
    }
}
