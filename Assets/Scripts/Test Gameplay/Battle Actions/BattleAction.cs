using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class BattleAction : ScriptableObject
    {
        [SerializeField] protected ActionInput actionInput;
        public virtual List<Vector2Int> TargetPositions { get { return new List<Vector2Int>(); } }

        public virtual void Execute()
        {

        }
    }

}
