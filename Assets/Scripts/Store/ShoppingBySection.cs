using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoppingBySection : MonoBehaviour, RequiredComponent
{
    [SerializeField] private ShoppingStep activatingStep;

    public void ConfigureRequiredComponent()
    {
        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.ShoppingSectionChanged, CheckActive);
    }

    private void CheckActive()
    {
        gameObject.SetActive(StoreTabsManager.Instance.CurrentShoppingStep == activatingStep);
    }
}