using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameDataManager : MonoBehaviour
{
    [SerializeField] private Garment garment;
    GameDataWriter writer;
    GameDataReader reader;

    string savePath;

    void Awake()
    {
        savePath = Application.persistentDataPath;
        savePath = Path.Combine(Application.persistentDataPath, "saveFile3");
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
        garment.Save(writer);
    }

    void Load()
    {
        BinaryReader bWriter = new BinaryReader(File.Open(savePath, FileMode.Open));
        reader = new GameDataReader(bWriter);
        garment.Load(reader);
    }
}
