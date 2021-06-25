using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get { return instance; } }
    private static Inventory instance;
    public Dictionary<DecorationType, Decoration> decorations;
    [SerializeField] private DecorationsSettings decorationsSettings;
    [SerializeField] private GameObject decorationPrefab;
    public DecorationSetting CurrentSelected { get { return decorationsSettings.Decorations[current]; } }
    private DecorationType current;
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
        decorations = new Dictionary<DecorationType, Decoration>();
        foreach (KeyValuePair<DecorationType, DecorationSetting> decoration in decorationsSettings.Decorations)
        {
            int quantity = 0;
            if (PlayerPrefs.GetInt(decoration.Key.ToString()) != 0)
                quantity = PlayerPrefs.GetInt(decoration.Key.ToString());
            Decoration deco = new Decoration(decoration.Value, quantity);
            decorations.Add(decoration.Key, deco);
            current = decoration.Key;
        }
    }

    public void ChangeSelected(DecorationType selected)
    {
        current = selected;
    }

    public GameObject GetOneDecoration()
    {
        GameObject newDeco = Instantiate(decorationPrefab);
        newDeco.GetComponent<GarmentDecoration>().LoadInfo(current.ToString(), CurrentSelected.Sprite);
        return newDeco;
    }

    public GameObject GetPrefabByOrnamentType(string code)
    {
        DecorationType decoType = (DecorationType)System.Enum.Parse(typeof(DecorationType), code);
        decorationPrefab.GetComponent<GarmentDecoration>().SpriteRenderer.sprite = decorationsSettings.Decorations[decoType].Sprite;
        return decorationPrefab;
    }
}
