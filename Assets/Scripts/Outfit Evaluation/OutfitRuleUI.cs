using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OutfitRuleUI : MonoBehaviour
{
    [SerializeField] private Image fillBar;
    [SerializeField] private TextMeshProUGUI prompt;

    public void Refresh(OutfitRule rule)
    {
        prompt.text = rule.Prompt;
        fillBar.fillAmount = rule.Progress;
    }
}
