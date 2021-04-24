using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PerformingEventsManager
{
    private static PerformingEventsManager instance;
    public static PerformingEventsManager Instance { get { return instance; } }
    private Dictionary<PerformingEvent, List<Action>> linkedEvents;

    public PerformingEventsManager()
    {
        instance = this;
        linkedEvents = new Dictionary<PerformingEvent, List<Action>>();
    }

    public void AddActionToEvent(PerformingEvent typeEvent, Action newAction)
    {
        if (!linkedEvents.ContainsKey(typeEvent))
            linkedEvents.Add(typeEvent, new List<Action>());
        if (linkedEvents[typeEvent] == null)
            linkedEvents[typeEvent] = new List<Action>();

        linkedEvents[typeEvent].Add(newAction);
    }

    public void Notify(PerformingEvent typeEvent)
    {
        if (!linkedEvents.ContainsKey(typeEvent) || linkedEvents[typeEvent] == null)
            return;

        foreach (Action linkedAction in linkedEvents[typeEvent])
        {
            linkedAction();
        }
    }
}
