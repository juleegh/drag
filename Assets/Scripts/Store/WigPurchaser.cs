using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigPurchaser : MonoBehaviour, RequiredComponent
{
    public static WigPurchaser Instance { get { return instance; } }
    private static WigPurchaser instance;
    [SerializeField] ColorPicking wigColorPicking;
    [SerializeField] WigPreview wigPreview;

    private Wig selectedWig;
    public Wig SelectedWig { get { return selectedWig; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
    }

    public void WigSelected(Wig wig)
    {
        selectedWig = wig;
        ShoppingEventsManager.Instance.Notify(ShoppingEvent.WigSelected);
        wigPreview.ChangeSelected(WigSelection.Instance.GetConfigFromType(wig.WigType));
    }

    public void PurchaseWig()
    {
        float price = selectedWig.Price;
        bool purchaseResult = MoneyManager.Instance.SpendDollars(price);

        if (purchaseResult)
        {
            SaveWig();
        }
    }

    private void SaveWig()
    {
        int amountOfWigs = PlayerPrefs.GetInt("WigsOwned", 0);
        PlayerPrefs.SetString("WigType" + amountOfWigs, selectedWig.CodeName);
        PlayerPrefs.SetString("WigColor" + amountOfWigs, wigColorPicking.GetCurrentColor().ToString());
        amountOfWigs++;
        PlayerPrefs.SetInt("WigsOwned", amountOfWigs);
    }
}
