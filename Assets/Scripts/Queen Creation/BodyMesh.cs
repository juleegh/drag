using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyMesh : ScriptableObject
{
    [SerializeField] private Mesh bodyMesh;
    public Mesh Mesh { get { return bodyMesh; } }

    public string OutfitName
    {
        get
        {
            string[] info = name.Split('_');
            return info[2];
        }
    }
}
