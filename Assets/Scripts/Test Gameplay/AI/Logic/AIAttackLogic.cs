using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class AIAttackLogic
    {
        public AIAttackLogic()
        {

        }

        public void AttackPlayer()
        {
            foreach (BattleAction battleAction in BattleAIInput.Instance.AttackActions.Values)
            {
                Vector2Int distance = BattleSectionManager.Instance.Player.CurrentPosition - BattleSectionManager.Instance.Opponent.CurrentPosition;
                if (battleAction.TargetDirections.Contains(distance))
                {
                    battleAction.Execute();
                    return;
                }
            }
        }

        public bool CanAttack()
        {
            foreach (BattleAction battleAction in BattleAIInput.Instance.AttackActions.Values)
            {
                if (!battleAction.HasEnoughStamina())
                    return false;
            }
            return true;
        }
    }
}
