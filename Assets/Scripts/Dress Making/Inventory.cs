using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get { return instance; } }
    private static Inventory instance;
    public Dictionary<string, Decoration> decorations;
    [SerializeField] private DecorationsSettings decorationsSettings;
    [SerializeField] private GameObject decorationPrefab;
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
            Decoration deco = new Decoration(decoration.Value, decoration.Key, quantity);
            decorations.Add(decoration.Key, deco);
            current = decoration.Key;
        }
    }

    public void ChangeSelected(string selected)
    {
        current = selected;
    }

    public GameObject GetOneDecoration()
    {
        GameObject newDeco = Instantiate(decorationPrefab);
        newDeco.GetComponent<GarmentDecoration>().LoadInfo(current, CurrentSelected.Sprite);
        return newDeco;
    }

    public GameObject GetPrefabByOrnamentType(string code)
    {
        decorationPrefab.GetComponent<GarmentDecoration>().SpriteRenderer.sprite = decorationsSettings.Decorations[code].Sprite;
        return decorationPrefab;
    }
}
