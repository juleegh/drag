using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayHourVisuals : MonoBehaviour, GlobalComponent
{
    [SerializeField] private TextMeshProUGUI hourText;
    [SerializeField] private GameObject clockContainer;

    public void ConfigureRequiredComponent()
    {
        GameEventsManager.Instance.AddActionToEvent(GameEvent.HourAdvanced, RefreshClock);
        GameEventsManager.Instance.AddActionToEvent(GameEvent.ShowHour, ShowClock);
        GameEventsManager.Instance.AddActionToEvent(GameEvent.HideHour, HideClock);
    }

    private void RefreshClock()
    {
        float hours = TimeManager.Instance.CurrentHour;

        float floored = Mathf.Floor(hours);
        float fractionalPart = hours - floored;
        fractionalPart *= 60;

        string hoursText = floored >= 10 ? floored.ToString() : "0" + floored;
        string minutesText = fractionalPart >= 10 ? fractionalPart.ToString("F0") : "0" + fractionalPart.ToString("F0");
        hourText.text = hoursText + ":" + minutesText;
    }

    private void ShowClock()
    {
        clockContainer.SetActive(true);
    }

    private void HideClock()
    {
        clockContainer.SetActive(false);
    }
}
