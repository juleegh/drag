using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigFitter : ColorPicking
{
    public static WigFitter Instance { get { return instance; } }
    private static WigFitter instance;

    [SerializeField] private MeshFilter wigMesh;
    [SerializeField] private MeshRenderer wigRenderer;
    [SerializeField] private WigsConfig wigsConfig;
    private Dictionary<WigType, Wig> wigs;
    public Dictionary<WigType, Wig> Wigs { get { return wigs; } }

    private WigType current;

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
        Mesh meshInstance = Instantiate(wigsConfig.Wigs[current].Mesh);
        wigMesh.mesh = meshInstance;
        //wigMesh.mesh = wigsConfig.Wigs[current].Mesh;
    }

    public override void SetCurrentColor(Color color)
    {
        base.SetCurrentColor(color);
        wigRenderer.material.color = color;
    }
}
