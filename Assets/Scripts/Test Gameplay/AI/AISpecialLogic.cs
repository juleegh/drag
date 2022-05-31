using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class AISpecialLogic
    {
        public AISpecialLogic(BattleAIInput configuration)
        {

        }

        public BattleAction SpecialAttackInRange()
        {
            foreach (BattleAction battleAction in BattleAIInput.Instance.SpecialActions.Values)
            {
                if (battleAction.ActionType != BattleActionType.Attack)
                    continue;

                foreach (Vector2Int pos in battleAction.TargetDirections)
                {
                    Vector2Int attackPosition = BattleSectionManager.Instance.Opponent.CurrentPosition + pos;
                    if (attackPosition == BattleSectionManager.Instance.Player.CurrentPosition)
                        return battleAction;
                }
            }

            return null;
        }

        public AITranslateInfo PositionToSpecialAttack()
        {
            AITranslateInfo translation = new AITranslateInfo();
            translation.distance = Vector2Int.zero;
            translation.finalPos = Vector2Int.zero;
            translation.steps = 0;
            bool first = true;

            foreach (BattleAction battleAction in BattleAIInput.Instance.SpecialActions.Values)
            {
                if (battleAction.ActionType != BattleActionType.Attack)
                    continue;

                foreach (Vector2Int pos in battleAction.TargetDirections)
                {
                    Vector2Int attackPosition = BattleSectionManager.Instance.Opponent.CurrentPosition + pos;
                    Vector2Int distance = BattleSectionManager.Instance.Player.CurrentPosition - attackPosition;
                    int steps = BattleAIInput.Instance.MoveLogic.StepsToCell(distance + BattleSectionManager.Instance.Opponent.CurrentPosition);

                    if (steps == -1)
                        continue;

                    if (first)
                    {
                        first = false;
                        translation.distance = distance;
                        translation.steps = steps;
                        translation.finalPos = distance + BattleSectionManager.Instance.Opponent.CurrentPosition;
                        continue;
                    }
                    else if (steps <= translation.steps)
                    {
                        translation.distance = distance;
                        translation.steps = steps;
                        translation.finalPos = distance + BattleSectionManager.Instance.Opponent.CurrentPosition;
                    }
                }
            }

            return translation;
        }
    }
}
