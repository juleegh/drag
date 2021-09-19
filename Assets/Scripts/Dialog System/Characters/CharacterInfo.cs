using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character")]
public class CharacterInfo : ScriptableObject
{
    [SerializeField] private string nameIdentifier;
    [SerializeField] private TextAsset dialogsFile;

    public string NameIdentifier { get { return nameIdentifier; } }
    public TextAsset DialogsFile { get { return dialogsFile; } }
}
