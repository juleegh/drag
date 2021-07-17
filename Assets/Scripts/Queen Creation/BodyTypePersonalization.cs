using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BodyTypePersonalization : MonoBehaviour
{
    private static BodyTypePersonalization instance;
    public static BodyTypePersonalization Instance { get { return instance; } }

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

    public void TryToSave()
    {
        if (nameField.text == "")
            return;
        else
        {
            SaveInfo();
            GlobalPlayerManager.Instance.GoToDragging();
        }
    }

    private void SaveInfo()
    {
        Color skin = BodyMeshController.Instance.SkinColor;
        string skinColor = skin.r + "," + skin.g + "," + skin.b;
        PlayerPrefs.SetString("Queen_Skin", skinColor);
        PlayerPrefs.SetString("Queen_Name", nameField.text);
    }
}
