using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AI Heal")]
    class AIHeal : AIActionOption
    {
        public override BattleActionType GetActionType()
        {
            return BattleActionType.Special;
        }

        public override void ExecuteAction()
        {
            //BattleAIInput.Instance.SpecialLogic.Heal();
        }
    }
}
