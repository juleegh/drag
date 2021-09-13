using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class StaminaCalculator : MonoBehaviour, RequiredComponent
{
    [SerializeField] private TextMeshProUGUI averageStamina;
    [SerializeField] private TextMeshProUGUI characterStamina;
    [SerializeField] private Image background;

    public void ConfigureRequiredComponent()
    {
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.ChoreographyLoaded, FillStaminaInfo);
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.ChoreographyUpdated, FillStaminaInfo);
        PracticeEventsManager.Instance.AddActionToEvent(PracticeEvents.NotEnoughStamina, NotEnoughStamina);
    }

    public void NotEnoughStamina()
    {
        background.color = Color.red;
        background.DOColor(Color.white, 0.35f);
    }

    private void FillStaminaInfo()
    {
        int stamina = ChoreographyEditor.Instance.Choreography.GetTotalStamina();
        averageStamina.text = "Used Stamina: " + stamina;
        characterStamina.text = "Character SP: " + ProgressManager.Instance.Stamina;
    }
}
