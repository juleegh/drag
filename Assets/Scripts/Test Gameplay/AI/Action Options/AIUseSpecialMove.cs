using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AI Special Move")]
    class AIUseSpecialMove : AIActionOption
    {
        public override BattleActionType GetActionType()
        {
            return BattleActionType.Special;
        }

        public override void ExecuteAction()
        {
            AITranslateInfo translate = BattleAIInput.Instance.MoveLogic.StepsToCell(BattleSectionManager.Instance.Player.CurrentPosition);
            BattleAIInput.Instance.SpecialLogic.CutPathWithSpecial(translate.path);
        }
    }
}
