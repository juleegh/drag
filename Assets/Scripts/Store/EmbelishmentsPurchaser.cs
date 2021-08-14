using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbelishmentsPurchaser : MonoBehaviour, RequiredComponent
{
    public static EmbelishmentsPurchaser Instance { get { return instance; } }
    private static EmbelishmentsPurchaser instance;

    private DecorationInfo selectedDecoration;
    public float CurrentPrice { get { return selectedDecoration.Price; } }

    [SerializeField] private SpriteRenderer previewRenderer;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.ShoppingSectionChanged, CleanArea);
    }

    private void CleanArea()
    {
        previewRenderer.sprite = null;
    }

    public void DecorationSelected(DecorationInfo decoration)
    {
        selectedDecoration = decoration;
        ShoppingEventsManager.Instance.Notify(ShoppingEvent.DecorationSelected);
        previewRenderer.sprite = selectedDecoration.Sprite;
    }

    public void PurchaseEmbelishment(int quantity)
    {
        float price = selectedDecoration.Price * quantity;
        bool purchaseResult = MoneyManager.Instance.SpendDollars(price);

        if (purchaseResult)
        {
            SaveEmbelishments(quantity);
        }
    }

    public int GetCurrentAmount()
    {
        return PlayerPrefs.GetInt(selectedDecoration.CodeName, 0);
    }

    private void SaveEmbelishments(int quantity)
    {
        int amountOfEmbelishment = PlayerPrefs.GetInt(selectedDecoration.CodeName, 0);
        PlayerPrefs.SetInt(selectedDecoration.CodeName, amountOfEmbelishment + quantity);
    }

    public int MaximumAllowedQuantity()
    {
        float currentDollars = MoneyManager.Instance.Dollars;
        return Mathf.FloorToInt(currentDollars / selectedDecoration.Price);
    }
}
