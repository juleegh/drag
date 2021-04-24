using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameData : MonoBehaviour
{
    public virtual void Save(GameDataWriter writer) { }
    public virtual void Load(GameDataReader reader) { }
}
