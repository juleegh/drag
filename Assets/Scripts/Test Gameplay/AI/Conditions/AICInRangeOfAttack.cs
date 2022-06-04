using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "AIC In Range Of Attack")]
    public class AICInRangeOfAttack : AICondition
    {
        public override bool MeetsRequirement()
        {
            Vector2Int distance = BattleSectionManager.Instance.Opponent.CurrentPosition - BattleSectionManager.Instance.Player.CurrentPosition;
            foreach (BattleAction battleAction in BattleAIInput.Instance.AttackActions.Values)
            {
                if (battleAction.TargetDirections.Contains(distance))
                    return true;
            }
            return false;
        }
    }
}


