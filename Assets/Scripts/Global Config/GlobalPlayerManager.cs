using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using UnityEngine.SceneManagement;

public class GlobalPlayerManager : MonoBehaviour
{
    private static GlobalPlayerManager instance;
    public static GlobalPlayerManager Instance { get { return instance; } }
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private MeshCollider meshCollider;
    [SerializeField] private Transform faceBone;
    public SkinnedMeshRenderer SkinnedMeshRenderer { get { return skinnedMeshRenderer; } }
    public MeshCollider MeshCollider { get { return meshCollider; } }
    public Transform FaceBone { get { return faceBone; } }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (PlayerPrefs.GetString("Queen_Name") == "")
        {
            SceneManager.LoadScene((int)GameFunctions.Queen_Creation);
        }
        else
            SceneManager.LoadScene((int)GameFunctions.Dress_Making);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = Vector3.zero;
    }

    public void GoToDragging()
    {
        SceneManager.LoadScene((int)GameFunctions.Dress_Making);
    }

    public void GoToPerforming()
    {
        SceneManager.LoadScene((int)GameFunctions.Performing);
    }

    private void LoadPlayerProfile()
    {

    }
}
