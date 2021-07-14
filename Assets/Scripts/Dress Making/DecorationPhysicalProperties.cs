using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationPhysicalProperties : MonoBehaviour
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
        Load();
    }

    private void Load()
    {
        //boxCollider.size = transform.lossyScale;
        //Debug.LogError(transform.lossyScale);
        data.SetPhysicalInfo(transform.position, GetColorVector(spriteRenderer.color), transform.rotation, transform.localScale);
    }

    private Vector3 GetColorVector(Color color)
    {
        return new Vector3(color.r, color.g, color.b);
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
