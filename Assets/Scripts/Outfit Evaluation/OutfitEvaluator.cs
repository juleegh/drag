using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitEvaluator : MonoBehaviour, GlobalComponent
{
    private static OutfitEvaluator instance;
    public static OutfitEvaluator Instance { get { return instance; } }

    private LevelOutfitRules currentRules { get { return ProgressManager.Instance.CurrentLevel.OutfitRules; } }

    private float currentProgress;
    public float CurrentProgress { get { return currentProgress; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
    }

    public void CheckProgress()
    {
        List<Decoration> decorations = CharacterOutfitManager.Instance.GetDecorations();
        currentProgress = 0;

        foreach (OutfitRule rule in currentRules.Rules)
        {
            rule.Evaluate(decorations);
            currentProgress += rule.Progress * rule.ProgressionWeight;
        }

        if (currentProgress > 1f)
            currentProgress = 1f;

        OutfitEventsManager.Instance.Notify(OutfitEvent.OutfitProgressUpdated);
    }
}
