using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CharacterOutfitManager : MonoBehaviour, GlobalComponent
{
    private static CharacterOutfitManager instance;
    public static CharacterOutfitManager Instance { get { return instance; } }

    private GameObject garmentHolder;
    GameDataWriter writer;
    GameDataReader reader;

    string savePath;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        GameEventsManager.Instance.AddActionToEvent(GameEvent.DependenciesLoaded, Initialize);
        GameEventsManager.Instance.AddActionToEvent(GameEvent.EnteredDraggingRoom, ClearDecorations);
        GameEventsManager.Instance.AddActionToEvent(GameEvent.OutfitCanceled, ClearDecorations);
    }

    private void Initialize()
    {
        garmentHolder = GlobalPlayerManager.Instance.Body;
    }

    public List<string> GetSavedOutfits()
    {
        int quantity = PlayerPrefs.GetInt("Outfit_Count", 0);
        List<string> outfitNames = new List<string>();
        for (int i = 0; i < quantity; i++)
        {
            outfitNames.Add(PlayerPrefs.GetString("Outfit" + i));
        }
        return outfitNames;
    }

    public void Save(string outfitName)
    {
        int outfits = PlayerPrefs.GetInt("Outfit_Count", 0);
        PlayerPrefs.SetString("Outfit" + outfits, outfitName);
        outfits++;
        PlayerPrefs.SetInt("Outfit_Count", outfits);

        savePath = Path.Combine(Application.persistentDataPath, outfitName);

        BinaryWriter bWriter = new BinaryWriter(File.Open(savePath, FileMode.Create));
        writer = new GameDataWriter(bWriter);
        Garment garment = new Garment();
        garment.SetDecorations(GetDecorations());
        garment.Save(writer);
        bWriter.Close();
    }

    public void Load(string outfitName)
    {
        ClearDecorations();
        savePath = Path.Combine(Application.persistentDataPath, outfitName);
        BinaryReader bWriter = new BinaryReader(File.Open(savePath, FileMode.Open));
        reader = new GameDataReader(bWriter);
        Garment garment = new Garment();
        garment.Load(reader);
        bWriter.Close();
    }

    public List<Decoration> GetDecorations()
    {
        List<Decoration> decos = new List<Decoration>();
        Decoration[] decosInBody = garmentHolder.GetComponentsInChildren<Decoration>();
        foreach (Decoration ornament in decosInBody)
        {
            decos.Add(ornament);
        }
        return decos;
    }

    private void ClearDecorations()
    {
        Decoration[] decosInBody = garmentHolder.GetComponentsInChildren<Decoration>();
        foreach (Decoration ornament in decosInBody)
        {
            Inventory.Instance.ReturnDecoration(ornament.gameObject);
        }
    }
}
