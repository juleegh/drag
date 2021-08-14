using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairColorPicker : ColorPicking
{
    [SerializeField] private WigPreview wigPreview;

    public override void SetCurrentColor(Color color)
    {
        base.SetCurrentColor(color);
        if (wigPreview != null) wigPreview.SetCurrentColor(color);
    }
}
