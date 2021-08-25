using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperienceVisuals : MonoBehaviour, GlobalComponent
{
    [SerializeField] private TextMeshProUGUI experienceText;

    public void ConfigureRequiredComponent()
    {
        GameEventsManager.Instance.AddActionToEvent(GameEvent.ExperienceGained, RefreshXP);
    }

    private void RefreshXP()
    {
        experienceText.text = ProgressManager.Instance.ExperiencePoints.ToString();
    }
}
