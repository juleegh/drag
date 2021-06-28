using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] private Image colorView;
    [SerializeField] private Slider hue;
    [SerializeField] private Slider saturation;
    [SerializeField] private Slider value;
    [SerializeField] private ColorPicking colorPicking;

    // Update is called once per frame
    void Update()
    {
        Color currentColor = Color.HSVToRGB(hue.value, saturation.value, value.value);
        colorView.color = currentColor;
        if (colorPicking != null) colorPicking.SetCurrentColor(currentColor);
    }
}
