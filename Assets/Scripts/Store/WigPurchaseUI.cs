using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WigPurchaseUI : MonoBehaviour, RequiredComponent
{
    [SerializeField] private Button purchaseButton;
    [SerializeField] private TextMeshProUGUI buttonText;

    public void ConfigureRequiredComponent()
    {
        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.ShoppingSectionChanged, CleanArea);
        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.WigSelected, UpdatePurchaseInfo);
        purchaseButton.onClick.AddListener(() => { WigPurchaser.Instance.PurchaseWig(); });
    }

    private void CleanArea()
    {
        if (StoreTabsManager.Instance.CurrentShoppingStep != ShoppingStep.Wigs)
            return;

        buttonText.text = "Select a wig";
    }

    private void UpdatePurchaseInfo()
    {
        float currentWigPrice = WigPurchaser.Instance.SelectedWig.Price;
        bool canAffordWig = MoneyManager.Instance.HasEnoughMoney(currentWigPrice);
        buttonText.text = canAffordWig ? "Get Wig ($" + currentWigPrice + ")" : "Not enough money ($" + currentWigPrice + ")";
        buttonText.color = canAffordWig ? Color.black : Color.red;
        purchaseButton.interactable = canAffordWig;
    }
}
