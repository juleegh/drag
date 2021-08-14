using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RotaryHeart.Lib.SerializableDictionary;

public class StoreTabsManager : MonoBehaviour, RequiredComponent
{
    private static StoreTabsManager instance;
    public static StoreTabsManager Instance { get { return instance; } }

    [Serializable]
    public class TabButtons : SerializableDictionaryBase<ShoppingStep, GameObject> { }

    [SerializeField] private TabButtons sections;
    [SerializeField] private Button cancelButton;

    private Dictionary<ShoppingStep, ShoppingTabButton> buttons;
    private ShoppingStep step;
    public ShoppingStep CurrentShoppingStep { get { return step; } }

    public void ConfigureRequiredComponent()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.DependenciesLoaded, LoadUI);
    }

    private void LoadUI()
    {
        int index = 0;
        buttons = new Dictionary<ShoppingStep, ShoppingTabButton>();
        ShoppingTabButton[] containerButtons = GetComponentsInChildren<ShoppingTabButton>();
        foreach (ShoppingTabButton button in containerButtons)
        {
            button.SetButtonClick((step) => { ButtonPressed(step); }, (ShoppingStep)index);
            buttons.Add((ShoppingStep)index, button);
            index++;
        }
        step = ShoppingStep.Wigs;
        ButtonPressed(ShoppingStep.Wigs);

        cancelButton.onClick.RemoveAllListeners();
        cancelButton.onClick.AddListener(LeaveStore);
    }

    public void ButtonPressed(ShoppingStep pressedButton)
    {
        step = pressedButton;
        foreach (KeyValuePair<ShoppingStep, GameObject> button in sections)
        {
            button.Value.SetActive(pressedButton == button.Key);
            buttons[button.Key].MarkAsSelected(pressedButton == button.Key);
        }
        ShoppingEventsManager.Instance.Notify(ShoppingEvent.ShoppingSectionChanged);
    }

    private void LeaveStore()
    {
        GlobalPlayerManager.Instance.GoToLobby();
    }

}
