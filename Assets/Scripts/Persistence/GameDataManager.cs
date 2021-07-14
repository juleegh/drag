using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] private GameObject garmentHolder;
    GameDataWriter writer;
    GameDataReader reader;

    string savePath;

    void Awake()
    {
        savePath = Application.persistentDataPath;
        savePath = Path.Combine(Application.persistentDataPath, "saveFile4");
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

    private List<GarmentDecoration> GetDecorations()
    {
        List<GarmentDecoration> decos = new List<GarmentDecoration>();
        GarmentDecoration[] decosInBody = garmentHolder.GetComponentsInChildren<GarmentDecoration>();
        foreach (GarmentDecoration ornament in decosInBody)
        {
            decos.Add(ornament);
        }
        return decos;
    }
}
