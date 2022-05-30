using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AI Attack Player")]
    class AIAttackPlayer : AIActionOption
    {
        public override BattleActionType GetActionType()
        {
            return BattleActionType.Attack;
        }

        public override void ExecuteAction()
        {
            BattleAIInput.Instance.AttackLogic.AttackPlayer();
        }
    }
}
