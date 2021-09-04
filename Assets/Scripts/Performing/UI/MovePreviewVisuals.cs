using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MovePreviewVisuals : MonoBehaviour, RequiredComponent
{
    [SerializeField] private Image background;
    [SerializeField] private TextMeshProUGUI textIdentifier;
    [SerializeField] private MoveType moveType;

    public void ConfigureRequiredComponent()
    {
        PerformingEventsManager.Instance.AddActionToEvent(PerformingEvent.DependenciesLoaded, LoadUI);
    }

    private void LoadUI()
    {
        background.color = PerformSystem.Instance.MovesProperties.ColorByMove[moveType];
        textIdentifier.text = MovesInputManager.Instance.GetKeyFromMoveType(moveType).ToString();
    }
}
