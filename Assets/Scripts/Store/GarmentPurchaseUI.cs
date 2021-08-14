using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GarmentPurchaseUI : MonoBehaviour, RequiredComponent
{
    [SerializeField] private Button purchaseButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void ConfigureRequiredComponent()
    {
        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.ShoppingSectionChanged, CleanArea);
        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.GarmentSelected, UpdatePurchaseInfo);
        purchaseButton.onClick.AddListener(() => { GarmentPurchaser.Instance.PurchaseGarment(); });
    }

    private void CleanArea()
    {
        if (StoreTabsManager.Instance.CurrentShoppingStep != ShoppingStep.Outfits)
            return;

        buttonText.text = "Select a garment";
    }

    private void UpdatePurchaseInfo()
    {
        float currentGarmentPrice = GarmentPurchaser.Instance.SelectedGarment.Price;
        bool canAffordGarment = MoneyManager.Instance.HasEnoughMoney(currentGarmentPrice);
        buttonText.text = canAffordGarment ? "Get Garment ($" + currentGarmentPrice + ")" : "Not enough money ($" + currentGarmentPrice + ")";
        buttonText.color = canAffordGarment ? Color.black : Color.red;
        purchaseButton.interactable = canAffordGarment;
    }
}
