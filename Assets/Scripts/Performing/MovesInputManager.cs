using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesInputManager : MonoBehaviour
{
    public static MovesInputManager Instance { get { return instance; } }
    private static MovesInputManager instance;

    [SerializeField] private KeyCode a;
    [SerializeField] private KeyCode b;
    [SerializeField] private KeyCode x;
    [SerializeField] private KeyCode y;

    public KeyCode A { get { return a; } }
    public KeyCode B { get { return b; } }
    public KeyCode X { get { return x; } }
    public KeyCode Y { get { return y; } }

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }
}
