using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "AIC Can Special Attack")]
    public class AICCanSpecialAttack : AICondition
    {
        public override bool MeetsRequirement()
        {
            return BattleAIInput.Instance.SpecialLogic.SpecialAttackInRange() != null;
        }
    }
}