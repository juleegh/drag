using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class WinnerScreen : MonoBehaviour, RequiredComponent
{
    [SerializeField] private TextMeshProUGUI winnerTitle;
    [SerializeField] private GameObject continuePrompt;
    [SerializeField] private GameObject container;

    bool playerCanSkip;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, HideScreen);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.PerformanceEnded, ShowScreen);
    }

    private void HideScreen()
    {
        playerCanSkip = false;
        container.SetActive(false);
        continuePrompt.SetActive(false);
    }

    private void ShowScreen()
    {
        container.SetActive(true);
        string winnerName = DanceBattleManager.Instance.PlayerWins ? GlobalPlayerManager.Instance.QueenName : ProgressManager.Instance.CurrentLevel.BossName;
        GameEventsManager.Instance.Notify(DanceBattleManager.Instance.PlayerWins ? GameEvent.PlayerWon : GameEvent.PlayerLose);
        winnerTitle.text = winnerName + " WINS";
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(3f);
        sequence.OnComplete(ShowPrompt);
        sequence.Play();
    }

    private void ShowPrompt()
    {
        continuePrompt.SetActive(true);
        playerCanSkip = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerCanSkip)
        {
            HideScreen();
            PerformingEventsManager.Instance.Notify(PerformingEvent.LeftDanceFloor);
            SoundManager.Instance.StopTrack();
        }
    }
}
