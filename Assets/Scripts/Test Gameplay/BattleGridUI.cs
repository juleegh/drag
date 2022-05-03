using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class BattleGridUI : MonoBehaviour
    {
        private Dictionary<Vector2Int, GridCell> grid;

        public void AssignGrid(Dictionary<Vector2Int, GridCell> g)
        {
            grid = g;
        }

        public void UpdatePreview()
        {
            List<Vector2Int> positions = BattleSectionManager.Instance.CurrentExecuter.GetTargetPositions();
            BattleActionType currentActionType = BattleSectionManager.Instance.CurrentExecuter.CurrentActionType;
            foreach (KeyValuePair<Vector2Int, GridCell> cell in grid)
            {
                cell.Value.Clean();
            }


            foreach (Vector2Int position in positions)
            {
                Vector2Int dancerPosition = BattleSectionManager.Instance.InTurn.CurrentPosition;
                Vector2Int cellPosition = dancerPosition + position;

                if (!grid.ContainsKey(cellPosition))
                    continue;

                switch (currentActionType)
                {
                    case BattleActionType.Attack:
                    case BattleActionType.Defend:
                    case BattleActionType.Move:
                        grid[cellPosition].PaintAction(currentActionType);
                        break;
                    case BattleActionType.Special:
                        grid[cellPosition].PaintAction(GetActionType(dancerPosition, cellPosition));
                        break;
                }
            }
        }

        private ActionInput GetInputByPosition(Vector2Int dancer, Vector2Int cell)
        {
            Vector2Int distance = cell - dancer;
            return BattleSectionManager.Instance.CurrentExecuter.GetActionByPosition(distance).ActionInput;
        }

        private BattleActionType GetActionType(Vector2Int dancer, Vector2Int cell)
        {
            ActionInput input = GetInputByPosition(dancer, cell);
            BattleAction battleAction = BattleSectionManager.Instance.CurrentExecuter.GetActionByInput(input);
            return battleAction != null ? battleAction.ActionType : BattleActionType.Unselected;
        }
    }
}
