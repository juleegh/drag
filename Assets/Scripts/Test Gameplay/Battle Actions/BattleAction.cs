using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class BattleAction : ScriptableObject
    {
        [SerializeField] protected ActionInput actionInput;
        [SerializeField] protected int requiredStamina;
        [SerializeField] protected Sprite sprite;
        public virtual List<Vector2Int> TargetDirections { get { return new List<Vector2Int>(); } }
        public Sprite Sprite { get { return sprite; } }

        [SerializeField] protected string shortDescription;
        public string ShortDescription { get { return shortDescription; } }
        public int RequiredStamina { get { return requiredStamina; } }
        public virtual BattleActionType ActionType { get { return BattleActionType.Unselected; } }
        public virtual ActionInput ActionInput { get { return actionInput; } }

        public virtual void Execute()
        {
            BattleGridManager.Instance.HighlightSelected(actionInput);
        }

        public bool HasEnoughStamina()
        {
            return BattleSectionManager.Instance.InTurn != null && BattleSectionManager.Instance.InTurn.Stats.Stamina >= requiredStamina;
        }

        public virtual bool WouldHaveEffect()
        {
            return true;
        }

        protected bool OpponentInTargetPosition()
        {
            Vector2Int attackerPosition = BattleSectionManager.Instance.InTurn.CurrentPosition;
            Vector2Int targetPosition = BattleSectionManager.Instance.NotInTurn.CurrentPosition;

            foreach (Vector2Int pos in TargetDirections)
            {
                if (attackerPosition + pos == targetPosition)
                    return true;
            }
            return false;
        }
    }

}
