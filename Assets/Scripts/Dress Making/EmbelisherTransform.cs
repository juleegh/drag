using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EmbelisherTransform : MonoBehaviour, RequiredComponent
{
    private static EmbelisherTransform instance;
    public static EmbelisherTransform Instance { get { return instance; } }

    [SerializeField] private Slider scale;
    [SerializeField] private Slider rotation;
    [SerializeField] private Toggle mirror;
    [SerializeField] private Transform decoration;
    [SerializeField] private TextMeshProUGUI quantity;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        scale.onValueChanged.AddListener(delegate { SomethingChanged(); });
        rotation.onValueChanged.AddListener(delegate { SomethingChanged(); });
        mirror.onValueChanged.AddListener((changed) => { SomethingChanged(); });

        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.OutfitStepChanged, CleanSection);
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.EmbelishmentSelected, UpdateQuantity);
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.EmbelishmentUsed, UpdateQuantity);
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.EmbelishmentDeleted, UpdateQuantity);

        quantity.text = "";

        SomethingChanged();
    }

    void UpdateQuantity()
    {
        quantity.text = "x" + Inventory.Instance.CurrentDecorationsLeft();
        UpdatePreview();
    }

    void CleanSection()
    {
        if (OutfitStepManager.Instance.CurrentOutfitStep != OutfitStep.Outfit)
            return;

        decoration.gameObject.SetActive(false);
        quantity.text = "";
    }

    void SomethingChanged()
    {
        Embelisher.Instance.EmbelishingVariables.CurrentScale = Vector3.one * scale.value;
        Embelisher.Instance.EmbelishingVariables.CurrentRotation = rotation.value;
        Embelisher.Instance.EmbelishingVariables.Mirrored = mirror.isOn;
    }

    public void UpdatePreview()
    {
        if (Embelisher.Instance == null || !Embelisher.Instance.HasEmbelishment)
            return;

        decoration.gameObject.SetActive(true);
        decoration.rotation = Quaternion.identity;
        decoration.Rotate(Vector3.forward * Embelisher.Instance.EmbelishingVariables.Rotation, Space.Self);
        if (Embelisher.Instance.EmbelishingVariables.Mirrored)
            decoration.Rotate(Vector3.up * 180, Space.Self);

        DecorationSetting currentDecoration = Inventory.Instance.CurrentSelected;
        decoration.localScale = Embelisher.Instance.EmbelishingVariables.Scale * AspectRatioUtil.GetSpriteAspectRatio(currentDecoration.Sprite);
        decoration.GetComponent<Image>().color = Embelisher.Instance.EmbelishingVariables.GetTempColor();
        decoration.GetComponent<Image>().sprite = Inventory.Instance.CurrentSelected.Sprite;
    }
}
