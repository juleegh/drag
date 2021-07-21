using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMeshController : MonoBehaviour, GlobalComponent
{
    private static BodyMeshController instance;
    public static BodyMeshController Instance { get { return instance; } }

    [SerializeField] private SkinnedMeshRenderer bodyMesh;
    [SerializeField] private MeshCollider colliderMesh;
    [SerializeField] private Material meshMaterial;
    [SerializeField] private Color baseSkinColor;
    [SerializeField] private Color baseClothesColor;
    [SerializeField] private string baseStyle;
    [SerializeField] private List<BodyMesh> bodyMeshes;

    private Dictionary<string, List<BodyMesh>> allOutfits;
    private List<OutfitStyle> outfitStyles;
    private Color skinColor;
    private Color clothesColor;
    private string outfitStyle;
    private string bodyType;
    public Color SkinColor { get { return skinColor; } }
    public Color ClothesColor { get { return clothesColor; } }
    public string OutfitStyle { get { return outfitStyle; } }

    public List<OutfitStyle> OutfitStyles { get { return outfitStyles; } }
    public string PlayerBodyType { get { return bodyType; } }

    public void ConfigureRequiredComponent()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GameEventsManager.Instance.AddActionToEvent(GameEvent.DependenciesLoaded, CheckPlayerSettings);
        LoadOutfits();
    }

    private void LoadOutfits()
    {
        allOutfits = new Dictionary<string, List<BodyMesh>>();

        foreach (BodyMesh bodyMesh in bodyMeshes)
        {
            string[] bodyInfo = bodyMesh.name.Split('_');
            string bodyType = bodyInfo[0] + "_" + bodyInfo[1];

            if (!allOutfits.ContainsKey(bodyType))
                allOutfits.Add(bodyType, new List<BodyMesh>());

            allOutfits[bodyType].Add(bodyMesh);
        }
        ChangeBody(bodyMeshes[0]);
        ChangeOutfit(baseStyle);
    }

    public void LoadOutfitsByPlayer()
    {
        if (!allOutfits.ContainsKey(PlayerBodyType))
            return;

        outfitStyles = new List<OutfitStyle>();
        foreach (BodyMesh bodyType in allOutfits[PlayerBodyType])
        {
            outfitStyles.Add(new OutfitStyle(bodyType));
        }
        outfitStyle = outfitStyles[0].CodeName;
    }

    public List<BodyMesh> GetBodyTypes()
    {
        List<BodyMesh> bodies = new List<BodyMesh>();
        foreach (string bodyType in allOutfits.Keys)
        {
            bodies.Add(allOutfits[bodyType][0]);
        }
        return bodies;
    }

    public void ChangeOutfit(string outfitName)
    {
        foreach (BodyMesh outfit in allOutfits[PlayerBodyType])
        {
            if (outfit.OutfitName.Equals(outfitName))
            {
                outfitStyle = outfitName;
                ChangeBody(outfit);
                return;
            }
        }
    }

    private void CheckPlayerSettings()
    {
        if (PlayerPrefs.GetString("Queen_Skin") != "")
        {
            string skin = PlayerPrefs.GetString("Queen_Skin");
            string[] values = skin.Split(',');
            float r = float.Parse(values[0]);
            float g = float.Parse(values[1]);
            float b = float.Parse(values[2]);
            Color skinColor = new Color(r, g, b, 1f);
            LoadOutfitsByPlayer();
            ChangeSkinColor(skinColor);
            ChangeBody(PlayerPrefs.GetString("Queen_Body"));
        }
        else
        {
            string[] bodyInfo = bodyMeshes[0].name.Split('_');
            bodyType = bodyInfo[0] + "_" + bodyInfo[1];
            ChangeBody(bodyType);
            ChangeSkinColor(baseSkinColor);
        }

        ChangeClothesColor(baseClothesColor);
    }

    public void ChangeBody(BodyMesh bodyTypeMesh)
    {
        string[] bodyInfo = bodyTypeMesh.name.Split('_');
        bodyType = bodyInfo[0] + "_" + bodyInfo[1];
        Mesh meshInstance = Instantiate(bodyTypeMesh.Mesh);
        bodyMesh.sharedMesh = meshInstance;
        colliderMesh.sharedMesh = meshInstance;
    }

    private void ChangeBody(string bodyType)
    {
        outfitStyle = allOutfits[bodyType][0].OutfitName;
        ChangeBody(allOutfits[bodyType][0]);
    }

    public void ChangeSkinColor(Color color)
    {
        skinColor = color;
        meshMaterial.SetColor("Color_71541232085d449d84d0f5bba4469500", color);
    }

    public void ChangeClothesColor(Color color)
    {
        clothesColor = color;
        meshMaterial.SetColor("Color_e17f239f599b4965bdc5b9b436d7ed12", color);
    }
}
