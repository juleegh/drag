using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wig")]
public class WigConfig : ScriptableObject
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int price;

    public Mesh Mesh { get { return mesh; } }
    public Sprite Sprite { get { return sprite; } }
    public int Price { get { return price; } }
}
