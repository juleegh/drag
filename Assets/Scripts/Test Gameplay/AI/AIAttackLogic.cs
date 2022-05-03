using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class AIAttackLogic
    {
        private BattleAIInput config;

        public AIAttackLogic(BattleAIInput configuration)
        {
            config = configuration;
        }

        public bool IsPlayerInAttackRange()
        {
            Vector2Int playerPos = BattleSectionManager.Instance.Player.CurrentPosition;
            List<Vector2Int> positions = new List<Vector2Int>();
            foreach (BattleAction battleAction in config.AttackActions.Values)
            {
                positions.AddRange(battleAction.TargetDirections);
            }

            return positions.Contains(BattleSectionManager.Instance.Opponent.CurrentPosition - BattleSectionManager.Instance.Player.CurrentPosition);
        }

        public void AttackPlayer()
        {
            foreach (BattleAction battleAction in config.AttackActions.Values)
            {
                Vector2Int distance = BattleSectionManager.Instance.Player.CurrentPosition - BattleSectionManager.Instance.Opponent.CurrentPosition;
                if (battleAction.TargetDirections.Contains(distance))
                {
                    battleAction.Execute();
                    return;
                }
            }
        }
    }
}
