using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin Type")]
public class SkinType : ScriptableObject
{
    [SerializeField] private Texture skinMaterial;
    public Texture SkinMaterial { get { return skinMaterial; } }
}
