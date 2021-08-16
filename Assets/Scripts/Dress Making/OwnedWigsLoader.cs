using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedWigsLoader : MonoBehaviour, RequiredComponent
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;

    public void ConfigureRequiredComponent()
    {
        if (OutfitEventsManager.Instance != null)
        {
            OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, LoadWigs);
        }
    }

    private void LoadWigs()
    {
        int amountOfWigs = PlayerPrefs.GetInt("WigsOwned", 0);

        for (int i = 0; i < amountOfWigs; i++)
        {
            string storedType = PlayerPrefs.GetString("WigType" + i);
            string storedColor = PlayerPrefs.GetString("WigColor" + i);
            Color wigColor = ColorConversion.ColorFromString(storedColor);
            WigType wigType = WigSelection.Instance.GetWigTypeFromName(storedType);
            Wig Wig = WigSelection.Instance.Wigs[wigType];

            InventoryItemButton button = Instantiate(buttonPrefab).GetComponent<InventoryItemButton>();
            button.Initialize(Wig);
            button.SetItemColor(wigColor);
            button.transform.SetParent(container);
        }
    }
}
