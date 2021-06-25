using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmbelishmentButton : MonoBehaviour
{
    [SerializeField] private Image preview;
    [SerializeField] private Button button;
    private Decoration decoration;

    public void Initialize(Decoration deco)
    {
        decoration = deco;
        preview.sprite = decoration.Sprite;
        button.onClick.AddListener(DecorationSelected);

    }

    private void DecorationSelected()
    {
        Inventory.Instance.ChangeSelected(decoration.DecoType);
    }
}
