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

    }

    protected virtual void DecorationSelected()
    {
        Decoration decoration = accesory as Decoration;
        if (decoration != null) Inventory.Instance.ChangeSelected(decoration.DecoType);

        Wig wig = accesory as Wig;
        if (wig != null) WigSelection.Instance.ChangeSelected(wig.WigType);
    }
}