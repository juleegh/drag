using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class WigSelection : MonoBehaviour, GlobalComponent
{
    public static WigSelection Instance { get { return instance; } }
    private static WigSelection instance;

    [SerializeField] private WigsConfig wigsConfig;
    private Dictionary<WigType, Wig> wigs;
    public Dictionary<WigType, Wig> Wigs { get { return wigs; } }
    private WigType current;
    public Wig Current { get { return wigs[current]; } }
    public List<WigConfig> WigList { get { return wigsConfig.Wigs.Values.ToList<WigConfig>(); } }

    public void ConfigureRequiredComponent()
    {
        instance = this;
        LoadWigs();
    }

    public void ChangeSelected(WigType selected)
    {
        current = selected;
        WigFitter.Instance.ChangeSelected(wigsConfig.Wigs[current]);
        OutfitEventsManager.Instance.Notify(OutfitEvent.WigSelected);
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

    public WigConfig GetConfigFromType(WigType wigType)
    {
        foreach (KeyValuePair<WigType, WigConfig> wig in wigsConfig.Wigs)
        {
            if (wig.Key == wigType)
                return wig.Value;
        }
        return null;
    }
}
