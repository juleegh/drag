using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AIC Should Use Special Move")]
    public class AICShouldUseSpecialMove : AICondition
    {
        public override bool MeetsRequirement()
        {
            float currentDistance = Vector2Int.Distance(BattleSectionManager.Instance.Opponent.CurrentPosition, BattleSectionManager.Instance.Player.CurrentPosition);
            AITranslateInfo info = BattleAIInput.Instance.MoveLogic.StepsToCell(BattleSectionManager.Instance.Player.CurrentPosition);

            AITranslateInfo specialTranslation = BattleAIInput.Instance.SpecialLogic.ResultWithSpecialMove(info.path);

            if (specialTranslation != null)
            {
                float newDistance = Vector2Int.Distance(BattleSectionManager.Instance.Player.CurrentPosition, specialTranslation.finalPos);
                if (newDistance < currentDistance)
                    return true;
            }

            return false;
        }
    }
}
