using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ProductButton : AccesoryButton
{
    public override void Initialize(Accesory acc)
    {
        accesory = acc;
        preview.sprite = accesory.Sprite;
        button.onClick.AddListener(DecorationSelected);

        DecorationInfo decoration = accesory as DecorationInfo;
        if (decoration != null)
        {
            RectTransform rect = preview.GetComponent<RectTransform>();
            float aspectRatio = AspectRatioUtil.GetSpriteAspectRatio(accesory.Sprite);
            float distanceFromEdges = 1 - aspectRatio;
            rect.anchorMin = new Vector2(distanceFromEdges / 2, distanceFromEdges / 2);
            rect.anchorMax = new Vector2(1 - distanceFromEdges / 2, 1 - distanceFromEdges / 2);
        }

    }

    protected override void DecorationSelected()
    {
        DecorationInfo decoration = accesory as DecorationInfo;
        if (decoration != null) EmbelishmentsPurchaser.Instance.DecorationSelected(decoration);

        Wig wig = accesory as Wig;
        if (wig != null) WigPurchaser.Instance.WigSelected(wig);

        OutfitStyle outfit = accesory as OutfitStyle;
        if (outfit != null) GarmentPurchaser.Instance.GarmentSelected(outfit);
    }
}