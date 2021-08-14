using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmbelishmentPurchaseUI : MonoBehaviour, RequiredComponent
{
    [SerializeField] private Button purchaseButton;
    [SerializeField] private Button addOne;
    [SerializeField] private Button addFive;
    [SerializeField] private Button minusOne;
    [SerializeField] private Button minusFive;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI currentquantityText;
    private int quantity;

    public void ConfigureRequiredComponent()
    {
        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.ShoppingSectionChanged, CleanArea);
        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.DecorationSelected, ChangedDecoration);
        addOne.onClick.AddListener(AddOne);
        addFive.onClick.AddListener(AddFive);
        minusOne.onClick.AddListener(MinusOne);
        minusFive.onClick.AddListener(MinusFive);
        purchaseButton.onClick.AddListener(() =>
        {
            EmbelishmentsPurchaser.Instance.PurchaseEmbelishment(quantity);
            ChangedDecoration();
        });
    }

    private void CleanArea()
    {
        if (StoreTabsManager.Instance.CurrentShoppingStep != ShoppingStep.Embelishments)
            return;

        buttonText.text = "Select an embelishment";
        currentquantityText.text = "Currently owned: -";
        quantityText.text = "-";
    }

    private void ChangedDecoration()
    {
        quantity = 1;
        UpdatePurchaseInfo();
    }

    private void UpdatePurchaseInfo()
    {
        float purchaseValue = quantity * EmbelishmentsPurchaser.Instance.CurrentPrice;
        quantityText.text = quantity.ToString();
        buttonText.text = "Get embelishments ($" + purchaseValue + ")";
        currentquantityText.text = "Currently owned: " + EmbelishmentsPurchaser.Instance.GetCurrentAmount();
    }

    private void AddOne()
    {
        quantity += 1;
        if (quantity > EmbelishmentsPurchaser.Instance.MaximumAllowedQuantity())
            quantity = EmbelishmentsPurchaser.Instance.MaximumAllowedQuantity();
        UpdatePurchaseInfo();
    }

    private void AddFive()
    {
        quantity += 5;
        if (quantity > EmbelishmentsPurchaser.Instance.MaximumAllowedQuantity())
            quantity = EmbelishmentsPurchaser.Instance.MaximumAllowedQuantity();
        UpdatePurchaseInfo();
    }

    private void MinusOne()
    {
        quantity -= 1;
        if (quantity < 0)
            quantity = 0;
        UpdatePurchaseInfo();
    }

    private void MinusFive()
    {
        quantity -= 5;
        if (quantity < 0)
            quantity = 0;
        UpdatePurchaseInfo();
    }
}
