using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarmentPurchaser : MonoBehaviour, RequiredComponent
{
    public static GarmentPurchaser Instance { get { return instance; } }
    private static GarmentPurchaser instance;
    [SerializeField] OutfitColorPicker GarmentColorPicking;

    private OutfitStyle selectedGarment;
    public OutfitStyle SelectedGarment { get { return selectedGarment; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.ShoppingSectionChanged, CleanArea);
    }

    private void CleanArea()
    {
        if (StoreTabsManager.Instance.CurrentShoppingStep == ShoppingStep.Outfits)
            BodyMeshController.Instance.ChangeSkinColor(Color.white);
    }

    public void GarmentSelected(OutfitStyle garment)
    {
        selectedGarment = garment;
        ShoppingEventsManager.Instance.Notify(ShoppingEvent.GarmentSelected);
        BodyMeshController.Instance.ChangeOutfit(selectedGarment.CodeName);
    }

    public void PurchaseGarment()
    {
        float price = selectedGarment.Price;
        bool purchaseResult = MoneyManager.Instance.SpendDollars(price);

        if (purchaseResult)
        {
            SaveGarment();
        }
    }

    private void SaveGarment()
    {
        int amountOfGarments = PlayerPrefs.GetInt("GarmentsOwned", 0);
        PlayerPrefs.SetString("GarmentType" + amountOfGarments, selectedGarment.CodeName);
        PlayerPrefs.SetString("GarmentColor" + amountOfGarments, ColorConversion.StringFromColor(GarmentColorPicking.GetCurrentColor()));
        amountOfGarments++;
        PlayerPrefs.SetInt("GarmentsOwned", amountOfGarments);
    }
}
