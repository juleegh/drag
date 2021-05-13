using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embelisher : MonoBehaviour
{
    private static Embelisher instance;
    public static Embelisher Instance { get { return instance; } }

    public Color CurrentColor;
    public Vector3 CurrentScale;
    public float CurrentRotation;

    private GameObject preview;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
        preview = Inventory.Instance.GetOneDecoration();
        CurrentScale = Vector3.one;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject decoration = Inventory.Instance.GetOneDecoration();
                decoration.transform.position = hit.point;
                decoration.transform.localScale = CurrentScale;
                decoration.transform.rotation = Quaternion.LookRotation(hit.normal);
                decoration.transform.Rotate(Vector3.forward * CurrentRotation, Space.Self);
                decoration.transform.SetParent(hit.transform);
                decoration.GetComponent<GarmentDecoration>().SetColor(CurrentColor);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            CurrentRotation -= 2f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            CurrentRotation += 2f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (CurrentScale.magnitude < 5)
                CurrentScale *= 1.1f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (CurrentScale.magnitude > 1.1f)
                CurrentScale *= 0.9f;
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                preview.transform.position = hit.point;
                preview.transform.rotation = Quaternion.LookRotation(hit.normal);
                preview.transform.Rotate(Vector3.forward * CurrentRotation, Space.Self);
                preview.transform.localScale = CurrentScale;
                preview.GetComponent<GarmentDecoration>().SetColor(CurrentColor);
            }
        }
    }
}
