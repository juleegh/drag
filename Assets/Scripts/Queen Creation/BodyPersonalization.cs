using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BodyPersonalization : MonoBehaviour
{
    private static BodyPersonalization instance;
    public static BodyPersonalization Instance { get { return instance; } }

    private SkinnedMeshRenderer bodyMesh { get { return GlobalPlayerManager.Instance.SkinnedMeshRenderer; } }
    private MeshCollider colliderMesh { get { return GlobalPlayerManager.Instance.MeshCollider; } }
    [SerializeField] private Button readyButton;
    [SerializeField] private TMP_InputField nameField;

    void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        readyButton.onClick.AddListener(TryToSave);
    }

    public void ChangedBody(BodyType bodyType)
    {
        Mesh meshInstance = Instantiate(bodyType.Mesh);
        bodyMesh.sharedMesh = meshInstance;
        colliderMesh.sharedMesh = meshInstance;
    }

    public void ChangeColor(SkinType skinType)
    {
        bodyMesh.material.SetTexture("_MainTex", skinType.SkinMaterial);
    }

    public void TryToSave()
    {
        if (nameField.text == "")
            return;
        else
            GlobalPlayerManager.Instance.GoToDragging();
    }
}
