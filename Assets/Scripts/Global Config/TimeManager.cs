using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour, GlobalComponent
{
    private static TimeManager instance;
    public static TimeManager Instance { get { return instance; } }

    [SerializeField] private int startingHour;
    [SerializeField] private int finishingHour;

    private float currentHour;
    public float CurrentHour { get { return currentHour; } }

    public void ConfigureRequiredComponent()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        currentHour = startingHour;
    }

    void Start()
    {
        GameEventsManager.Instance.Notify(GameEvent.HourAdvanced);
    }

    public void AdvanceHour(float hours = 1)
    {
        currentHour += hours;

        if (currentHour >= finishingHour)
            currentHour = startingHour;

        GameEventsManager.Instance.Notify(GameEvent.HourAdvanced);
    }

}
