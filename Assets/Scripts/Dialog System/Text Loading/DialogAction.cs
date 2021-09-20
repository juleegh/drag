using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogAction
{
    protected Action action;

    public void SetAction(string gameEventName)
    {
        ParseAndSaveEvent(gameEventName);
    }

    public void ExecuteAction()
    {
        if (action != null)
            action();
    }

    private void ParseAndSaveEvent(string eventName)
    {
        try
        {
            PerformingEvent gameEvent = (PerformingEvent)System.Enum.Parse(typeof(PerformingEvent), eventName);
            action = () => PerformingEventsManager.Instance.Notify(gameEvent);
            return;
        }
        catch (Exception) { }

        try
        {
            GameEvent gameEvent = (GameEvent)System.Enum.Parse(typeof(GameEvent), eventName);
            action = () => GameEventsManager.Instance.Notify(gameEvent);
            return;
        }
        catch (Exception) { }

        Debug.LogError(eventName);

    }

}
