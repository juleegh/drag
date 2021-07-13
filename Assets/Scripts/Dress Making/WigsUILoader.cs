using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigsUILoader : MonoBehaviour, IRequiredComponent
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;

    public void ConfigureRequiredComponent()
    {
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, LoadWigs);
    }

    private void LoadWigs()
    {
        foreach (Wig Wig in WigSelection.Instance.Wigs.Values)
        {
            AccesoryButton button = Instantiate(buttonPrefab).GetComponent<AccesoryButton>();
            button.Initialize(Wig);
            button.transform.SetParent(container);
        }
    }
}
