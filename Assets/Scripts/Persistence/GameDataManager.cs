using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour, RequiredComponent
{
    private GameObject garmentHolder;
    GameDataWriter writer;
    GameDataReader reader;

    string savePath;

    public void ConfigureRequiredComponent()
    {
        OutfitEventsManager.Instance.AddActionToEvent(OutfitEvent.DependenciesLoaded, Initialize);
    }

    private void Initialize()
    {
        savePath = Application.persistentDataPath;
        savePath = Path.Combine(Application.persistentDataPath, "saveFile4");
        garmentHolder = GlobalPlayerManager.Instance.Body;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            Save();
        if (Input.GetKeyDown(KeyCode.P))
            Load();
    }

    void Save()
    {
        BinaryWriter bWriter = new BinaryWriter(File.Open(savePath, FileMode.Create));
        writer = new GameDataWriter(bWriter);
        Garment garment = new Garment();
        garment.SetDecorations(GetDecorations());
        garment.Save(writer);
    }

    void Load()
    {
        BinaryReader bWriter = new BinaryReader(File.Open(savePath, FileMode.Open));
        reader = new GameDataReader(bWriter);
        Garment garment = new Garment();
        garment.Load(reader);
    }

    private List<Decoration> GetDecorations()
    {
        List<Decoration> decos = new List<Decoration>();
        Decoration[] decosInBody = garmentHolder.GetComponentsInChildren<Decoration>();
        foreach (Decoration ornament in decosInBody)
        {
            decos.Add(ornament);
        }
        return decos;
    }
}
