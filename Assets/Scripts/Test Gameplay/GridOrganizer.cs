using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class GridOrganizer : MonoBehaviour
    {
        [ContextMenu("Organize")]
        public void Organize()
        {
            GridCell[] cells = GetComponentsInChildren<GridCell>();
            foreach (GridCell cell in cells)
            {
                Vector3Int roundedPos = Vector3Int.RoundToInt(cell.transform.position);
                roundedPos.y = 0;
                cell.transform.position = roundedPos;
            }
        }

        void Start()
        {
            GridLimit[] cells = GetComponentsInChildren<GridLimit>();
            foreach (GridLimit cell in cells)
            {
                Vector3Int roundedPos = Vector3Int.RoundToInt(cell.transform.position);
                Vector2Int pos = new Vector2Int(roundedPos.x, roundedPos.z);

                cell.SetLimit(ActionInput.Up, !BattleGridManager.Instance.CanMoveInDirection(pos, Vector2Int.up));
                cell.SetLimit(ActionInput.Down, !BattleGridManager.Instance.CanMoveInDirection(pos, Vector2Int.down));
                cell.SetLimit(ActionInput.Left, !BattleGridManager.Instance.CanMoveInDirection(pos, Vector2Int.left));
                cell.SetLimit(ActionInput.Right, !BattleGridManager.Instance.CanMoveInDirection(pos, Vector2Int.right));
            }
        }
    }
}
