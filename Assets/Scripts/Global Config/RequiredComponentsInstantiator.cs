using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RequiredComponentsInstantiator<T> : MonoBehaviour
{
    private EventsManager<T> eventsManager;
    private IEnumerable<RequiredComponent> requiredComponents;

    void Awake()
    {
        if (GlobalPlayerManager.Instance == null)
            return;

        requiredComponents = FindObjectsOfType<MonoBehaviour>().OfType<RequiredComponent>();
        eventsManager = new EventsManager<T>();
        foreach (RequiredComponent comp in requiredComponents)
        {
            comp.ConfigureRequiredComponent();
        }
        eventsManager.Notify(default(T));
    }
}
