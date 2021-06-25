using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decoration Settings")]
public class DecorationSetting : ScriptableObject
{
    [SerializeField] private int price;
    [SerializeField] private Sprite sprite;
    [SerializeField] private DecorationType decorationType;

    public string CodeName { get { return decorationType.ToString(); } }
    public int Price { get { return price; } }
    public Sprite Sprite { get { return sprite; } }
    public DecorationType DecoType { get { return decorationType; } }
}
