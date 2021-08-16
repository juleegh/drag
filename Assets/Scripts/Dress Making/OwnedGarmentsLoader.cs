using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedGarmentsLoader : MonoBehaviour, RequiredComponent
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;

    public void ConfigureRequiredComponent()
    {
        if (OutfitEventsManager.Instance != null)
        {
            OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, LoadStyles);
        }
    }

    private void LoadStyles()
    {

        int amountOfGarments = PlayerPrefs.GetInt("GarmentsOwned", 0);

        for (int i = 0; i < amountOfGarments; i++)
        {
            string storedType = PlayerPrefs.GetString("GarmentType" + i);
            string storedColor = PlayerPrefs.GetString("GarmentColor" + i);
            Color garmentColor = ColorConversion.ColorFromString(storedColor);
            OutfitStyle garment = BodyMeshController.Instance.GetOutfitByName(storedType);

            InventoryItemButton button = Instantiate(buttonPrefab).GetComponent<InventoryItemButton>();
            button.Initialize(garment);
            button.SetItemColor(garmentColor);
            button.transform.SetParent(container);
        }
    }
}
