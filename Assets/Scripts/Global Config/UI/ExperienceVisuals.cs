using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceVisuals : MonoBehaviour, GlobalComponent
{
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private GameObject experienceContainer;

    public void ConfigureRequiredComponent()
    {
        GameEventsManager.Instance.AddActionToEvent(GameEvent.ExperienceGained, RefreshXP);
        GameEventsManager.Instance.AddActionToEvent(GameEvent.ShowExperience, ShowXP);
        GameEventsManager.Instance.AddActionToEvent(GameEvent.HideExperience, HideXP);
    }

    private void RefreshXP()
    {
        experienceText.text = ProgressManager.Instance.ExperiencePoints.ToString();
    }

    private void ShowXP()
    {
        experienceContainer.SetActive(true);
    }

    private void HideXP()
    {
        experienceContainer.SetActive(false);
    }
}
