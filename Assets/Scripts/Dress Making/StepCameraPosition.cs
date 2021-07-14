using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;
using DG.Tweening;

public class StepCameraPosition : MonoBehaviour, IRequiredComponent
{
    [Serializable]
    public class StepPositions : SerializableDictionaryBase<OutfitStep, Transform> { }

    [SerializeField] private StepPositions positions;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float movementDelay;

    public void ConfigureRequiredComponent()
    {
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.OutfitStepChanged, MoveCameraToSection);
    }

    private void MoveCameraToSection()
    {
        cameraTransform.DOMove(positions[OutfitStepManager.Instance.CurrentOutfitStep].position, movementDelay).SetEase(Ease.OutExpo);
        cameraTransform.DORotate(positions[OutfitStepManager.Instance.CurrentOutfitStep].eulerAngles, movementDelay).SetEase(Ease.OutExpo);
    }
}
