using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Garment : GameData
{
    private List<Decoration> decorations;

    public void SetDecorations(List<Decoration> decos)
    {
        decorations = decos;
    }

    public override void Save(GameDataWriter writer)
    {
        writer.Write(ColorConversion.VectorFromColor(BodyMeshController.Instance.ClothesColor));
        writer.Write(decorations.Count);
        foreach (Decoration ornament in decorations)
        {
            ornament.Save(writer);
        }

        writer.Write(WigFitter.Instance.CurrentWig);
        writer.Write(ColorConversion.VectorFromColor(WigFitter.Instance.CurrentColor));

        foreach (FacePart facepart in MakeupSelection.Instance.FaceParts)
        {
            writer.Write(MakeupSelection.Instance.GetCurrent(facepart).name);
            writer.Write(ColorConversion.VectorFromColor(MakeupSelection.Instance.GetCurrentColor(facepart)));
        }
    }

    public override void Load(GameDataReader reader)
    {
        Color clothesColor = ColorConversion.ColorFromVector(reader.ReadVector3());
        BodyMeshController.Instance.ChangeClothesColor(clothesColor);

        int ornaments = reader.ReadInt();
        for (int i = 0; i < ornaments; i++)
        {
            string code = reader.ReadString();

            GameObject ornament = Inventory.Instance.GetEmbelishmentByOrnamentType(code);
            ornament.GetComponent<Decoration>().LoadFromFile(reader);
            ornament.transform.SetParent(PosePerformer.Instance.GetClosestBone(ornament.transform.position));
        }
        string wig = reader.ReadString();
        WigSelection.Instance.ChangeSelected(wig);
        Color wigColor = ColorConversion.ColorFromVector(reader.ReadVector3());
        WigFitter.Instance.SetCurrentColor(wigColor);

        foreach (FacePart facepart in MakeupSelection.Instance.FaceParts)
        {
            string faceFeature = reader.ReadString();
            MakeupSelection.Instance.SetMakeupPart(facepart, faceFeature);
            Vector3 color = reader.ReadVector3();
            MakeupSelection.Instance.SetMakeupColor(facepart, ColorConversion.ColorFromVector(color));
        }
    }
}
