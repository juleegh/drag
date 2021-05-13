using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Embelisher : MonoBehaviour
{
    private static Embelisher instance;
    public static Embelisher Instance { get { return instance; } }

    public Color CurrentColor;

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
                decoration.transform.rotation = Quaternion.LookRotation(hit.normal);
                decoration.transform.SetParent(hit.transform);
                decoration.GetComponent<GarmentDecoration>().SetColor(CurrentColor);
            }
        }
    }
}
