using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration
{
    private string decoName;
    private string codeName;
    private int quantity;
    private int price;
    private Sprite sprite;
    public string DecoName { get { return decoName; } }
    public int Price { get { return price; } }
    public int Quantity { get { return quantity; } }
    public string CodeName { get { return codeName; } }
    public Sprite Sprite { get { return sprite; } }

    public Decoration(DecorationSetting deco, string codeN, int q)
    {
        decoName = deco.DecoName;
        codeName = codeN;
        quantity = q;
        price = deco.Price;
        sprite = deco.Sprite;
    }
}
