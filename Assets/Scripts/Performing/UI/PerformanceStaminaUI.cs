using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerformanceStaminaUI : MonoBehaviour, RequiredComponent
{
    [SerializeField] private Image staminaFill;
    [SerializeField] private GameObject container;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, ClearUI);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.StartPerformance, SetUI);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.BuffPassed, UpdateStaminaUsed);
    }

    private void ClearUI()
    {
        container.SetActive(false);
    }

    private void SetUI()
    {
        container.SetActive(true);
        staminaFill.fillAmount = 1;
    }

    private void UpdateStaminaUsed()
    {
        staminaFill.fillAmount = ((float)PerformingChoreoController.Instance.CharacterStamina / (float)ProgressManager.Instance.Stamina);
    }
}