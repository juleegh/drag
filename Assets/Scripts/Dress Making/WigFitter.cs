using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WigFitter : WigPreview, GlobalComponent
{
    public static WigFitter Instance { get { return instance; } }
    private static WigFitter instance;

    public Color CurrentColor { get { return currentColor; } }
    public string CurrentWig { get { return current != null ? current.name : ""; } }

    public void ConfigureRequiredComponent()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }
}
