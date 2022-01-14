using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

namespace TestGameplay
{
    public class PlayerActionsManager : MonoBehaviour
    {
        private static PlayerActionsManager instance;
        public static PlayerActionsManager Instance { get { return instance; } }

        [Serializable]
        public class ActionsDictionary : SerializableDictionaryBase<ActionInput, BattleAction> { }

        [SerializeField] private ActionsDictionary moveActions;
        [SerializeField] private ActionsDictionary attackActions;
        [SerializeField] private ActionsDictionary defenseActions;

        private BattleActionType currentActionType;
        public BattleActionType CurrentActionType { get { return currentActionType; } }

        void Awake()
        {
            instance = this;
            currentActionType = BattleActionType.Unselected;
        }

        public void ChangeActionType(BattleActionType actionType)
        {
            currentActionType = actionType;
            BattleGridManager.Instance.UpdatePreview();
        }

        public void ExecutedAction(ActionInput executedAction)
        {
            switch (currentActionType)
            {
                case BattleActionType.Attack:
                    attackActions[executedAction].Execute();
                    break;
                case BattleActionType.Move:
                    moveActions[executedAction].Execute();
                    break;
                case BattleActionType.Defend:
                    defenseActions[executedAction].Execute();
                    break;
            }
            UIBattleActionSelection.Instance.ShowExecutedAction(executedAction);
        }

        public List<Vector2Int> GetTargetPositions()
        {
            List<Vector2Int> positions = new List<Vector2Int>();

            switch (currentActionType)
            {
                case BattleActionType.Attack:
                    foreach (BattleAction battleAction in attackActions.Values)
                    {
                        positions.AddRange(battleAction.TargetPositions);
                    }
                    break;
                case BattleActionType.Move:
                    foreach (BattleAction battleAction in moveActions.Values)
                    {
                        positions.AddRange(battleAction.TargetPositions);
                    }
                    break;
            }

            return positions;
        }
    }
}
