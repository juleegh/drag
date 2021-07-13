using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EventsManager<T>
{
    private static EventsManager<T> instance;
    public static EventsManager<T> Instance { get { return instance; } }
    private Dictionary<T, List<Action>> linkedEvents;

    public EventsManager()
    {
        instance = this;
        linkedEvents = new Dictionary<T, List<Action>>();
    }

    public void AddActionToEvent(T typeEvent, Action newAction)
    {
        if (!linkedEvents.ContainsKey(typeEvent))
            linkedEvents.Add(typeEvent, new List<Action>());
        if (linkedEvents[typeEvent] == null)
            linkedEvents[typeEvent] = new List<Action>();

        linkedEvents[typeEvent].Add(newAction);
    }

    public void Notify(T typeEvent)
    {
        if (!linkedEvents.ContainsKey(typeEvent) || linkedEvents[typeEvent] == null)
            return;

        foreach (Action linkedAction in linkedEvents[typeEvent])
        {
            linkedAction();
        }
    }
}
