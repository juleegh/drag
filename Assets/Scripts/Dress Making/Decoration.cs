using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration
{
    private DecorationType decoType;
    private int quantity;
    private int price;
    private Sprite sprite;
    public int Price { get { return price; } }
    public int Quantity { get { return quantity; } }
    public string CodeName { get { return decoType.ToString(); } }
    public Sprite Sprite { get { return sprite; } }
    public DecorationType DecoType { get { return decoType; } }

    public Decoration(DecorationSetting deco, int q)
    {
        decoType = deco.DecoType;
        quantity = q;
        price = deco.Price;
        sprite = deco.Sprite;
    }
}
