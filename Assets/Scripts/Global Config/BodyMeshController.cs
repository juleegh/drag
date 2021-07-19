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
    [SerializeField] private List<BodyType> outfits;

    private Dictionary<string, List<BodyType>> allOutfits;
    private List<OutfitStyle> outfitStyles;
    private Color skinColor;
    private Color clothesColor;
    private string outfitStyle;
    public Color SkinColor { get { return skinColor; } }
    public Color ClothesColor { get { return clothesColor; } }
    public string OutfitStyle { get { return outfitStyle; } }

    public List<OutfitStyle> OutfitStyles { get { return outfitStyles; } }
    public string PlayerBodyType { get { return PlayerPrefs.GetString("Queen_Body"); } }

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
        allOutfits = new Dictionary<string, List<BodyType>>();
        outfitStyles = new List<OutfitStyle>();

        foreach (BodyType outfit in outfits)
        {
            string[] bodyInfo = outfit.name.Split('_');
            string bodyType = bodyInfo[0] + "_" + bodyInfo[1];

            if (!allOutfits.ContainsKey(bodyType))
                allOutfits.Add(bodyType, new List<BodyType>());

            allOutfits[bodyType].Add(outfit);
        }
    }

    public void LoadOutfitsByPlayer()
    {
        if (!allOutfits.ContainsKey(PlayerBodyType))
            return;

        foreach (BodyType bodyType in allOutfits[PlayerBodyType])
        {
            outfitStyles.Add(new OutfitStyle(bodyType));
        }
    }

    public List<BodyType> GetBodyTypes()
    {
        List<BodyType> bodies = new List<BodyType>();
        foreach (string bodyType in allOutfits.Keys)
        {
            bodies.Add(allOutfits[bodyType][0]);
        }
        return bodies;
    }

    public void ChangeOutfit(string outfitName)
    {
        foreach (BodyType outfit in allOutfits[PlayerBodyType])
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
            ChangeBody(PlayerBodyType);
        }
        else
        {
            ChangeSkinColor(baseSkinColor);
        }

        ChangeClothesColor(baseClothesColor);
    }

    public void ChangeBody(BodyType bodyType)
    {
        Mesh meshInstance = Instantiate(bodyType.Mesh);
        bodyMesh.sharedMesh = meshInstance;
        colliderMesh.sharedMesh = meshInstance;
    }

    private void ChangeBody(string bodyType)
    {
        string[] bodyInfo = bodyType.Split('_');
        string body = bodyInfo[0] + "_" + bodyInfo[1];
        outfitStyle = allOutfits[body][0].OutfitName;
        ChangeBody(allOutfits[body][0]);
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
