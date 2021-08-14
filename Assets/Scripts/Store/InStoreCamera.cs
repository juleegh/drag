using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;
using DG.Tweening;

public class InStoreCamera : MonoBehaviour, RequiredComponent
{
    [Serializable]
    public class StepPositions : SerializableDictionaryBase<ShoppingStep, Transform> { }

    [SerializeField] private StepPositions positions;
    [SerializeField] private GameObject cameraObject;
    [SerializeField] private float movementDelay;


    public void ConfigureRequiredComponent()
    {
        ShoppingEventsManager.Instance.AddActionToEvent(ShoppingEvent.ShoppingSectionChanged, MoveCameraToSection);
    }

    private void MoveCameraToSection()
    {
        cameraObject.transform.DOMove(positions[StoreTabsManager.Instance.CurrentShoppingStep].position, movementDelay).SetEase(Ease.OutExpo);
        cameraObject.transform.DORotate(positions[StoreTabsManager.Instance.CurrentShoppingStep].eulerAngles, movementDelay).SetEase(Ease.OutExpo);
        GlobalPlayerManager.Instance.transform.rotation = Quaternion.identity;
    }
}