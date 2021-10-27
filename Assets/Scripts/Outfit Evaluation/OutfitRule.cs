using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitRule : ScriptableObject
{
    protected float progress;
    [SerializeField] protected float progressionWeight;

    public virtual float Progress { get { return progress; } }
    public virtual float ProgressionWeight { get { return progressionWeight; } }
    public virtual string Prompt { get { return ""; } }

    public virtual void Evaluate(List<Decoration> decorations)
    {
        progress = 1f;
    }
}
