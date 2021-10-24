using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutfitOptionsSelection : MonoBehaviour, RequiredComponent
{
    [SerializeField] private GameObject container;
    [SerializeField] private Transform buttonContainer;
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Button closeButton;

    public void ConfigureRequiredComponent()
    {
        LobbyEventsManager.Instance.AddActionToEvent(LobbyEvent.DependenciesLoaded, LoadOptions);
    }

    private void LoadOptions()
    {
        closeButton.onClick.AddListener(CloseSection);
        List<string> outfits = CharacterOutfitManager.Instance.GetSavedOutfits();

        foreach (string outfit in outfits)
        {
            GameObject button = Instantiate(buttonPrefab);
            button.GetComponent<OutfitOptionButton>().Initialize((selected) => { OutfitSelected(selected); }, outfit);
            button.transform.SetParent(buttonContainer);
        }
    }

    private void OutfitSelected(string outfit)
    {
        CharacterOutfitManager.Instance.Load(outfit);
    }

    private void CloseSection()
    {
        container.SetActive(false);
    }
}
