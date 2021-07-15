using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigSelection : MonoBehaviour, RequiredComponent
{
    public static WigSelection Instance { get { return instance; } }
    private static WigSelection instance;

    [SerializeField] private WigsConfig wigsConfig;
    private Dictionary<WigType, Wig> wigs;
    public Dictionary<WigType, Wig> Wigs { get { return wigs; } }
    private WigType current;

    public void ConfigureRequiredComponent()
    {
        instance = this;
        LoadWigs();
    }

    public void ChangeSelected(WigType selected)
    {
        current = selected;
        WigFitter.Instance.ChangeSelected(wigsConfig.Wigs[current]);
    }

    public void ChangeSelected(string selected)
    {
        current = GetWigTypeFromName(selected);
        WigFitter.Instance.ChangeSelected(wigsConfig.Wigs[current]);
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

    public WigType GetWigTypeFromName(string wigName)
    {
        foreach (KeyValuePair<WigType, Wig> wig in wigs)
        {
            if (wig.Value.CodeName.Equals(wigName))
                return wig.Key;
        }
        return WigType.None;
    }
}
