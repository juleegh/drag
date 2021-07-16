using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMeshController : MonoBehaviour, GlobalComponent
{
    private static BodyMeshController instance;
    public static BodyMeshController Instance { get { return instance; } }

    [SerializeField] private Material meshMaterial;
    [SerializeField] private Color baseSkinColor;
    [SerializeField] private Color baseClothesColor;
    private Color skinColor;
    private Color clothesColor;
    public Color SkinColor { get { return skinColor; } }
    public Color ClothesColor { get { return clothesColor; } }

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
    }

    private void CheckPlayerSettings()
    {
        if (PlayerPrefs.GetString("Queen_Skin") != "")
        {
            string skin = PlayerPrefs.GetString("Queen_Skin");
            string[] values = skin.Split(',');
            int r = int.Parse(values[0]);
            int g = int.Parse(values[1]);
            int b = int.Parse(values[2]);
            Color skinColor = new Color(r, g, b, 1f);
            ChangeSkinColor(skinColor);
        }
        else
        {
            ChangeSkinColor(baseSkinColor);
            ChangeClothesColor(baseClothesColor);
        }

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
