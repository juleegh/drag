using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyVisuals : MonoBehaviour, GlobalComponent
{
    [SerializeField] private TextMeshProUGUI moneyText;

    public void ConfigureRequiredComponent()
    {
        GameEventsManager.Instance.AddActionToEvent(GameEvent.MoneyChanged, RefreshWallet);
    }

    private void RefreshWallet()
    {
        moneyText.text = MoneyManager.Instance.Dollars.ToString("F2");
    }
}
