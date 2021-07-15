using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wig : Accesory
{
    private string codeName;
    private WigType wigType;
    private int price;
    public int Price { get { return price; } }
    public string CodeName { get { return codeName; } }
    public WigType WigType { get { return wigType; } }

    public Wig(WigConfig wig, WigType type)
    {
        codeName = wig.name;
        wigType = type;
        price = wig.Price;
        sprite = wig.Sprite;
    }
}
