using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private Image colorView;
    [SerializeField] private Slider hue;
    [SerializeField] private Slider saturation;
    [SerializeField] private Slider value;
    [SerializeField] private ColorPicking colorPicking;

    void Awake()
    {
        if (hue != null) hue.onValueChanged.AddListener(delegate { SomethingChanged(); });
        if (saturation != null) saturation.onValueChanged.AddListener(delegate { SomethingChanged(); });
        if (value != null) value.onValueChanged.AddListener(delegate { SomethingChanged(); });
    }

    void Start()
    {
        SomethingChanged();
    }

    void SomethingChanged()
    {
        Color currentColor = GetInputColor();

        if (colorPicking != null)
            colorPicking.SetCurrentColor(currentColor);

        colorView.color = currentColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (colorPicking != null && colorPicking.GetCurrentColor() != GetInputColor())
            colorView.color = colorPicking.GetCurrentColor();
    }

    private Color GetInputColor()
    {
        float h = 1;
        if (hue != null) h = hue.value;
        float s = 1;
        if (saturation != null) s = saturation.value;
        float v = 1;
        if (value != null) v = value.value;

        return Color.HSVToRGB(h, s, v);
    }
}
