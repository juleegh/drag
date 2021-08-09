using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour, GlobalComponent
{
    private static MoneyManager instance;
    public static MoneyManager Instance { get { return instance; } }

    private float dollars;
    public float Dollars { get { return dollars; } }

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
    }

    void Start()
    {
        GameEventsManager.Instance.Notify(GameEvent.MoneyChanged);
    }

    public void GainDollars(float quantity)
    {
        dollars += quantity;
        GameEventsManager.Instance.Notify(GameEvent.MoneyChanged);
    }
}
