using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    class AIBehaviourTree : MonoBehaviour
    {
        [SerializeField] private List<AIActionOption> turnOptions;
        AIActionOption currentlySelected;

        public void ChooseActionForTurn()
        {
            foreach (AIActionOption turnOption in turnOptions)
            {
                if (turnOption.CanExecute())
                {
                    currentlySelected = turnOption;
                    break;
                }
            }
        }

        public BattleActionType GetNextActionType()
        {
            if (currentlySelected == null)
                return BattleActionType.Unselected;

            return currentlySelected.GetActionType();
        }

        public void ExecuteNextAction()
        {
            if (currentlySelected != null)
                currentlySelected.ExecuteAction();
        }
    }
}
