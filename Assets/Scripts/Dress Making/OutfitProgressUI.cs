using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutfitProgressUI : MonoBehaviour, RequiredComponent
{
    [SerializeField] private TextMeshProUGUI outfitPrompt;
    [SerializeField] private Image progressFillBar;

    public void ConfigureRequiredComponent()
    {
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, LoadPrompt);
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.OutfitProgressUpdated, OutfitProgressUpdated);

    }

    void LoadPrompt()
    {
        outfitPrompt.text = ProgressManager.Instance.CurrentLevel.OutfitRules.Prompt;
        OutfitProgressUpdated();
    }

    void OutfitProgressUpdated()
    {
        progressFillBar.fillAmount = OutfitEvaluator.Instance.CurrentProgress;
    }
}
