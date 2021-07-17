using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

public class MakeupSelection : MonoBehaviour, GlobalComponent
{
    public static MakeupSelection Instance { get { return instance; } }
    private static MakeupSelection instance;

    [SerializeField] private List<FaceFeature> makeupConfigs;
    private Dictionary<FacePart, List<FaceFeature>> makeupOptions;
    private Dictionary<FacePart, Color> makeupColor;
    private Dictionary<FacePart, int> indexes;
    private FacePart[] faceParts;
    public FacePart[] FaceParts { get { return faceParts; } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
        LoadMakeup();
    }

    public FaceFeature GetNext(FacePart facePart)
    {
        indexes[facePart]++;
        if (indexes[facePart] >= makeupOptions[facePart].Count)
            indexes[facePart] = 0;

        return makeupOptions[facePart][indexes[facePart]];
    }

    public FaceFeature GetPrevious(FacePart facePart)
    {
        indexes[facePart]--;
        if (indexes[facePart] < 0)
            indexes[facePart] = makeupOptions[facePart].Count - 1;

        return makeupOptions[facePart][indexes[facePart]];
    }

    public void UpdatedColor(FacePart part, Color color)
    {
        makeupColor[part] = color;
    }

    public FaceFeature GetCurrent(FacePart facePart)
    {
        return makeupOptions[facePart][indexes[facePart]];
    }

    public Color GetCurrentColor(FacePart facePart)
    {
        return makeupColor[facePart];
    }

    private void LoadMakeup()
    {
        makeupOptions = new Dictionary<FacePart, List<FaceFeature>>();
        foreach (FaceFeature feature in makeupConfigs)
        {
            if (!makeupOptions.ContainsKey(feature.Location))
                makeupOptions.Add(feature.Location, new List<FaceFeature>());

            makeupOptions[feature.Location].Add(feature);
        }

        indexes = new Dictionary<FacePart, int>();
        makeupColor = new Dictionary<FacePart, Color>();
        foreach (FacePart makeup in makeupOptions.Keys)
        {
            indexes.Add(makeup, 0);
            makeupColor.Add(makeup, Color.white);
        }

        faceParts = (FacePart[])System.Enum.GetValues(typeof(FacePart));
    }

    public void SetMakeupPart(FacePart part, string partType)
    {
        int index = 0;
        foreach (FaceFeature feature in makeupOptions[part])
        {
            if (feature.name.Equals(partType))
                break;
            index++;
        }
        indexes[part] = index;
        MakeupManager.Instance.SelectedFeature(makeupOptions[part][indexes[part]]);
    }

    public void SetMakeupColor(FacePart part, Color color)
    {
        makeupColor[part] = color;
        MakeupManager.Instance.SelectedColor(part, color);
    }
}
