using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMesh : ScriptableObject
{
    [SerializeField] private Mesh bodyMesh;
    [SerializeField] private float price;
    public Mesh Mesh { get { return bodyMesh; } }
    public float Price { get { return price; } }

    public string OutfitName
    {
        get
        {
            string[] info = name.Split('_');
            return info[2];
        }
    }
}
