using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigFitter : MonoBehaviour
{
    public static WigFitter Instance { get { return instance; } }
    private static WigFitter instance;

    private GameObject currentPrefab;
    private Color currentColor;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void ChangeSelected(WigConfig selected)
    {
        GameObject meshInstance = Instantiate(selected.Mesh);
        Destroy(currentPrefab);
        meshInstance.transform.SetParent(this.transform);
        meshInstance.transform.localPosition = Vector3.zero;
        meshInstance.transform.localScale = Vector3.one;
        meshInstance.transform.localRotation = Quaternion.identity;
        currentPrefab = meshInstance;
        currentPrefab.GetComponentInChildren<MeshRenderer>().material.color = currentColor;
    }

    public void SetCurrentColor(Color color)
    {
        currentColor = color;
        if (currentPrefab != null) currentPrefab.GetComponentInChildren<MeshRenderer>().material.color = color;
    }
}
