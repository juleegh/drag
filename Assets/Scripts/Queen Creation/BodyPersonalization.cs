using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyPersonalization : MonoBehaviour
{
    private static BodyPersonalization instance;
    public static BodyPersonalization Instance { get { return instance; } }

    [SerializeField] private SkinnedMeshRenderer bodyMesh;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangedBody(BodyType bodyType)
    {
        Mesh meshInstance = Instantiate(bodyType.Mesh);
        bodyMesh.sharedMesh = meshInstance;
    }

    public void ChangeColor(SkinType skinType)
    {
        bodyMesh.material.SetTexture("_MainTex", skinType.SkinMaterial);
    }
}
