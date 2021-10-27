using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutfitProgressUI : MonoBehaviour, RequiredComponent
{
    [SerializeField] private GameObject evaluationContainer;
    [SerializeField] private TextMeshProUGUI outfitPrompt;
    [SerializeField] private Image progressFillBar;
    [SerializeField] private GameObject rulePrefab;
    [SerializeField] private Transform rulesContainer;
    [SerializeField] private Button toggleButton;
    [SerializeField] private TextMeshProUGUI togglePrompt;

    private List<OutfitRuleUI> rulesInPlay;
    private List<OutfitRule> Rules { get { return ProgressManager.Instance.CurrentLevel.OutfitRules.Rules; } }
    bool visible = false;

    public void ConfigureRequiredComponent()
    {
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, LoadPrompt);
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.OutfitProgressUpdated, OutfitProgressUpdated);
        toggleButton.onClick.AddListener(Toggle);

        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.OutfitStepChanged, CleanSection);
    }

    private void CleanSection()
    {
        evaluationContainer.SetActive(OutfitStepManager.Instance.CurrentOutfitStep == OutfitStep.Outfit);
        visible = true;
        Toggle();
    }

    void Toggle()
    {
        visible = !visible;
        rulesContainer.gameObject.SetActive(visible);
        togglePrompt.text = visible ? "V" : "^";
    }

    void LoadPrompt()
    {
        outfitPrompt.text = ProgressManager.Instance.CurrentLevel.OutfitRules.Prompt;
        rulesInPlay = new List<OutfitRuleUI>();
        foreach (OutfitRule rule in Rules)
        {
            OutfitRuleUI nextRule = Instantiate(rulePrefab).GetComponent<OutfitRuleUI>();
            nextRule.transform.SetParent(rulesContainer);
            rulesInPlay.Add(nextRule);
        }

        OutfitProgressUpdated();
        CleanSection();
    }

    void OutfitProgressUpdated()
    {
        progressFillBar.fillAmount = OutfitEvaluator.Instance.CurrentProgress;

        for (int i = 0; i < Rules.Count; i++)
        {
            rulesInPlay[i].Refresh(Rules[i]);
        }
    }
}
