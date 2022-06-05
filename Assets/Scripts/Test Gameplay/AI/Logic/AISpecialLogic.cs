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
                if (!battleAction.HasEnoughStamina() || battleAction.ActionType != BattleActionType.Attack)
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

        public bool CanSpecialAttack()
        {
            foreach (BattleAction battleAction in BattleAIInput.Instance.SpecialActions.Values)
            {
                if (!battleAction.HasEnoughStamina() || battleAction.ActionType != BattleActionType.Attack)
                    continue;
                
                return true;
            }
            return false;
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
                if (!battleAction.HasEnoughStamina() || battleAction.ActionType != BattleActionType.Attack)
                    continue;

                foreach (Vector2Int pos in battleAction.TargetDirections)
                {
                    Vector2Int attackPosition = BattleSectionManager.Instance.Opponent.CurrentPosition + pos;
                    Vector2Int distance = BattleSectionManager.Instance.Player.CurrentPosition - attackPosition;
                    AITranslateInfo translationTemp = BattleAIInput.Instance.MoveLogic.StepsToCell(distance + BattleSectionManager.Instance.Opponent.CurrentPosition);

                    if (translationTemp == null)
                        continue;

                    int steps = translationTemp.steps;

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

        public AITranslateInfo ResultWithSpecialMove(List<Vector2Int> destination)
        {

            foreach (BattleAction battleAction in BattleAIInput.Instance.SpecialActions.Values)
            {
                if (!battleAction.HasEnoughStamina() || battleAction.ActionType != BattleActionType.Move)
                    continue;

                Vector2Int position = BattleSectionManager.Instance.Opponent.CurrentPosition + battleAction.TargetDirections[battleAction.TargetDirections.Count - 1];
                if (destination.Contains(position) && position != BattleSectionManager.Instance.Player.CurrentPosition)
                {
                    AITranslateInfo info = new AITranslateInfo();
                    info.finalPos = position;
                    info.steps = battleAction.TargetDirections.Count;
                    return info;
                }
            }

            return null;
        }

        public void CutPathWithSpecial(Vector2Int destination)
        {
            float currentDistance = Vector2Int.Distance(BattleSectionManager.Instance.Opponent.CurrentPosition, destination);
            foreach (BattleAction battleAction in BattleAIInput.Instance.SpecialActions.Values)
            {
                if (!battleAction.HasEnoughStamina() || battleAction.ActionType != BattleActionType.Move)
                    continue;

                Vector2Int endPos = BattleSectionManager.Instance.Opponent.CurrentPosition + battleAction.TargetDirections[battleAction.TargetDirections.Count - 1];
                float distance = Vector2Int.Distance(endPos, destination);

                if (BattleGridManager.Instance.IsValidPosition(endPos) && distance < currentDistance)
                {
                    battleAction.Execute();
                    return;
                }
            }
        }
    }
}
