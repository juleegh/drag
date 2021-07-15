using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeupManager : MonoBehaviour, RequiredComponent
{
    private static MakeupManager instance;
    public static MakeupManager Instance { get { return instance; } }

    [SerializeField] private SpriteRenderer brows;
    [SerializeField] private SpriteRenderer shadows;
    [SerializeField] private SpriteRenderer lips;

    public void ConfigureRequiredComponent()
    {
        instance = this;
    }

    public void SelectedFeature(FaceFeature feature)
    {
        switch (feature.Location)
        {
            case FacePart.Eyebrow:
                brows.sprite = feature.Sprite;
                break;
            case FacePart.Eyeshadow:
                shadows.sprite = feature.Sprite;
                break;
            case FacePart.Lips:
                lips.sprite = feature.Sprite;
                break;
        }
    }

    public void SelectedColor(FacePart feature, Color color)
    {
        switch (feature)
        {
            case FacePart.Eyebrow:
                brows.color = color;
                break;
            case FacePart.Eyeshadow:
                shadows.color = color;
                break;
            case FacePart.Lips:
                lips.color = color;
                break;
        }
    }
}
