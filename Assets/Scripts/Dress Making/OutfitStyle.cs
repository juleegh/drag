using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitStyle : Accesory
{
    private string codeName;
    public string CodeName { get { return codeName; } }

    public OutfitStyle(BodyMesh bodyType)
    {
        codeName = bodyType.OutfitName;
    }
}
