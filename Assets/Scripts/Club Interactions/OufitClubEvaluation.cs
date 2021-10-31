using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class OufitClubEvaluation : MonoBehaviour, RequiredComponent
{
    [SerializeField] private GameObject evaluationContainer;
    [SerializeField] private TextMeshProUGUI clubTitle;
    [SerializeField] private TextMeshProUGUI outfitPrompt;
    [SerializeField] private Image progressFillBar;
    [SerializeField] private GameObject rulePrefab;
    [SerializeField] private GameObject continuePrompt;
    [SerializeField] private Transform rulesContainer;

    private List<OutfitRuleUI> rulesInPlay;
    private List<OutfitRule> Rules { get { return ProgressManager.Instance.CurrentLevel.OutfitRules.Rules; } }
    bool showingEvaluation = false;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, LoadPrompt);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.OutfitEvaluationStarted, ShowEvaluation);
    }

    private void Update()
    {
        if (showingEvaluation || !evaluationContainer.activeInHierarchy)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
            HideEvaluation();
    }

    void LoadPrompt()
    {
        outfitPrompt.text = ProgressManager.Instance.CurrentLevel.OutfitRules.Prompt;
        clubTitle.text = ProgressManager.Instance.CurrentLevel.ClubName;

        rulesInPlay = new List<OutfitRuleUI>();
        foreach (OutfitRule rule in Rules)
        {
            OutfitRuleUI nextRule = Instantiate(rulePrefab).GetComponent<OutfitRuleUI>();
            nextRule.transform.SetParent(rulesContainer);
            rulesInPlay.Add(nextRule);
            nextRule.Refresh(rule);
        }
        evaluationContainer.SetActive(false);
    }

    void ShowEvaluation()
    {
        DialogSystemController.Instance.EndDialog();

        continuePrompt.SetActive(false);
        evaluationContainer.SetActive(true);
        showingEvaluation = true;
        progressFillBar.fillAmount = 0;

        foreach (OutfitRuleUI ruleUI in rulesInPlay)
            ruleUI.Clean();

        StartCoroutine(FillBars());
    }

    private IEnumerator FillBars()
    {
        yield return new WaitForSeconds(2.5f);

        float pause = 0.5f;
        float totalTime = (OutfitRuleUI.AnimTime + pause) * rulesInPlay.Count;

        progressFillBar.DOFillAmount(OutfitEvaluator.Instance.CurrentProgress, totalTime);

        for (int i = 0; i < Rules.Count; i++)
        {
            rulesInPlay[i].Refresh(Rules[i]);
            yield return new WaitForSeconds(OutfitRuleUI.AnimTime + pause);
        }

        PerformingEventsManager.Instance.Notify(PerformingEvent.OutfitEvaluated);
        yield return new WaitForSeconds(1.25f);
        showingEvaluation = false;
        continuePrompt.SetActive(true);
    }

    void HideEvaluation()
    {
        evaluationContainer.SetActive(false);
        PerformingEventsManager.Instance.Notify(PerformingEvent.OutfitEvaluationEnded);
    }
}
