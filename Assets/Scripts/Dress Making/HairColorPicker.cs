using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairColorPicker : ColorPicking
{
    public override void SetCurrentColor(Color color)
    {
        base.SetCurrentColor(color);
        if (WigFitter.Instance != null) WigFitter.Instance.SetCurrentColor(color);
    }
}
