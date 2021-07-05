using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Body Type")]
public class BodyType : ScriptableObject
{
    [SerializeField] private Mesh bodyMesh;
    public Mesh Mesh { get { return bodyMesh; } }
}
