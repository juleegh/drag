using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "AI Special Attack")]
    class AISpecialAttack : AIActionOption
    {
        public override BattleActionType GetActionType()
        {
            return BattleActionType.Special;
        }

        public override void ExecuteAction()
        {
            BattleAIInput.Instance.SpecialLogic.SpecialAttackInRange().Execute();
        }
    }
}
