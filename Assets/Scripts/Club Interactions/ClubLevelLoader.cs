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

    public void ConfigureRequiredComponent()
    {
        instance = this;
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, LoadClub);
    }

    private void LoadClub()
    {
        currentClubConfiguration.LoadClubConfig();
        PerformingEventsManager.Instance.Notify(PerformingEvent.ClubLoaded);
    }
}
