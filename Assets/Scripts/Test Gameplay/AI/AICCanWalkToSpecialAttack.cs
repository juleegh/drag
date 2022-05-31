using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AIC Can Special Attack")]
    public class AICCanWalkToSpecialAttack : AICondition
    {
        public override bool MeetsRequirement()
        {
            AITranslateInfo info = BattleAIInput.Instance.SpecialLogic.PositionToSpecialAttack();

            if (info.distance == Vector2Int.zero)
                return false;

            int steps = info.steps;
            int turnsLeft = BattleSectionManager.Instance.TemposRemaining;
            return turnsLeft - 1 >= steps;
        }
    }
}