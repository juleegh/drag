using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmbelisherTransform : MonoBehaviour, RequiredComponent
{
    private static EmbelisherTransform instance;
    public static EmbelisherTransform Instance { get { return instance; } }

    [SerializeField] private Slider scale;
    [SerializeField] private Slider rotation;
    [SerializeField] private Toggle mirror;
    [SerializeField] private Transform decoration;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        scale.onValueChanged.AddListener(delegate { SomethingChanged(); });
        rotation.onValueChanged.AddListener(delegate { SomethingChanged(); });
        mirror.onValueChanged.AddListener((changed) => { SomethingChanged(); });

        SomethingChanged();
    }

    void Initialize()
    {
        EmbelishingVariables.ValueChanged += UpdatePreview;
    }

    void SomethingChanged()
    {
        Embelisher.Instance.EmbelishingVariables.CurrentScale = Vector3.one * scale.value;
        Embelisher.Instance.EmbelishingVariables.CurrentRotation = rotation.value;
        Embelisher.Instance.EmbelishingVariables.Mirrored = mirror.isOn;
    }

    public void UpdatePreview()
    {
        if (Embelisher.Instance == null)
            return;
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
