using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogAction
{
    protected Character belongingCharacter;
    protected Action action;
    private Vector3 originPosition;
    private Quaternion originRotation;
    private bool hasCallback;
    public bool HasCallback { get { return hasCallback; } }

    public void SetCharacter(Character character)
    {
        belongingCharacter = character;
        hasCallback = false;
    }

    public void SetAction(string gameEventName)
    {
        ParseAndSaveEvent(gameEventName);
    }

    public void SetupCallback(string gameEventName)
    {
        ParseAndSaveCallback(gameEventName);
        hasCallback = true;
    }

    public void ExecuteAction()
    {
        if (action != null)
            action();
    }

    public void SetCallbackPosition()
    {
        originPosition = CharacterWalking.Instance.transform.position;
        originRotation = CharacterWalking.Instance.transform.rotation;
    }

    private void ActionCallback()
    {
        CharacterWalking.Instance.PlacePlayerForWalking(originPosition, originRotation);
        DialogSystemController.Instance.StartInteraction(belongingCharacter);
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
    }

    private void ParseAndSaveCallback(string eventName)
    {
        try
        {
            PerformingEvent gameEvent = (PerformingEvent)System.Enum.Parse(typeof(PerformingEvent), eventName);
            PerformingEventsManager.Instance.AddActionToEvent(gameEvent, ActionCallback);
            return;
        }
        catch (Exception) { }

        try
        {
            GameEvent gameEvent = (GameEvent)System.Enum.Parse(typeof(GameEvent), eventName);
            GameEventsManager.Instance.AddActionToEvent(gameEvent, ActionCallback);
            return;
        }
        catch (Exception) { }
    }

}
