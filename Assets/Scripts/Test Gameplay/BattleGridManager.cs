using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace TestGameplay
{
    public class BattleGridManager : MonoBehaviour
    {
        private static BattleGridManager instance;
        public static BattleGridManager Instance { get { return instance; } }

        [SerializeField] private Transform cellsContainer;
        [SerializeField] private Vector2Int initialPosPlayer;
        [SerializeField] private Vector2Int initialPosOpponent;
        [SerializeField] private BattleGridUI gridUI;
        [SerializeField] private PlayerActionOptionsPreview actionPreview;

        private Dictionary<Vector2Int, GridCell> grid;
        public List<GridCell> Cells { get { return grid.Values.ToList(); } }

        void Awake()
        {
            instance = this;

            grid = new Dictionary<Vector2Int, GridCell>();

            GridCell[] cells = cellsContainer.GetComponentsInChildren<GridCell>();
            foreach (GridCell cell in cells)
            {
                Vector3Int roundedPos = Vector3Int.CeilToInt(cell.transform.position);
                roundedPos.y = 0;
                cell.transform.position = roundedPos;

                Vector2Int pos = new Vector2Int(roundedPos.x, roundedPos.z);
                grid[pos] = cell;
            }
        }

        void Start()
        {
            BattleSectionManager.Instance.Player.Initialize(initialPosPlayer);
            BattleSectionManager.Instance.Opponent.Initialize(initialPosOpponent);
            gridUI.AssignGrid(grid);
        }

        public bool MoveCharacter(Vector2Int direction)
        {
            if (!IsValidPosition(BattleSectionManager.Instance.InTurn.CurrentPosition + direction))
                return false;

            BattleSectionManager.Instance.InTurn.Move(direction);
            CheckForOverlappedCharacter();
            return true;
        }

        public void CheckForOverlappedCharacter()
        {
            if (BattleSectionManager.Instance.InTurn.CurrentPosition != BattleSectionManager.Instance.NotInTurn.CurrentPosition)
                return;

            List<Vector2Int> directions = new List<Vector2Int>();
            directions.Add(Vector2Int.down);
            directions.Add(Vector2Int.left);
            directions.Add(Vector2Int.right);
            directions.Add(Vector2Int.up);
            directions.Add(Vector2Int.up + Vector2Int.right);
            directions.Add(Vector2Int.up + Vector2Int.left);
            directions.Add(Vector2Int.down + Vector2Int.right);
            directions.Add(Vector2Int.down + Vector2Int.left);

            while (directions.Count > 0)
            {
                Vector2Int current = directions[Random.Range(0, directions.Count)];
                directions.Remove(current);
                if (IsValidPosition(current + BattleSectionManager.Instance.NotInTurn.CurrentPosition))
                {
                    BattleSectionManager.Instance.NotInTurn.Move(current);
                    return;
                }
            }
        }

        public bool IsValidPosition(Vector2Int position)
        {
            return grid.ContainsKey(position);// && BattleSectionManager.Instance.NotInTurn.CurrentPosition != position;
        }

        public bool CanMoveInDirection(Vector2Int position, Vector2Int direction)
        {
            Vector2Int target = position + direction;
            return grid.ContainsKey(target);
        }

        public void CharacterAttacked(Vector2Int direction, int damage)
        {
            BattleSectionManager.Instance.NotInTurn.ReceiveDamage(BattleSectionManager.Instance.InTurn.CurrentPosition, BattleSectionManager.Instance.InTurn.CurrentPosition + direction, damage);
        }

        public Vector3 ConvertPosition(Vector2Int position)
        {
            return new Vector3(position.x, 0, position.y);
        }

        public Vector2Int ConvertPosition(Vector3 position)
        {
            return new Vector2Int((int)position.x, (int)position.z);
        }

        public void UpdatePreview()
        {
            gridUI.UpdatePreview();
            actionPreview.UpdateActionPreview();
        }

        public void HighlightSelected(ActionInput actionInput)
        {
            actionPreview.HighlightSelected(actionInput);
        }
    }
}
