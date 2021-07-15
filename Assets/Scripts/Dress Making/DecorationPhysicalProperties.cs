using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private BoxCollider boxCollider;
    public SpriteRenderer SpriteRenderer { get { return spriteRenderer; } }
    GarmentDecoration data;

    public void LoadInfo(string code, Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
        data = new GarmentDecoration();
        data.LoadInfo(code);
        ConfigPhysicalInfo();
    }

    private void ConfigPhysicalInfo()
    {
        //boxCollider.size = transform.lossyScale;
        //Debug.LogError(transform.lossyScale);
        data.SetPhysicalInfo(transform.position, VectorFromColor(spriteRenderer.color), transform.rotation, transform.localScale);
    }

    public void LoadFromFile(GameDataReader dataReader)
    {
        data = new GarmentDecoration();
        data.Load(dataReader);
        transform.position = data.Position;
        transform.rotation = data.Rotation;
        transform.localScale = data.Scale;
        spriteRenderer.color = ColorFromVector(data.Color);
    }

    public void Save(GameDataWriter dataWriter)
    {
        data.Save(dataWriter);
    }

    private Vector3 VectorFromColor(Color color)
    {
        return new Vector3(color.r, color.g, color.b);
    }

    private Color ColorFromVector(Vector3 color)
    {
        return new Color(color.x, color.y, color.z, 1);
    }

    public void PreviewColor(bool isPreview)
    {
        if (isPreview)
        {
            Color target = spriteRenderer.color;
            target.a = 0.3f;
            spriteRenderer.color = target;
        }
        else
        {

            Color target = spriteRenderer.color;
            target.a = 1f;
            spriteRenderer.color = target;
        }
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
