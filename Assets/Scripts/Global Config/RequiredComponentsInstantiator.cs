using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RequiredComponentsInstantiator<T> : MonoBehaviour
{
    private EventsManager<T> eventsManager;
    private IEnumerable<IRequiredComponent> requiredComponents;

    void Awake()
    {
        requiredComponents = FindObjectsOfType<MonoBehaviour>().OfType<IRequiredComponent>();
        eventsManager = new EventsManager<T>();
        foreach (IRequiredComponent comp in requiredComponents)
        {
            comp.ConfigureRequiredComponent();
        }
        eventsManager.Notify(default(T));
    }
}
