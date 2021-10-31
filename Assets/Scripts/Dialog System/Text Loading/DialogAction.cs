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

    private void ParseAndSaveEvent(string gameEvent)
    {
        if (EventParsing.IsGameEvent(gameEvent))
        {
            action = () => GameEventsManager.Instance.Notify(EventParsing.GetGameEvent(gameEvent));
        }
        else if (EventParsing.IsPerformingEvent(gameEvent))
        {
            action = () => PerformingEventsManager.Instance.Notify(EventParsing.GetPerformingEvent(gameEvent));
        }
    }

    private void ParseAndSaveCallback(string gameEvent)
    {
        if (EventParsing.IsGameEvent(gameEvent))
        {
            GameEventsManager.Instance.AddActionToEvent(EventParsing.GetGameEvent(gameEvent), ActionCallback);
        }
        else if (EventParsing.IsPerformingEvent(gameEvent))
        {
            PerformingEventsManager.Instance.AddActionToEvent(EventParsing.GetPerformingEvent(gameEvent), ActionCallback);
        }
    }

}
