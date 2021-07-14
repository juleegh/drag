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
    [SerializeField] private CameraController cameraObject;
    [SerializeField] private float movementDelay;


    public void ConfigureRequiredComponent()
    {
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.OutfitStepChanged, MoveCameraToSection);
    }

    private void MoveCameraToSection()
    {
        cameraObject.ClearValues();
        cameraObject.transform.DOMove(positions[OutfitStepManager.Instance.CurrentOutfitStep].position, movementDelay).SetEase(Ease.OutExpo);
        cameraObject.transform.DORotate(positions[OutfitStepManager.Instance.CurrentOutfitStep].eulerAngles, movementDelay).SetEase(Ease.OutExpo);
    }
}
