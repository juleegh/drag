using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyOptionsNavigation : MonoBehaviour
{
    [SerializeField] private Button selectOutfitBtn;
    [SerializeField] private Button newOutfitBtn;
    [SerializeField] private Button goPerformingBtn;
    [SerializeField] private Button goWorkBtn;
    [SerializeField] private GameObject selectionUI;

    void Awake()
    {
        selectOutfitBtn.onClick.AddListener(ShowSelectOutfitUI);
        newOutfitBtn.onClick.AddListener(() => { GoToSection(GameFunctions.Dress_Making); });
        goPerformingBtn.onClick.AddListener(() => { GoToSection(GameFunctions.Performing); });
        goWorkBtn.onClick.AddListener(() => { GoToSection(GameFunctions.Work); });
    }

    private void ShowSelectOutfitUI()
    {
        selectionUI.SetActive(true);
    }

    private void GoToSection(GameFunctions function)
    {
        switch (function)
        {
            case GameFunctions.Dress_Making:
                GlobalPlayerManager.Instance.GoToDragging();
                break;
            case GameFunctions.Performing:
                GlobalPlayerManager.Instance.GoToPerforming();
                break;
            case GameFunctions.Work:
                GlobalPlayerManager.Instance.GoToWork();
                break;

        }
    }
}
