using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitStyle : Accesory
{
    private string codeName;
    public string CodeName { get { return codeName; } }

    public OutfitStyle(BodyType bodyType)
    {
        codeName = bodyType.OutfitName;
    }
}
