using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "AI Flee")]
    class AIFlee : AIActionOption
    {
        public override BattleActionType GetActionType()
        {
            return BattleActionType.Move;
        }

        public override void ExecuteAction()
        {
            BattleAIInput.Instance.MoveLogic.MoveAwayFromPlayer();
        }
    }
}
