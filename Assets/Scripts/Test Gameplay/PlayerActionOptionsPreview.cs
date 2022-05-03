using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{

    public class PlayerActionOptionsPreview : MonoBehaviour
    {
        [SerializeField] private PlayerActionPreview UpAction;
        [SerializeField] private PlayerActionPreview DownAction;
        [SerializeField] private PlayerActionPreview LeftAction;
        [SerializeField] private PlayerActionPreview RightAction;

        public void UpdateActionPreview()
        {
            PaintDescription(ActionInput.Up);
            PaintDescription(ActionInput.Left);
            PaintDescription(ActionInput.Down);
            PaintDescription(ActionInput.Right);
            PositionToPlayer();
        }

        public void HighlightSelected(ActionInput actionInput)
        {
            GetActionPreview(actionInput).HighlightSelected();
        }

        private void PositionToPlayer()
        {
            Vector2Int pos = BattleSectionManager.Instance.InTurn.CurrentPosition;
            transform.position = new Vector3(pos.x, 1.8f, pos.y);
        }

        private void PaintDescription(ActionInput actionInput)
        {
            BattleAction battleAction = BattleSectionManager.Instance.CurrentExecuter.GetActionByInput(actionInput);
            GetActionPreview(actionInput).PaintAction(battleAction);
        }

        private PlayerActionPreview GetActionPreview(ActionInput actionInput)
        {
            //if (BattleSectionManager.Instance.CurrentExecuter.CurrentActionType != BattleActionType.Move && BattleSectionManager.Instance.CurrentExecuter.CurrentActionType != BattleActionType.Defend)
            //  return GetPreviewByRotation(actionInput);

            switch (actionInput)
            {
                case ActionInput.Up:
                    return UpAction;
                case ActionInput.Down:
                    return DownAction;
                case ActionInput.Left:
                    return LeftAction;
                default:
                    return RightAction;
            }
        }

        /*
                private PlayerActionPreview GetPreviewByRotation(ActionInput actionInput)
                {
                    Vector2Int direction = InputToOrientation(actionInput);
                    Vector2Int orientation = BattleSectionManager.Instance.InTurn.Orientation;
                    Vector2Int xValue = orientation * direction.y;
                    Vector2Int yValue = new Vector2Int(orientation.y, -orientation.x) * direction.x;
                    orientation = xValue + yValue;

                    if (orientation == Vector2Int.up)
                        return UpAction;
                    if (orientation == Vector2Int.down)
                        return DownAction;
                    if (orientation == Vector2Int.left)
                        return LeftAction;
                    else
                        return RightAction;
                }

                private Vector2Int InputToOrientation(ActionInput actionInput)
                {
                    switch (actionInput)
                    {
                        case ActionInput.Up:
                            return Vector2Int.up;
                        case ActionInput.Down:
                            return Vector2Int.down;
                        case ActionInput.Left:
                            return Vector2Int.left;
                        default:
                            return Vector2Int.right;
                    }
                }

                private ActionInput OrientationToInput(Vector2Int orientation)
                {
                    if (orientation == Vector2Int.up)
                        return ActionInput.Up;
                    if (orientation == Vector2Int.down)
                        return ActionInput.Down;
                    if (orientation == Vector2Int.left)
                        return ActionInput.Left;
                    else
                        return ActionInput.Right;
                }
        */
    }
}
