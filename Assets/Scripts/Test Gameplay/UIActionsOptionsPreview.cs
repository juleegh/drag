using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class UIActionsOptionsPreview : MonoBehaviour
    {
        private static UIActionsOptionsPreview instance;
        public static UIActionsOptionsPreview Instance { get { return instance; } }

        [SerializeField] private GameObject container;
        [SerializeField] private UIActionPreview UpAction;
        [SerializeField] private UIActionPreview DownAction;
        [SerializeField] private UIActionPreview LeftAction;
        [SerializeField] private UIActionPreview RightAction;

        void Awake()
        {
            instance = this;
        }

        public void UpdateActionPreview()
        {
            container.SetActive(BattleSectionManager.Instance.IsPlayerTurn);
            PaintDescription(ActionInput.Up);
            PaintDescription(ActionInput.Left);
            PaintDescription(ActionInput.Down);
            PaintDescription(ActionInput.Right);
        }

        public void HighlightSelected(ActionInput actionInput)
        {
            GetActionPreview(actionInput).HighlightSelected();
        }

        private void PaintDescription(ActionInput actionInput)
        {
            BattleAction battleAction = BattleSectionManager.Instance.CurrentExecuter.GetActionByInput(actionInput);
            GetActionPreview(actionInput).PaintAction(battleAction);
        }

        private UIActionPreview GetActionPreview(ActionInput actionInput)
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
