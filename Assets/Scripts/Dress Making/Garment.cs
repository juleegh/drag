using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Garment : GameData
{
    private List<GarmentDecoration> decorations;

    public void SetDecorations(List<GarmentDecoration> decos)
    {
        decorations = decos;
    }

    public override void Save(GameDataWriter writer)
    {
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
