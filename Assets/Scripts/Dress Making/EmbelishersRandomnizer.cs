using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EmbelishersRandomnizer : MonoBehaviour
{
    [SerializeField] private Slider rotationProportion;
    [SerializeField] private Slider scaleProportion;
    [SerializeField] private Slider colorProportion;
    [SerializeField] private Toggle sizeToggle;
    [SerializeField] private Toggle rotationToggle;
    [SerializeField] private Toggle hueToggle;
    [SerializeField] private Toggle saturationToggle;
    [SerializeField] private Toggle valueToggle;
    [SerializeField] private GameObject rotationContainer;
    [SerializeField] private GameObject scaleContainer;
    [SerializeField] private GameObject colorContainer;

    private UnityAction<bool> valueWasChanged;
    void Awake()
    {
        valueWasChanged += SomethingChanged;

        rotationProportion.onValueChanged.AddListener(delegate { SomethingChanged(true); });
        scaleProportion.onValueChanged.AddListener(delegate { SomethingChanged(true); });
        colorProportion.onValueChanged.AddListener(delegate { SomethingChanged(true); });
        sizeToggle.onValueChanged.AddListener(valueWasChanged);
        rotationToggle.onValueChanged.AddListener(valueWasChanged);
        hueToggle.onValueChanged.AddListener(valueWasChanged);
        saturationToggle.onValueChanged.AddListener(valueWasChanged);
        valueToggle.onValueChanged.AddListener(valueWasChanged);

        rotationContainer.SetActive(rotationToggle.isOn);
        scaleContainer.SetActive(sizeToggle.isOn);
        colorContainer.SetActive(hueToggle.isOn || saturationToggle.isOn || valueToggle.isOn);
    }

    private void SomethingChanged(bool result)
    {
        Embelisher.Instance.EmbelishingVariables.RotationPercentage = rotationProportion.value;
        Embelisher.Instance.EmbelishingVariables.ScalePercentage = scaleProportion.value;
        Embelisher.Instance.EmbelishingVariables.ColorPercentage = colorProportion.value;
        Embelisher.Instance.EmbelishingVariables.RandomnizeScale = sizeToggle.isOn;
        Embelisher.Instance.EmbelishingVariables.RandomnizeRotation = rotationToggle.isOn;
        Embelisher.Instance.EmbelishingVariables.RandomnizeColorHue = hueToggle.isOn;
        Embelisher.Instance.EmbelishingVariables.RandomnizeColorSaturation = saturationToggle.isOn;
        Embelisher.Instance.EmbelishingVariables.RandomnizeColorValue = valueToggle.isOn;
        Embelisher.Instance.EmbelishingVariables.RandomnizeValues();

        rotationContainer.SetActive(rotationToggle.isOn);
        scaleContainer.SetActive(sizeToggle.isOn);
        colorContainer.SetActive(hueToggle.isOn || saturationToggle.isOn || valueToggle.isOn);
    }
}
