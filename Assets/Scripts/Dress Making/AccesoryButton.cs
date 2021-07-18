using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AccesoryButton : MonoBehaviour
{
    [SerializeField] protected Image preview;
    [SerializeField] protected Button button;
    protected Accesory accesory;

    public virtual void Initialize(Accesory acc)
    {
        accesory = acc;
        preview.sprite = accesory.Sprite;
        button.onClick.AddListener(DecorationSelected);

        DecorationInfo decoration = accesory as DecorationInfo;
        if (decoration != null)
        {
            RectTransform rect = preview.GetComponent<RectTransform>();
            float dimension = SpriteAspectRatio.GetDimension(accesory.Sprite);
            float aspectRatio = SpriteAspectRatio.GetAspectRatio(dimension);
            float distanceFromEdges = 1 - aspectRatio;
            rect.anchorMin = new Vector2(distanceFromEdges / 2, distanceFromEdges / 2);
            rect.anchorMax = new Vector2(1 - distanceFromEdges / 2, 1 - distanceFromEdges / 2);
        }

    }

    protected virtual void DecorationSelected()
    {
        DecorationInfo decoration = accesory as DecorationInfo;
        if (decoration != null) Inventory.Instance.ChangeSelected(decoration.DecoType);

        Wig wig = accesory as Wig;
        if (wig != null) WigSelection.Instance.ChangeSelected(wig.WigType);
    }
}