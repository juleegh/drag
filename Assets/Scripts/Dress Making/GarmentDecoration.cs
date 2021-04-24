using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarmentDecoration : GameData
{
    private string codeName;
    private Vector3 position;
    private Quaternion rotation;
    private Vector3 scale;
    public string CodeName { get { return codeName; } }
    public Vector3 Position { get { return position; } }
    public Quaternion Rotation { get { return rotation; } }
    public Vector3 Scale { get { return scale; } }

    public void LoadInfo(string code)
    {
        codeName = code;
    }

    public override void Save(GameDataWriter writer)
    {
        writer.Write(codeName);
        writer.Write(transform.localPosition);
        writer.Write(transform.localRotation);
        writer.Write(transform.localScale);
    }

    public override void Load(GameDataReader reader)
    {
        transform.localPosition = reader.ReadVector3();
        transform.localRotation = reader.ReadQuaternion();
        transform.localScale = reader.ReadVector3();
    }
}
