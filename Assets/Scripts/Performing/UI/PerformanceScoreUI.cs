using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class PerformanceScoreUI : MonoBehaviour, RequiredComponent
{
    [SerializeField] private Image scoreBar;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI multiplier;
    [SerializeField] private GameObject container;
    [SerializeField] private bool isPlayers;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, CleanScore);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.StartPerformance, ShowScore);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.MovePerformed, UpdateScore);
    }

    private void CleanScore()
    {
        container.SetActive(false);
        scoreBar.gameObject.SetActive(false);
    }

    private void ShowScore()
    {
        container.SetActive(true);
        scoreBar.gameObject.SetActive(true);
        UpdateScore();
    }

    private void UpdateScore()
    {
        PerformanceStatus status = isPlayers ? DanceBattleManager.Instance.Player : DanceBattleManager.Instance.Opponent;
        float totalScore = DanceBattleManager.Instance.Player.PerformingScore + DanceBattleManager.Instance.Opponent.PerformingScore;
        float scoreAmount = totalScore > 0 ? (float)status.PerformingScore / totalScore : 0.5f;
        scoreBar.DOFillAmount(scoreAmount, 0.3f).SetEase(Ease.InOutQuad);
        scoreText.text = "Score: " + status.PerformingScore;
        multiplier.text = "x" + status.Multiplier;
    }
}
