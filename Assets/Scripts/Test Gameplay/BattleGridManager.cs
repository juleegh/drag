using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class BattleGridManager : MonoBehaviour
    {
        private static BattleGridManager instance;
        public static BattleGridManager Instance { get { return instance; } }

        [SerializeField] private int rows;
        [SerializeField] private int columns;
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Transform cellsContainer;
        [SerializeField] private Vector2Int initialPosPlayer;
        [SerializeField] private Vector2Int initialPosOpponent;

        private Dictionary<Vector2Int, GridCell> grid;

        void Awake()
        {
            instance = this;

            grid = new Dictionary<Vector2Int, GridCell>();
            for (int column = 0; column < columns; column++)
            {
                for (int row = 0; row < rows; row++)
                {
                    GridCell cell = Instantiate(cellPrefab).GetComponent<GridCell>();
                    grid[new Vector2Int(column, row)] = cell;
                    cell.transform.position = new Vector3(column, 0f, row);
                }
            }

        }

        void Start()
        {
            BattleSectionManager.Instance.Player.Initialize(initialPosPlayer);
            BattleSectionManager.Instance.Opponent.Initialize(initialPosOpponent);
        }

        public void CharacterMoved(Vector2Int position, float delay = 0.5f)
        {
            BattleSectionManager.Instance.InTurn.Move(position, delay);
            UpdatePreview();
        }

        public void CharacterAttacked(Vector2Int position, float damage)
        {
            BattleSectionManager.Instance.NotInTurn.ReceiveDamage(BattleSectionManager.Instance.InTurn.CurrentPosition + position, damage);
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
            List<Vector2Int> positions = PlayerActionsManager.Instance.GetTargetPositions();

            foreach (KeyValuePair<Vector2Int, GridCell> cell in grid)
            {
                switch (PlayerActionsManager.Instance.CurrentActionType)
                {
                    case BattleActionType.Unselected:
                        cell.Value.Clean();
                        break;
                    case BattleActionType.Attack:
                        if (positions.Contains(cell.Key - BattleSectionManager.Instance.InTurn.CurrentPosition))
                            cell.Value.PaintAttack();
                        else
                            cell.Value.Clean();
                        break;
                    case BattleActionType.Move:
                        if (positions.Contains(cell.Key - BattleSectionManager.Instance.InTurn.CurrentPosition))
                            cell.Value.PaintMove();
                        else
                            cell.Value.Clean();
                        break;
                }
            }
        }
    }
}
