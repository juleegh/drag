using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Garment : GameData
{
    private List<GarmentDecoration> GetDecorations()
    {
        List<GarmentDecoration> decos = new List<GarmentDecoration>();
        GarmentDecoration[] decosInBody = GameObject.FindObjectsOfType<GarmentDecoration>();
        foreach (GarmentDecoration ornament in decosInBody)
        {
            decos.Add(ornament);
        }
        return decos;
    }

    public override void Save(GameDataWriter writer)
    {
        List<GarmentDecoration> decorations = GetDecorations();
        writer.Write(decorations.Count);
        foreach (GarmentDecoration ornament in decorations)
        {
            ornament.Save(writer);
        }
    }

    public override void Load(GameDataReader reader)
    {
        int ornaments = reader.ReadInt();
        for (int i = 0; i < ornaments; i++)
        {
            string code = reader.ReadString();

            GameObject ornament = Inventory.Instance.GetEmbelishmentByOrnamentType(code);
            ornament.GetComponent<GarmentDecoration>().Load(reader);
            ornament.transform.SetParent(PosePerformer.Instance.GetClosestBone(ornament.transform.position));

        }
    }
}
