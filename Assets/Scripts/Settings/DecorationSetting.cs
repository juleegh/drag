using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Decoration Settings")]
public class DecorationSetting : ScriptableObject
{
    [SerializeField] private string decoName;
    [SerializeField] private int price;
    [SerializeField] private GameObject prefab;

    public string DecoName { get { return decoName; } }
    public int Price { get { return price; } }
    public GameObject Prefab { get { return prefab; } }
}
