using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StaminaCalculator : MonoBehaviour, RequiredComponent
{
    [SerializeField] private TextMeshProUGUI averageStamina;
    [SerializeField] private TextMeshProUGUI characterStamina;

    public void ConfigureRequiredComponent()
    {
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.ChoreographyLoaded, FillStaminaInfo);
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.ChoreographyUpdated, FillStaminaInfo);
    }

    private void FillStaminaInfo()
    {
        int stamina = ChoreographyEditor.Instance.Choreography.GetAverageStamina();
        averageStamina.text = "Average SP required: " + stamina;
        characterStamina.text = "Character SP: " + ProgressManager.Instance.Stamina;
    }
}
