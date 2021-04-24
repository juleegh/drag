using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get { return instance; } }
    public static Inventory instance;
    public Dictionary<string, Decoration> decorations;
    [SerializeField] private DecorationsSettings decorationsSettings;
    public DecorationSetting CurrentSelected { get { return decorationsSettings.Decorations[current]; } }
    private string current = "";
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadDecorations();
        }
        else
            Destroy(this.gameObject);
    }

    private void LoadDecorations()
    {
        decorations = new Dictionary<string, Decoration>();
        foreach (KeyValuePair<string, DecorationSetting> decoration in decorationsSettings.Decorations)
        {
            int quantity = 0;
            if (PlayerPrefs.GetInt(decoration.Key) != 0)
                quantity = PlayerPrefs.GetInt(decoration.Key);
            Decoration deco = new Decoration(decoration.Value.DecoName, decoration.Key, quantity, decoration.Value.Price);
            decorations.Add(decoration.Key, deco);
            current = decoration.Key;
        }
    }

    public GameObject GetOneDecoration()
    {
        GameObject newDeco = Instantiate(CurrentSelected.Prefab);
        newDeco.GetComponent<GarmentDecoration>().LoadInfo(current);
        return newDeco;
    }

    public GameObject GetPrefabByOrnamentType(string code)
    {
        return decorationsSettings.Decorations[code].Prefab;
    }
}
