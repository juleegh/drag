using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class AIMoveLogic
    {
        private BattleAIInput config;

        public AIMoveLogic(BattleAIInput configuration)
        {
            config = configuration;
        }

        public void MoveTorwardsPlayer()
        {
            Vector2Int distance = BattleSectionManager.Instance.Opponent.CurrentPosition - BattleSectionManager.Instance.Player.CurrentPosition;
            ActionInput executedAction = ActionInput.Up;

            if (distance.x < 0 && config.MoveActions[ActionInput.Right].WouldHaveEffect())
                executedAction = ActionInput.Right;
            else if (distance.x > 0 && config.MoveActions[ActionInput.Left].WouldHaveEffect())
                executedAction = ActionInput.Left;
            else if (distance.y < 0 && config.MoveActions[ActionInput.Up].WouldHaveEffect())
                executedAction = ActionInput.Up;
            else if (distance.y > 0 && config.MoveActions[ActionInput.Down].WouldHaveEffect())
                executedAction = ActionInput.Down;

            config.MoveActions[executedAction].Execute();
        }

        public void MoveAwayFromPlayer()
        {
            Vector2Int distance = BattleSectionManager.Instance.Player.CurrentPosition - BattleSectionManager.Instance.Opponent.CurrentPosition;
            ActionInput executedAction = ActionInput.Up;

            if (distance.x != 0)
            {
                if (distance.x < 0)
                    executedAction = ActionInput.Right;
                else
                    executedAction = ActionInput.Left;
            }
            else
            {
                if (distance.y < 0)
                    executedAction = ActionInput.Up;
                else
                    executedAction = ActionInput.Down;
            }

            config.MoveActions[executedAction].Execute();
        }

        public bool CanMoveToSpecial(List<Vector2Int> positions)
        {
            if (positions.Count < 1)
                return false;

            foreach (Vector2Int pos in positions)
            {
                Vector2Int targetPos = BattleSectionManager.Instance.Player.CurrentPosition - pos;
                Vector2Int currentPos = BattleSectionManager.Instance.Opponent.CurrentPosition;
                if (!BattleGridManager.Instance.IsValidPosition(targetPos))
                    continue;

                if (Mathf.Abs(targetPos.x - currentPos.x) + Mathf.Abs(targetPos.y - currentPos.y) > BattleSectionManager.Instance.TemposRemaining)
                    continue;

                return true;
            }

            return false;
        }

        public void MoveTorwardsSpecialAttack(List<Vector2Int> positions)
        {
            if (positions.Count < 1)
                return;

            Vector2Int position = positions[0];
            Vector2Int distanceToPos = BattleSectionManager.Instance.Player.CurrentPosition - (BattleSectionManager.Instance.Opponent.CurrentPosition + position);
            foreach (Vector2Int pos in positions)
            {
                Vector2Int distanceFromPlayer = BattleSectionManager.Instance.Player.CurrentPosition - (BattleSectionManager.Instance.Opponent.CurrentPosition + pos);
                Vector2Int newPos = BattleSectionManager.Instance.Opponent.CurrentPosition + distanceFromPlayer;

                if (!BattleGridManager.Instance.IsValidPosition(newPos))
                {
                    continue;
                }

                if (distanceFromPlayer.magnitude < distanceToPos.magnitude)
                {
                    position = pos;
                    distanceToPos = distanceFromPlayer;
                }
            }

            Vector2Int distance = BattleSectionManager.Instance.Player.CurrentPosition - (position + BattleSectionManager.Instance.Opponent.CurrentPosition);
            ActionInput executedAction = ActionInput.Up;

            if (distance.x != 0)
            {
                if (distance.x > 0)
                    executedAction = ActionInput.Right;
                else
                    executedAction = ActionInput.Left;
            }
            else
            {
                if (distance.y > 0)
                    executedAction = ActionInput.Up;
                else
                    executedAction = ActionInput.Down;
            }

            config.MoveActions[executedAction].Execute();
        }
    }
}
