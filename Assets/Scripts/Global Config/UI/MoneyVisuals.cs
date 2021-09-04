using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyVisuals : MonoBehaviour, GlobalComponent
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private GameObject moneyContainer;

    public void ConfigureRequiredComponent()
    {
        GameEventsManager.Instance.AddActionToEvent(GameEvent.MoneyChanged, RefreshWallet);
        GameEventsManager.Instance.AddActionToEvent(GameEvent.HideMoney, HideWallet);
        GameEventsManager.Instance.AddActionToEvent(GameEvent.ShowMoney, ShowWallet);
    }

    private void RefreshWallet()
    {
        moneyText.text = MoneyManager.Instance.Dollars.ToString("F2");
    }

    private void HideWallet()
    {
        moneyContainer.SetActive(false);
    }

    private void ShowWallet()
    {
        moneyContainer.SetActive(true);
    }
}
