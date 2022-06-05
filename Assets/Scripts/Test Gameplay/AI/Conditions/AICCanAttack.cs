using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AIC Can Attack")]
    public class AICCanAttack : AICondition
    {
        public override bool MeetsRequirement()
        {
            return BattleAIInput.Instance.AttackLogic.CanAttack();
        }
    }
}