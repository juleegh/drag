using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WigsUILoader : MonoBehaviour, RequiredComponent
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private Image wigPreview;

    public void ConfigureRequiredComponent()
    {
        if (OutfitEventsManager.Instance != null)
        {
            OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, LoadWigs);
            OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.WigSelected, RefreshView);
        }

        if (ShoppingEventsManager.Instance != null)
        {
            ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.DependenciesLoaded, LoadWigs);
        }
    }

    private void LoadWigs()
    {
        foreach (Wig Wig in WigSelection.Instance.Wigs.Values)
        {
            AccesoryButton button = Instantiate(buttonPrefab).GetComponent<AccesoryButton>();
            button.Initialize(Wig);
            button.transform.SetParent(container);
        }
        RefreshView();
    }

    private void RefreshView()
    {
        if (wigPreview != null)
            wigPreview.sprite = WigSelection.Instance.Current.Sprite;
    }
}
