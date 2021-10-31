using System;

public static class EventParsing
{
    public static bool IsGameEvent(string eventName)
    {
        try
        {
            GameEvent gameEvent = (GameEvent)System.Enum.Parse(typeof(GameEvent), eventName);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static bool IsPerformingEvent(string eventName)
    {
        try
        {
            PerformingEvent gameEvent = (PerformingEvent)System.Enum.Parse(typeof(PerformingEvent), eventName);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static PerformingEvent GetPerformingEvent(string eventName)
    {
        return (PerformingEvent)System.Enum.Parse(typeof(PerformingEvent), eventName);
    }

    public static GameEvent GetGameEvent(string eventName)
    {
        return (GameEvent)System.Enum.Parse(typeof(GameEvent), eventName);
    }
}