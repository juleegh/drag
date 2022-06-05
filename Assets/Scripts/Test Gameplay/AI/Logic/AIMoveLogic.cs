using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class AIMoveLogic
    {
        private ShortestPath dijkstra;
        private List<Vector2Int> currentPath;
        private Vector2Int currentObjective;

        public AIMoveLogic()
        {
            dijkstra = new ShortestPath();
        }

        public void Initialize()
        {
            dijkstra.Initialize(BattleGridManager.Instance.Cells);
            currentPath = dijkstra.findShortestPath(BattleSectionManager.Instance.Opponent.CurrentPosition, BattleSectionManager.Instance.Player.CurrentPosition);
            currentObjective = BattleSectionManager.Instance.Player.CurrentPosition;
            currentPath.Remove(currentPath[0]);
        }

        public void MoveTorwardsPlayer()
        {
            if (currentObjective != BattleSectionManager.Instance.Player.CurrentPosition || currentPath.Count == 0)
            {
                currentPath = dijkstra.findShortestPath(BattleSectionManager.Instance.Opponent.CurrentPosition, BattleSectionManager.Instance.Player.CurrentPosition);
                currentObjective = BattleSectionManager.Instance.Player.CurrentPosition;
                currentPath.Remove(currentPath[0]);
            }

            Vector2Int distance = BattleSectionManager.Instance.Opponent.CurrentPosition - currentPath[0];
            ActionInput executedAction = ActionInput.Up;

            if (distance.x < 0 && BattleAIInput.Instance.MoveActions[ActionInput.Right].WouldHaveEffect())
                executedAction = ActionInput.Right;
            else if (distance.x > 0 && BattleAIInput.Instance.MoveActions[ActionInput.Left].WouldHaveEffect())
                executedAction = ActionInput.Left;
            else if (distance.y < 0 && BattleAIInput.Instance.MoveActions[ActionInput.Up].WouldHaveEffect())
                executedAction = ActionInput.Up;
            else if (distance.y > 0 && BattleAIInput.Instance.MoveActions[ActionInput.Down].WouldHaveEffect())
                executedAction = ActionInput.Down;

            currentPath.Remove(currentPath[0]);
            BattleAIInput.Instance.MoveActions[executedAction].Execute();
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

            BattleAIInput.Instance.MoveActions[executedAction].Execute();
        }

        public AITranslateInfo StepsToCell(Vector2Int destination)
        {
            if (!BattleGridManager.Instance.IsValidPosition(destination))
                return null;


            AITranslateInfo translation = new AITranslateInfo();
            translation.distance = destination - BattleSectionManager.Instance.Opponent.CurrentPosition;
            translation.finalPos = destination;
            translation.path = dijkstra.findShortestPath(BattleSectionManager.Instance.Opponent.CurrentPosition, destination);
            translation.steps = translation.path.Count;

            return translation;
        }

        public void MoveTorwardsCell(Vector2Int position)
        {
            if (currentObjective != position || currentPath.Count == 0)
            {
                currentPath = dijkstra.findShortestPath(BattleSectionManager.Instance.Opponent.CurrentPosition, position);
                currentObjective = position;
                currentPath.Remove(currentPath[0]);
            }

            Vector2Int distance = BattleSectionManager.Instance.Opponent.CurrentPosition - currentPath[0];
            ActionInput executedAction = ActionInput.Up;

            if (distance.x < 0 && BattleAIInput.Instance.MoveActions[ActionInput.Right].WouldHaveEffect())
                executedAction = ActionInput.Right;
            else if (distance.x > 0 && BattleAIInput.Instance.MoveActions[ActionInput.Left].WouldHaveEffect())
                executedAction = ActionInput.Left;
            else if (distance.y < 0 && BattleAIInput.Instance.MoveActions[ActionInput.Up].WouldHaveEffect())
                executedAction = ActionInput.Up;
            else if (distance.y > 0 && BattleAIInput.Instance.MoveActions[ActionInput.Down].WouldHaveEffect())
                executedAction = ActionInput.Down;

            currentPath.Remove(currentPath[0]);
            BattleAIInput.Instance.MoveActions[executedAction].Execute();
        }

        public void ResetCurrentTarget()
        {
            currentPath.Clear();
        }
    }
}
