using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class BattleInputReader : MonoBehaviour
    {
        private PlayerActionsManager actionsManager { get { return PlayerActionsManager.Instance; } }
        private bool hasExecutedAction;

        private void Start()
        {
            actionsManager.ChangeActionType(BattleActionType.Move);
            UIBattleActionSelection.Instance.PaintSelectedAction(ActionInput.Right);
            hasExecutedAction = false;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                actionsManager.ChangeActionType(BattleActionType.Special);
                UIBattleActionSelection.Instance.PaintSelectedAction(ActionInput.Up);
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                actionsManager.ChangeActionType(BattleActionType.Attack);
                UIBattleActionSelection.Instance.PaintSelectedAction(ActionInput.Left);
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                actionsManager.ChangeActionType(BattleActionType.Move);
                UIBattleActionSelection.Instance.PaintSelectedAction(ActionInput.Right);
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                actionsManager.ChangeActionType(BattleActionType.Defend);
                UIBattleActionSelection.Instance.PaintSelectedAction(ActionInput.Down);
            }

            if (!BattleSectionManager.Instance.IsPlayerTurn)
                return;

            if (!BattleActionTempo.Instance.IsOnPostTempo && !BattleActionTempo.Instance.IsOnPreTempo)
            {
                hasExecutedAction = false;
                return;
            }

            if (hasExecutedAction)
                return;

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                actionsManager.ExecutedAction(ActionInput.Down);
                hasExecutedAction = true;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                actionsManager.ExecutedAction(ActionInput.Up);
                hasExecutedAction = true;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                actionsManager.ExecutedAction(ActionInput.Left);
                hasExecutedAction = true;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                actionsManager.ExecutedAction(ActionInput.Right);
                hasExecutedAction = true;
            }
        }

    }
}
