using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigFitter : MonoBehaviour, GlobalComponent
{
    public static WigFitter Instance { get { return instance; } }
    private static WigFitter instance;

    private GameObject currentPrefab;
    private Color currentColor;
    public Color CurrentColor { get { return currentColor; } }
    public string CurrentWig { get { return current != null ? current.name : ""; } }
    private WigConfig current;

    public void ConfigureRequiredComponent()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void ChangeSelected(WigConfig selected)
    {
        current = selected;
        Destroy(currentPrefab);
        if (selected.Mesh == null)
            return;
        GameObject meshInstance = Instantiate(selected.Mesh);
        meshInstance.transform.SetParent(this.transform);
        meshInstance.transform.localPosition = Vector3.zero;
        meshInstance.transform.localScale = Vector3.one;
        meshInstance.transform.localRotation = Quaternion.identity;
        currentPrefab = meshInstance;
        currentPrefab.GetComponentInChildren<MeshRenderer>().material.color = currentColor;
        SetCurrentColor(currentColor);
    }

    public void SetCurrentColor(Color color)
    {
        currentColor = color;
        if (currentPrefab != null) currentPrefab.GetComponentInChildren<MeshRenderer>().material.SetColor("Color_20784d248b894514aa916400667bcfb5", color);
    }
}
