using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitStyle : Accesory
{
    private string codeName;
    public string CodeName { get { return codeName; } }
    private float price;
    public float Price { get { return price; } }

    public OutfitStyle(BodyMesh bodyType)
    {
        codeName = bodyType.OutfitName;
        price = bodyType.Price;
    }
}
