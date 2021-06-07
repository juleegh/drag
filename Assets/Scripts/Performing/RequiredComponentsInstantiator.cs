using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RequiredComponentsInstantiator : MonoBehaviour
{
    private PerformingEventsManager eventsManager;
    private IEnumerable<IRequiredComponent> requiredComponents;

    void Start()
    {
        requiredComponents = FindObjectsOfType<MonoBehaviour>().OfType<IRequiredComponent>();
        eventsManager = new PerformingEventsManager();
        foreach (IRequiredComponent comp in requiredComponents)
        {
            comp.ConfigureRequiredComponent();
        }
        eventsManager.Notify(PerformingEvent.DependenciesLoaded);
    }
}
