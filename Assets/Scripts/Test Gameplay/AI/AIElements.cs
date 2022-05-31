using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class AICondition : ScriptableObject
    {
        public virtual bool MeetsRequirement()
        {
            return true;
        }
    }

    public class AIActionOption : ScriptableObject
    {
        [SerializeField] protected List<AICondition> conditions;

        public virtual void ExecuteAction()
        {
            
        }

        public bool CanExecute()
        {
            if (conditions.Count == 0)
                return true;

            Debug.LogWarning("--- Evaluating: " + name + "--------");

            foreach (AICondition condition in conditions)
            {
                bool success = condition.MeetsRequirement();
                Debug.LogWarning("----- Condition met: " + condition.GetType() + "? : " + success);
                if (!success)
                    return false;
            }
            return true;
        }

        public virtual BattleActionType GetActionType()
        {
            return BattleActionType.Unselected;
        }
    }
}

