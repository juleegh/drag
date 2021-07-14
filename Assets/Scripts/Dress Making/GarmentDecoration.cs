using UnityEngine;

public class GarmentDecoration : GameData
{
    private string codeName;
    private Vector3 position;
    private Vector3 color;
    private Quaternion rotation;
    private Vector3 scale;
    public string CodeName { get { return codeName; } }
    public Vector3 Position { get { return position; } }
    public Quaternion Rotation { get { return rotation; } }
    public Vector3 Color { get { return color; } }
    public Vector3 Scale { get { return scale; } }

    public void LoadInfo(string code)
    {
        codeName = code;
    }

    public void SetPhysicalInfo(Vector3 pos, Vector3 col, Quaternion rot, Vector3 sca)
    {
        position = pos;
        color = col;
        rotation = rot;
        scale = sca;
    }

    public override void Save(GameDataWriter writer)
    {
        writer.Write(codeName);
        writer.Write(position);
        writer.Write(rotation);
        writer.Write(scale);
        writer.Write(color);
    }

    public override void Load(GameDataReader reader)
    {
        position = reader.ReadVector3();
        rotation = reader.ReadQuaternion();
        scale = reader.ReadVector3();
        color = reader.ReadVector3();
    }
}
