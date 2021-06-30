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
    }

    private void SomethingChanged(bool result)
    {
        Embelisher.Instance.EmbelishingVariables.rotationPercentage = rotationProportion.value;
        Embelisher.Instance.EmbelishingVariables.scalePercentage = scaleProportion.value;
        Embelisher.Instance.EmbelishingVariables.colorPercentage = colorProportion.value;
        Embelisher.Instance.EmbelishingVariables.randomnizeScale = sizeToggle.isOn;
        Embelisher.Instance.EmbelishingVariables.randomnizeRotation = rotationToggle.isOn;
        Embelisher.Instance.EmbelishingVariables.randomnizeColorHue = hueToggle.isOn;
        Embelisher.Instance.EmbelishingVariables.randomnizeColorSaturation = saturationToggle.isOn;
        Embelisher.Instance.EmbelishingVariables.randomnizeColorValue = valueToggle.isOn;
        Embelisher.Instance.EmbelishingVariables.RandomnizeValues();
    }
}
