using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinColorPicker : ColorPicking
{
    public override void SetCurrentColor(Color color)
    {
        base.SetCurrentColor(color);
        if (BodyMeshController.Instance != null) BodyMeshController.Instance.ChangeSkinColor(color);
    }
}