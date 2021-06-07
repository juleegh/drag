using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinColorConfigurator : MonoBehaviour
{
    [SerializeField] private SkinColorSettings colorSettings;
    [SerializeField] private Renderer skinMaterial;
    [SerializeField] private SkinType skinType;
    [SerializeField] private ClothesColor clothesColor;

    [ContextMenu("Change Color")]
    public void ChangeColor()
    {
        skinMaterial.material.SetTexture("_MainTex", colorSettings.SkinColors[skinType][clothesColor]);
    }
}
