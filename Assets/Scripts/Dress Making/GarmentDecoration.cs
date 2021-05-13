using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] private SpriteRenderer spriteRenderer;
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }

    public void LoadInfo(string code, Sprite sprite)
    {
        codeName = code;
        spriteRenderer.sprite = sprite;
    }

    public void SetColor(Color newColor)
    {
        spriteRenderer.color = newColor;
    }

    public override void Save(GameDataWriter writer)
    {
        writer.Write(codeName);
        writer.Write(transform.localPosition);
        writer.Write(transform.localRotation);
        writer.Write(transform.localScale);
        writer.Write(GetColorVector());
    }

    public override void Load(GameDataReader reader)
    {
        transform.localPosition = reader.ReadVector3();
        transform.localRotation = reader.ReadQuaternion();
        transform.localScale = reader.ReadVector3();
        spriteRenderer.color = LoadColorVector(reader.ReadVector3());
    }

    private Color LoadColorVector(Vector3 vector)
    {
        return new Color(vector.x, vector.y, vector.z, 1f);
    }

    private Vector3 GetColorVector()
    {
        return new Vector3(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b);
    }
}
