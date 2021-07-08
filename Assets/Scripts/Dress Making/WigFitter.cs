using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigFitter : MonoBehaviour
{
    public static WigFitter Instance { get { return instance; } }
    private static WigFitter instance;

    [SerializeField] private MeshFilter wigMesh;
    [SerializeField] private MeshRenderer wigRenderer;
    [SerializeField] private WigsConfig wigsConfig;
    private Dictionary<WigType, Wig> wigs;
    public Dictionary<WigType, Wig> Wigs { get { return wigs; } }

    private WigType current;
    private GameObject currentPrefab;
    private Color currentColor;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            LoadWigs();
        }
        else
            Destroy(this.gameObject);
    }

    private void LoadWigs()
    {
        wigs = new Dictionary<WigType, Wig>();
        foreach (KeyValuePair<WigType, WigConfig> wig in wigsConfig.Wigs)
        {
            Wig nextWig = new Wig(wig.Value, wig.Key);
            wigs.Add(wig.Key, nextWig);
            current = wig.Key;
        }
    }

    public void ChangeSelected(WigType selected)
    {
        current = selected;
        GameObject meshInstance = Instantiate(wigsConfig.Wigs[current].Mesh);
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
