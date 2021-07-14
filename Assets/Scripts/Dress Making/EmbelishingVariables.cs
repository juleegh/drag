using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmbelishingVariables
{
    public bool randomnizeRotation = false;
    public bool randomnizeScale = false;
    public bool randomnizeColorHue = false;
    public bool randomnizeColorSaturation = false;
    public bool randomnizeColorValue = false;

    public float rotationPercentage;
    public float scalePercentage;
    public float colorPercentage;

    public Vector3 CurrentScale;
    public float CurrentRotation;
    public bool mirrored = false;

    public Vector3 RandomScale;
    public float RandomRotation;
    public Vector3 RandomColorVariation;

    public Vector3 Scale { get { return CurrentScale + RandomScale; } }
    public float Rotation { get { return CurrentRotation + RandomRotation; } }

    public void RandomnizeValues()
    {
        RandomScale = randomnizeScale ? Vector3.one * Random.Range(-scalePercentage, scalePercentage) : Vector3.zero;
        RandomRotation = randomnizeRotation ? 180 * Random.Range(-rotationPercentage, rotationPercentage) : 0;
        RandomColorVariation = new Vector3(randomnizeColorHue ? Random.Range(-colorPercentage, colorPercentage) : 0, randomnizeColorSaturation ? Random.Range(-colorPercentage, colorPercentage) : 0, randomnizeColorValue ? Random.Range(-colorPercentage, colorPercentage) : 0);
    }

    public Color GetTempColor(Color currentColor)
    {
        float h = 0; float s = 0; float v = 0;
        Color.RGBToHSV(currentColor, out h, out s, out v);
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
}
