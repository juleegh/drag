using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPreview : MonoBehaviour
{
    [SerializeField] protected SkinnedMeshRenderer bodyMesh;
    [SerializeField] protected MeshCollider colliderMesh;
    [SerializeField] protected Material meshMaterial;
    public Material MeshMaterial { get { return meshMaterial; } }
    public SkinnedMeshRenderer BodyMesh { get { return bodyMesh; } }

    protected Color skinColor;
    protected Color clothesColor;
    protected string outfitStyle;
    protected string bodyType;
    public Color SkinColor { get { return skinColor; } }
    public Color ClothesColor { get { return clothesColor; } }
    public string OutfitStyle { get { return outfitStyle; } }
    protected Material bodyMaterial;


    public virtual void ChangeBody(BodyMesh bodyTypeMesh)
    {
        string[] bodyInfo = bodyTypeMesh.name.Split('_');
        bodyType = bodyInfo[0] + "_" + bodyInfo[1];
        Mesh meshInstance = Instantiate(bodyTypeMesh.Mesh);
        bodyMesh.sharedMesh = meshInstance;
        colliderMesh.sharedMesh = meshInstance;
    }

    public virtual void ChangeSkinColor(Color color)
    {
        if (bodyMaterial == null)
            bodyMaterial = bodyMesh.material;

        skinColor = color;
        meshMaterial.SetColor("Color_71541232085d449d84d0f5bba4469500", color);
    }

    public virtual void ChangeClothesColor(Color color)
    {
        if (bodyMaterial == null)
            bodyMaterial = bodyMesh.material;

        clothesColor = color;
        meshMaterial.SetColor("Color_e17f239f599b4965bdc5b9b436d7ed12", color);
    }
}