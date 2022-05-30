using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class AIDefenseLogic
    {
        public AIDefenseLogic()
        {

        }

        public void DefendFromPlayer()
        {
            Vector2Int distance = BattleSectionManager.Instance.Player.CurrentPosition - BattleSectionManager.Instance.Opponent.CurrentPosition;
            BattleAction executedAction = null;

            if (distance.x > 0)
                executedAction = BattleAIInput.Instance.DefenseActions[ActionInput.Right];
            else if (distance.x < 0)
                executedAction = BattleAIInput.Instance.DefenseActions[ActionInput.Left];
            else if (distance.y > 0)
                executedAction = BattleAIInput.Instance.DefenseActions[ActionInput.Up];
            else if (distance.y < 0)
                executedAction = BattleAIInput.Instance.DefenseActions[ActionInput.Down];

            if (executedAction != null)
                executedAction.Execute();
        }

        public bool ShouldDefendFromPlayer()
        {
            Vector2Int distance = BattleSectionManager.Instance.Player.CurrentPosition - BattleSectionManager.Instance.Opponent.CurrentPosition;

            if (distance.x > 0)
                return BattleSectionManager.Instance.Opponent.Stats.Defense[ActionInput.Right] <= 0;
            else if (distance.x < 0)
                return BattleSectionManager.Instance.Opponent.Stats.Defense[ActionInput.Left] <= 0;
            else if (distance.y > 0)
                return BattleSectionManager.Instance.Opponent.Stats.Defense[ActionInput.Up] <= 0;
            else if (distance.y < 0)
                return BattleSectionManager.Instance.Opponent.Stats.Defense[ActionInput.Down] <= 0;

            return false;
        }
    }
}
