using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MoveDelayScreen : MonoBehaviour, RequiredComponent
{
    private Image screen;

    public void ConfigureRequiredComponent()
    {
        screen = GetComponent<Image>();
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.CurrentTempoStarted, FadeIn);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.TempoEnded, FadeOut);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.MovePerformed, FadeOut);
    }

    private void FadeIn()
    {
        screen.DOFade(0.65f, 0.3f);
    }

    private void FadeOut()
    {
        screen.DOFade(0f, 0.3f);
    }
}
