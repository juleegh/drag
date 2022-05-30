using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AI Defend In Direction")]
    class AIDefendInDirection : AIActionOption
    {
        public override BattleActionType GetActionType()
        {
            return BattleActionType.Defend;
        }

        public override void ExecuteAction()
        {
            BattleAIInput.Instance.DefenseLogic.DefendFromPlayer();
        }
    }
}
