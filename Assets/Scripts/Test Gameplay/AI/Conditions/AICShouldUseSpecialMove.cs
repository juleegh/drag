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
            AITranslateInfo info = BattleAIInput.Instance.MoveLogic.StepsToCell(BattleSectionManager.Instance.Player.CurrentPosition);

            foreach (Vector2Int pos in info.path)
            {
                if (BattleAIInput.Instance.SpecialLogic.CanReachWithSpecial(pos))
                    return true;
            }

            return false;
        }
    }
}
