using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using System;
using UnityEngine.SceneManagement;

public class GlobalPlayerManager : MonoBehaviour, GlobalComponent
{
    private static GlobalPlayerManager instance;
    public static GlobalPlayerManager Instance { get { return instance; } }
    [SerializeField] private GameObject body;
    [SerializeField] private Transform faceBone;
    public GameObject Body { get { return body; } }
    public Transform FaceBone { get { return faceBone; } }

    public void ConfigureRequiredComponent()
    {
        //PlayerPrefs.DeleteAll();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        GameEventsManager.Instance.AddActionToEvent(GameEvent.DependenciesLoaded, CheckEntry);
    }

    void CheckEntry()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        if (PlayerPrefs.GetString("Queen_Name") == "")
        {
            SceneManager.LoadScene((int)GameFunctions.Queen_Creation);
        }
        else
            SceneManager.LoadScene((int)GameFunctions.Lobby);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = Vector3.zero;
    }

    public void GoToDragging()
    {
        SceneManager.LoadScene((int)GameFunctions.Dress_Making);
        GameEventsManager.Instance.Notify(GameEvent.EnteredDraggingRoom);
    }

    public void GoToPerforming()
    {
        SceneManager.LoadScene((int)GameFunctions.Performing);
    }

    public void GoToLobby()
    {
        SceneManager.LoadScene((int)GameFunctions.Lobby);
    }
}
