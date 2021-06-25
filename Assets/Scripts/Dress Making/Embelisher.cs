using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embelisher : MonoBehaviour
{
    private static Embelisher instance;
    public static Embelisher Instance { get { return instance; } }

    public Color CurrentColor;
    private Vector3 CurrentScale;
    private float CurrentRotation;
    private bool mirrored = false;

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
        preview.gameObject.SetActive(false);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer == LayerMask.NameToLayer("Manniquin"))
            {
                GameObject decoration = Inventory.Instance.GetOneDecoration();
                CreateObjectToHit(decoration, hit);
                decoration.transform.SetParent(PosePerformer.Instance.GetClosestBone(hit.point));
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
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            mirrored = !mirrored;
        }
        else
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.layer == LayerMask.NameToLayer("Manniquin"))
            {
                preview.gameObject.SetActive(true);
                CreateObjectToHit(preview, hit);
                preview.GetComponent<GarmentDecoration>().LoadInfo(Inventory.Instance.CurrentSelected.CodeName, Inventory.Instance.CurrentSelected.Sprite);
            }
        }

    }

    private void CreateObjectToHit(GameObject decoration, RaycastHit hit)
    {
        decoration.transform.position = hit.point + hit.normal * 0.01f;
        decoration.transform.rotation = Quaternion.LookRotation(hit.normal);
        decoration.transform.Rotate(Vector3.forward * CurrentRotation, Space.Self);
        if (mirrored)
            decoration.transform.Rotate(Vector3.up * 180, Space.Self);
        decoration.transform.localScale = CurrentScale;
        decoration.GetComponent<GarmentDecoration>().SetColor(CurrentColor);
    }
}
