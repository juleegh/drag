using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decoration Settings")]
public class DecorationSetting : ScriptableObject
{
    [SerializeField] private string decoName;
    [SerializeField] private int price;
    [SerializeField] private Sprite sprite;

    public string DecoName { get { return decoName; } }
    public int Price { get { return price; } }
    public Sprite Sprite { get { return sprite; } }
}
