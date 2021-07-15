using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour, IRequiredComponent
{
    public static Inventory Instance { get { return instance; } }
    private static Inventory instance;
    public Dictionary<DecorationType, DecorationInfo> decorations;
    [SerializeField] private DecorationsSettings decorationsSettings;
    [SerializeField] private SimpleObjectPool decorationsPool;
    public DecorationSetting CurrentSelected { get { return decorationsSettings.Decorations[current]; } }
    private DecorationType current;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        LoadDecorations();
    }

    private void LoadDecorations()
    {
        decorations = new Dictionary<DecorationType, DecorationInfo>();
        foreach (KeyValuePair<DecorationType, DecorationSetting> decoration in decorationsSettings.Decorations)
        {
            int quantity = 0;
            if (PlayerPrefs.GetInt(decoration.Key.ToString()) != 0)
                quantity = PlayerPrefs.GetInt(decoration.Key.ToString());
            DecorationInfo deco = new DecorationInfo(decoration.Value, quantity);
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
        GameObject newDeco = decorationsPool.GetObject();
        newDeco.GetComponent<Decoration>().LoadInfo(current.ToString(), CurrentSelected.Sprite);
        return newDeco;
    }

    public GameObject GetEmbelishmentByOrnamentType(string code)
    {
        DecorationType decoType = (DecorationType)System.Enum.Parse(typeof(DecorationType), code);
        GameObject decorationPrefab = decorationsPool.GetObject();
        decorationPrefab.GetComponent<Decoration>().LoadInfo(code, decorationsSettings.Decorations[decoType].Sprite);
        return decorationPrefab;
    }
}
