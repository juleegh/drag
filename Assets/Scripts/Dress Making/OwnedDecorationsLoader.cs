using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnedDecorationsLoader : MonoBehaviour, RequiredComponent
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform container;
    [SerializeField] private SimpleObjectPool buttonsPool;
    private bool loaded;
    private Dictionary<DecorationInfo, AccesoryButton> buttons;

    public void ConfigureRequiredComponent()
    {
        loaded = false;

        if (OutfitEventsManager.Instance != null)
        {
            OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.OutfitStepChanged, CheckDecorations);
            OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.EmbelishmentUsed, UpdateDecoration);
            OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.EmbelishmentDeleted, UpdateDecoration);
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

    private void UpdateDecoration()
    {
        foreach (KeyValuePair<DecorationInfo, AccesoryButton> button in buttons)
        {
            bool shouldBeActive = PlayerPrefs.GetInt(button.Key.CodeName, 0) > 0;
            button.Value.gameObject.SetActive(shouldBeActive);
        }
    }

    private void LoadDecorations()
    {
        buttons = new Dictionary<DecorationInfo, AccesoryButton>();
        foreach (DecorationInfo Decoration in Inventory.Instance.decorations.Values)
        {
            int ownedQuantity = PlayerPrefs.GetInt(Decoration.CodeName, 0);

            AccesoryButton button = Instantiate(buttonPrefab).GetComponent<AccesoryButton>();
            button.Initialize(Decoration);
            button.transform.SetParent(container);
            buttons.Add(Decoration, button);

            button.gameObject.SetActive(ownedQuantity > 0);
        }
        loaded = true;
    }
}
