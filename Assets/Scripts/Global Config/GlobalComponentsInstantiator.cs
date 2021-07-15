using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GlobalComponentsInstantiator : MonoBehaviour
{
    private EventsManager<GameEvent> eventsManager;
    private IEnumerable<GlobalComponent> globalComponents;

    void Awake()
    {
        globalComponents = FindObjectsOfType<MonoBehaviour>().OfType<GlobalComponent>();
        eventsManager = new EventsManager<GameEvent>();
        foreach (GlobalComponent comp in globalComponents)
        {
            comp.ConfigureRequiredComponent();
        }
        eventsManager.Notify(GameEvent.DependenciesLoaded);
        DontDestroyOnLoad(this);
    }
}
