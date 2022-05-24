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
        [SerializeField] private BattleGridUI gridUI;
        private UIActionsOptionsPreview actionPreview { get { return UIActionsOptionsPreview.Instance; } }

        private Dictionary<Vector2Int, GridCell> grid;
        private List<GridActor> gridActors;
        private List<IGridDanger> gridDangers;
        public List<GridCell> Cells { get { return grid.Values.ToList(); } }

        void Awake()
        {
            instance = this;

            grid = new Dictionary<Vector2Int, GridCell>();
            gridActors = new List<GridActor>();
            gridDangers = new List<IGridDanger>();

            GridCell[] cells = cellsContainer.GetComponentsInChildren<GridCell>();
            foreach (GridCell cell in cells)
            {
                Vector3Int roundedPos = Vector3Int.CeilToInt(cell.transform.position);
                roundedPos.y = 0;
                cell.transform.position = roundedPos;

                Vector2Int pos = new Vector2Int(roundedPos.x, roundedPos.z);
                grid[pos] = cell;
            }

            GridActor[] actors = GameObject.FindObjectsOfType<GridActor>();
            foreach (GridActor actor in actors)
            {
                actor.Initialize();
                gridActors.Add(actor);
            }

            IEnumerable<IGridDanger> dangers = FindObjectsOfType<MonoBehaviour>().OfType<IGridDanger>();
            foreach (IGridDanger danger in dangers)
            {
                gridDangers.Add(danger);
            }
        }

        void Start()
        {
            BattleSectionManager.Instance.Player.Initialize();
            BattleSectionManager.Instance.Opponent.Initialize();
            BattleRespawn.Instance.SetCheckpoint(BattleSectionManager.Instance.Player, BattleSectionManager.Instance.Player.CurrentPosition);
            BattleRespawn.Instance.SetCheckpoint(BattleSectionManager.Instance.Opponent, BattleSectionManager.Instance.Opponent.CurrentPosition);
            gridUI.AssignGrid(grid);
        }

        public bool MoveCharacter(Vector2Int direction, bool canPush = false)
        {
            if (!IsValidPosition(BattleSectionManager.Instance.InTurn.CurrentPosition + direction))
                return false;

            if (!canPush && IsPositionOccupied(BattleSectionManager.Instance.InTurn.CurrentPosition + direction))
                return false;

            BattleSectionManager.Instance.InTurn.Move(direction, MoveCallback);
            return true;
        }

        private void CheckForOverlappedCharacter()
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
                    BattleSectionManager.Instance.NotInTurn.Move(current, MoveCallback);
                    return;
                }
            }
        }

        private void CheckForDangerousTiles()
        {
            foreach (IGridDanger danger in gridDangers)
            {
                Vector2Int position = danger.GetAffectedArea();
                BattleCharacter character = GetCharacterInPosition(position);
                if (character != null)
                {
                    CharacterAttacked(danger.GetCurrentPosition(), danger.GetDelta(), danger.GetDamage());
                }
            }
        }

        public bool IsValidPosition(Vector2Int position)
        {
            return grid.ContainsKey(position);
        }

        public bool IsPositionOccupied(Vector2Int position)
        {
            foreach (GridActor actor in gridActors)
            {
                if (actor.CurrentPosition == position && actor.BlocksCell())
                    return true;
            }
            return false;
        }

        private BattleCharacter GetCharacterInPosition(Vector2Int position)
        {
            foreach (GridActor actor in gridActors)
            {
                if (actor.CurrentPosition == position)
                {
                    BattleCharacter character = actor as BattleCharacter;
                    if (character != null)
                        return character;
                }
            }
            return null;
        }

        private void MoveCallback()
        {
            CheckForOverlappedCharacter();
            CheckForDangerousTiles();
        }

        public bool CanMoveInDirection(Vector2Int position, Vector2Int direction)
        {
            Vector2Int target = position + direction;
            return grid.ContainsKey(target);
        }

        public void CharacterAttacked(Vector2Int origin, Vector2Int direction, int damage)
        {
            foreach (GridActor actor in gridActors)
            {
                if (actor.ActiveInGrid)
                    actor.ReceiveDamage(origin, origin + direction, damage);
            }

            List<GridActor> toRemove = new List<GridActor>();
            foreach (GridActor actor in gridActors)
            {
                if (!actor.ActiveInGrid)
                    toRemove.Add(actor);
            }

            foreach (GridActor actor in toRemove)
            {
                gridActors.Remove(actor);
            }
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
