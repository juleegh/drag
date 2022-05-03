using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

namespace TestGameplay
{
    public class PlayerActionsManager : BattleActionExecuter
    {
        protected static PlayerActionsManager instance;
        public static PlayerActionsManager Instance { get { return instance; } }

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
            BattleAction selected = null;
            switch (currentActionType)
            {
                case BattleActionType.Attack:
                    if (attackActions.ContainsKey(executedAction))
                        selected = attackActions[executedAction];
                    break;
                case BattleActionType.Move:
                    if (moveActions.ContainsKey(executedAction))
                        selected = moveActions[executedAction];
                    break;
                case BattleActionType.Defend:
                    if (defenseActions.ContainsKey(executedAction))
                        selected = defenseActions[executedAction];
                    break;
                case BattleActionType.Special:
                    if (specialActions.ContainsKey(executedAction))
                        selected = specialActions[executedAction];
                    break;
            }

            if (selected != null)
            {
                selected.Execute();
            }
        }
    }
}
