using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClubLevelLoader : MonoBehaviour, RequiredComponent
{
    private static ClubLevelLoader instance;
    public static ClubLevelLoader Instance { get { return instance; } }

    [SerializeField] private ClubConfiguration currentClubConfiguration;
    [SerializeField] private SimpleObjectPool npcPool;
    public SimpleObjectPool NPCPool { get { return npcPool; } }
    public ClubConfiguration CurrentClubConfiguration { get { return currentClubConfiguration; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, LoadClub);
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.LeftDanceFloor, LeftDanceFloor);
    }

    private void LoadClub()
    {
        currentClubConfiguration.LoadClubConfig();
        PerformingEventsManager.Instance.Notify(PerformingEvent.ClubLoaded);
    }

    private void LeftDanceFloor()
    {
        currentClubConfiguration.SetPlayerInPosition();
    }
}
