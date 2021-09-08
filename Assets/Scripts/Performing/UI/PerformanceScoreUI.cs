using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PerformanceScoreUI : MonoBehaviour, RequiredComponent
{
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
    }

    private void ShowScore()
    {
        container.SetActive(true);
        UpdateScore();
    }

    private void UpdateScore()
    {
        PerformanceStatus status = isPlayers ? DanceBattleManager.Instance.Player : DanceBattleManager.Instance.Opponent;
        scoreText.text = "Score: " + status.PerformingScore;
        multiplier.text = "x" + status.Multiplier;
    }
}
