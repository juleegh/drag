using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Button exitButton;
    [SerializeField] private GameObject prompt;

    void Awake()
    {
        button.onClick.AddListener(Worked);
        exitButton.onClick.AddListener(LeaveWork);
        prompt.SetActive(false);
    }

    void Worked()
    {
        TimeManager.Instance.AdvanceHour();
        MoneyManager.Instance.GainDollars(35);
        CheckSchedule();
    }

    void LeaveWork()
    {
        GlobalPlayerManager.Instance.GoToLobby();
    }

    void CheckSchedule()
    {
        if (TimeManager.Instance.CurrentHour >= 17)
        {
            prompt.SetActive(true);
        }
    }
}
