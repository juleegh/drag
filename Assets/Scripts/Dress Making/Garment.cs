using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Garment : GameData
{
    private List<GarmentDecoration> GetDecorations()
    {
        List<GarmentDecoration> decos = new List<GarmentDecoration>();
        foreach (Transform ornament in transform)
        {
            if (ornament.GetComponent<GarmentDecoration>() != null)
                decos.Add(ornament.GetComponent<GarmentDecoration>());
        }
        return decos;
    }

    public override void Save(GameDataWriter writer)
    {
        List<GarmentDecoration> decorations = GetDecorations();
        writer.Write(decorations.Count);
        foreach (GarmentDecoration ornament in decorations)
        {
            ornament.GetComponent<GarmentDecoration>().Save(writer);
        }
    }

    public override void Load(GameDataReader reader)
    {
        int ornaments = reader.ReadInt();
        for (int i = 0; i < ornaments; i++)
        {
            string code = reader.ReadString();

            GameObject ornament = Instantiate(Inventory.Instance.GetPrefabByOrnamentType(code));
            ornament.transform.SetParent(transform);
            ornament.GetComponent<GarmentDecoration>().Load(reader);

        }
    }
}
