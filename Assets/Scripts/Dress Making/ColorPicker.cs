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
        hue.onValueChanged.AddListener(delegate { SomethingChanged(); });
        saturation.onValueChanged.AddListener(delegate { SomethingChanged(); });
        value.onValueChanged.AddListener(delegate { SomethingChanged(); });
    }

    void Start()
    {
        SomethingChanged();
    }

    void SomethingChanged()
    {
        Color currentColor = Color.HSVToRGB(hue.value, saturation.value, value.value);
        if (colorPicking != null)
            colorPicking.SetCurrentColor(currentColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (colorPicking != null)
            colorView.color = colorPicking.GetCurrentColor();
        else
        {
            Color currentColor = Color.HSVToRGB(hue.value, saturation.value, value.value);
            colorView.color = currentColor;
        }
    }
}
