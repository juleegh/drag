using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AI Walk To Special Attack")]
    class AIWalkToSPecialAttack : AIActionOption
    {
        public override BattleActionType GetActionType()
        {
            return BattleActionType.Move;
        }

        public override void ExecuteAction()
        {
            AITranslateInfo info = BattleAIInput.Instance.SpecialLogic.PositionToSpecialAttack();
            BattleAIInput.Instance.MoveLogic.MoveTorwardsCell(info.finalPos);
        }
    }
}
