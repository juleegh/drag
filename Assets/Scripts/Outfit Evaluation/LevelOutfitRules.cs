using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOutfitRules : ScriptableObject
{
    [SerializeField] private string prompt;
    public string Prompt { get { return prompt; } }

    [SerializeField] private List<OutfitRule> rules;
    public List<OutfitRule> Rules { get { return rules; } }
}
