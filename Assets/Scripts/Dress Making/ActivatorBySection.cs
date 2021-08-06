using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorBySection : MonoBehaviour, RequiredComponent
{
    [SerializeField] private OutfitStep activatingStep;

    public void ConfigureRequiredComponent()
    {
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.OutfitStepChanged, CheckActive);
    }

    private void CheckActive()
    {
        gameObject.SetActive(OutfitStepManager.Instance.CurrentOutfitStep == activatingStep);
    }
}
