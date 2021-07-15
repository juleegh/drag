using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Face Feature")]
public class FaceFeature : ScriptableObject
{
    [SerializeField] private FacePart facePart;
    [SerializeField] private Sprite sprite;

    public FacePart Location { get { return facePart; } }
    public Sprite Sprite { get { return sprite; } }
}
