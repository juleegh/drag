using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbelishingVariables
{
    public delegate void EmbelisherChanges();
    public static event EmbelisherChanges ValueChanged;

    private Color currentColor;
    private Sprite currentStyle;

    private bool randomnizeRotation = false;
    private bool randomnizeScale = false;
    private bool randomnizeColorHue = false;
    private bool randomnizeColorSaturation = false;
    private bool randomnizeColorValue = false;

    private float rotationPercentage;
    private float scalePercentage;
    private float colorPercentage;

    private Vector3 currentScale;
    private float currentRotation;
    private bool mirrored = false;

    private Vector3 randomScale;
    private float randomRotation;
    private Vector3 randomColorVariation;

    public Color CurrentColor { get { return currentColor; } set { currentColor = value; CallForUpdate(); } }
    public Sprite CurrentStyle { get { return currentStyle; } set { currentStyle = value; CallForUpdate(); } }

    public bool RandomnizeRotation { get { return randomnizeRotation; } set { randomnizeRotation = value; CallForUpdate(); } }
    public bool RandomnizeScale { get { return randomnizeScale; } set { randomnizeScale = value; CallForUpdate(); } }
    public bool RandomnizeColorHue { get { return randomnizeColorHue; } set { randomnizeColorHue = value; CallForUpdate(); } }
    public bool RandomnizeColorSaturation { get { return randomnizeColorSaturation; } set { randomnizeColorSaturation = value; CallForUpdate(); } }
    public bool RandomnizeColorValue { get { return randomnizeColorValue; } set { randomnizeColorValue = value; CallForUpdate(); } }

    public float RotationPercentage { get { return rotationPercentage; } set { rotationPercentage = value; CallForUpdate(); } }
    public float ScalePercentage { get { return scalePercentage; } set { scalePercentage = value; CallForUpdate(); } }
    public float ColorPercentage { get { return colorPercentage; } set { colorPercentage = value; CallForUpdate(); } }

    public Vector3 CurrentScale { get { return currentScale; } set { currentScale = value; CallForUpdate(); } }
    public float CurrentRotation { get { return currentRotation; } set { currentRotation = value; CallForUpdate(); } }
    public bool Mirrored { get { return mirrored; } set { mirrored = value; CallForUpdate(); } }

    public Vector3 RandomScale { get { return randomScale; } set { randomScale = value; CallForUpdate(); } }
    public float RandomRotation { get { return randomRotation; } set { randomRotation = value; CallForUpdate(); } }
    public Vector3 RandomColorVariation { get { return randomColorVariation; } set { randomColorVariation = value; CallForUpdate(); } }

    public Vector3 Scale { get { return CurrentScale + RandomScale; } }
    public float Rotation { get { return CurrentRotation + RandomRotation; } }

    public void RandomnizeValues()
    {
        RandomScale = RandomnizeScale ? Vector3.one * Random.Range(-ScalePercentage, ScalePercentage) : Vector3.zero;
        RandomRotation = RandomnizeRotation ? 180 * Random.Range(-RotationPercentage, RotationPercentage) : 0;
        RandomColorVariation = new Vector3(RandomnizeColorHue ? Random.Range(-ColorPercentage, ColorPercentage) : 0, RandomnizeColorSaturation ? Random.Range(-ColorPercentage, ColorPercentage) : 0, RandomnizeColorValue ? Random.Range(-ColorPercentage, ColorPercentage) : 0);
    }

    public Color GetTempColor()
    {
        float h = 0; float s = 0; float v = 0;
        Color.RGBToHSV(CurrentColor, out h, out s, out v);
        h = (h + RandomColorVariation.x);
        s = (s + RandomColorVariation.y);
        v = (v + RandomColorVariation.z);
        h = Mathf.Abs(h);
        s = Mathf.Abs(s);
        v = Mathf.Abs(v);
        h = Mathf.Clamp(h, 0, 1);
        s = Mathf.Clamp(s, 0, 1);
        v = Mathf.Clamp(v, 0, 1);
        return Color.HSVToRGB(h, s, v);
    }

    private void CallForUpdate()
    {
        EmbelisherTransform.Instance.UpdatePreview();
    }
}
