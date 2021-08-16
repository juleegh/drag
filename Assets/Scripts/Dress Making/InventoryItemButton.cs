using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemButton : AccesoryButton
{
    private Color itemColor;

    public void SetItemColor(Color selectedColor)
    {
        itemColor = selectedColor;
        preview.color = itemColor;
    }

    public override void Initialize(Accesory acc)
    {
        accesory = acc;
        preview.sprite = accesory.Sprite;
        button.onClick.AddListener(DecorationSelected);
    }

    protected override void DecorationSelected()
    {
        Wig wig = accesory as Wig;
        if (wig != null)
        {
            WigSelection.Instance.ChangeSelected(wig.WigType);
            WigFitter.Instance.SetCurrentColor(itemColor);
        }

        OutfitStyle outfit = accesory as OutfitStyle;
        if (outfit != null)
        {
            BodyMeshController.Instance.ChangeOutfit(outfit.CodeName);
            BodyMeshController.Instance.ChangeClothesColor(itemColor);
        }
    }
}
