using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubConfiguration : MonoBehaviour, RequiredComponent
{
    [SerializeField] private Transform entryPoint;
    [SerializeField] private Transform dancefloor;
    [SerializeField] private CharacterWalking character;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, SetPlayerInEntryPoint);
    }

    private void SetPlayerInEntryPoint()
    {
        character.transform.position = entryPoint.position;
        character.PossesPlayer();
    }
}
