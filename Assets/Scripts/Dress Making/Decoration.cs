using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoration : Accesory
{
    private DecorationType decoType;
    private int quantity;
    private int price;
    public int Price { get { return price; } }
    public int Quantity { get { return quantity; } }
    public string CodeName { get { return decoType.ToString(); } }
    public DecorationType DecoType { get { return decoType; } }

    public Decoration(DecorationSetting deco, int q)
    {
        decoType = deco.DecoType;
        quantity = q;
        price = deco.Price;
        sprite = deco.Sprite;
    }
}
