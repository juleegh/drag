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
    }
}
