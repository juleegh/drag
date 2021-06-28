using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPicking : MonoBehaviour
{
    protected Color currentColor;

    public virtual void SetCurrentColor(Color color)
    {
        currentColor = color;
    }

    public Color GetCurrentColor()
    {
        return currentColor;
    }
}
