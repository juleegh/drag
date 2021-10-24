using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitQuantityRule : OutfitRule
{
    [SerializeField] private int quantityRequired;

    public override void Evaluate(List<Decoration> decorations)
    {
        int currentQuantity = decorations.Count;
        progress = (float)currentQuantity / (float)quantityRequired;
    }
}
