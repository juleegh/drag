using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class AISpecialLogic
    {
        public AISpecialLogic(BattleAIInput configuration)
        {

        }

        public BattleAction PickAbilityToUse()
        {
            foreach (BattleAction battleAction in BattleAIInput.Instance.SpecialActions.Values)
            {
                if (!battleAction.HasEnoughStamina())
                    continue;

                if (battleAction.ActionType == BattleActionType.Defend && battleAction.WouldHaveEffect())
                    return battleAction;
                else if (battleAction.ActionType == BattleActionType.Attack)
                    return battleAction;
            }

            return null;
        }

        public bool SpecialAbilityIsInRange(BattleAction battleAction)
        {
            foreach (Vector2Int pos in battleAction.TargetDirections)
            {
                Vector2Int attackPosition = BattleSectionManager.Instance.Opponent.CurrentPosition + pos;
                if (attackPosition == BattleSectionManager.Instance.Player.CurrentPosition)
                    return true;
            }

            return false;
        }
    }
}
