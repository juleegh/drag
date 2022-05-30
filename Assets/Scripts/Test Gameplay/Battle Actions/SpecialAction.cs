using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "Move Action")]
    public class SpecialAction : BattleAction
    {
        [SerializeField] protected BattleActionType actionType;

        public override BattleActionType ActionType { get { return actionType; } }
    }
}