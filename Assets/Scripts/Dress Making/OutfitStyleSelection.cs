using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitStyleSelection : MonoBehaviour, RequiredComponent
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;

    public void ConfigureRequiredComponent()
    {
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, LoadStyles);
    }

    private void LoadStyles()
    {
        foreach (OutfitStyle outfit in BodyMeshController.Instance.OutfitStyles)
        {
            AccesoryButton button = Instantiate(buttonPrefab).GetComponent<AccesoryButton>();
            button.Initialize(outfit);
            button.transform.SetParent(container);
        }
    }
}