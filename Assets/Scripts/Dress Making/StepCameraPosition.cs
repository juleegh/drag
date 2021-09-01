using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;
using DG.Tweening;

public class StepCameraPosition : MonoBehaviour, RequiredComponent
{
    [Serializable]
    public class StepPositions : SerializableDictionaryBase<OutfitStep, Transform> { }

    [Serializable]
    public class StepPoses : SerializableDictionaryBase<OutfitStep, PoseType> { }

    [SerializeField] private StepPositions positions;
    [SerializeField] private StepPoses poses;
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
        PosePerformer.Instance.HitPose(poses[OutfitStepManager.Instance.CurrentOutfitStep]);
        GlobalPlayerManager.Instance.transform.rotation = Quaternion.identity;
    }
}
