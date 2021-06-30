using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embelisher : ColorPicking
{
    private static Embelisher instance;
    public static Embelisher Instance { get { return instance; } }
    public EmbelishingVariables EmbelishingVariables;

    private GameObject preview;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            EmbelishingVariables = new EmbelishingVariables();
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    void Start()
    {
        preview = Inventory.Instance.GetOneDecoration();
        EmbelishingVariables.CurrentScale = Vector3.one;
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
                CheckRandoms();
                decoration.transform.SetParent(PosePerformer.Instance.GetClosestBone(hit.point));
            }
        }
        else if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            EmbelishingVariables.CurrentRotation -= 2f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            EmbelishingVariables.CurrentRotation += 2f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            if (EmbelishingVariables.CurrentScale.magnitude < 5)
                EmbelishingVariables.CurrentScale *= 1.1f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            if (EmbelishingVariables.CurrentScale.magnitude > 1.1f)
                EmbelishingVariables.CurrentScale *= 0.9f;
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            EmbelishingVariables.mirrored = !EmbelishingVariables.mirrored;
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
        decoration.transform.Rotate(Vector3.forward * EmbelishingVariables.Rotation, Space.Self);
        if (EmbelishingVariables.mirrored)
            decoration.transform.Rotate(Vector3.up * 180, Space.Self);
        decoration.transform.localScale = EmbelishingVariables.Scale;
        decoration.GetComponent<GarmentDecoration>().SetColor(GetTempColor());
    }

    private Color GetTempColor()
    {
        float h = 0; float s = 0; float v = 0;
        Color.RGBToHSV(currentColor, out h, out s, out v);
        h = (h + EmbelishingVariables.RandomColorVariation.x);
        s = (s + EmbelishingVariables.RandomColorVariation.y);
        v = (v + EmbelishingVariables.RandomColorVariation.z);
        h = Mathf.Abs(h);
        s = Mathf.Abs(s);
        v = Mathf.Abs(v);
        h = Mathf.Clamp(h, 0, 1);
        s = Mathf.Clamp(s, 0, 1);
        v = Mathf.Clamp(v, 0, 1);
        return Color.HSVToRGB(h, s, v);
    }

    private void CheckRandoms()
    {
        EmbelishingVariables.RandomnizeValues();
    }

    public override void SetCurrentColor(Color color)
    {
        base.SetCurrentColor(color);
        EmbelishingVariables.RandomnizeValues();
    }
}
