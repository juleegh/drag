using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationUILoader : MonoBehaviour, RequiredComponent
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;
    private bool loaded;

    public void ConfigureRequiredComponent()
    {
        loaded = false;

        if (OutfitEventsManager.Instance != null)
        {
            OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.OutfitStepChanged, CheckDecorations);
        }

        if (ShoppingEventsManager.Instance != null)
        {
            ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.ShoppingSectionChanged, CheckDecorations);
        }
    }

    private void CheckDecorations()
    {
        //if (OutfitStepManager.Instance.CurrentOutfitStep == OutfitStep.Outfit)
        {
            if (!loaded)
                LoadDecorations();
        }
    }

    private void LoadDecorations()
    {
        foreach (DecorationInfo Decoration in Inventory.Instance.decorations.Values)
        {
            AccesoryButton button = Instantiate(buttonPrefab).GetComponent<AccesoryButton>();
            button.Initialize(Decoration);
            button.transform.SetParent(container);
        }
        loaded = true;
    }
}
