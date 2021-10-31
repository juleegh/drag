using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class OutfitRuleUI : MonoBehaviour
{
    [SerializeField] private Image fillBar;
    [SerializeField] private TextMeshProUGUI prompt;
    private static float animTime = 0.65f;
    public static float AnimTime { get { return animTime; } }

    public void Clean()
    {
        fillBar.fillAmount = 0;
    }

    public void Refresh(OutfitRule rule)
    {
        prompt.text = rule.Prompt;
        fillBar.DOFillAmount(rule.Progress, animTime);
    }
}
