using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "Defense Action")]
    public class DefenseAction : BattleAction
    {
        public override BattleActionType ActionType { get { return BattleActionType.Defend; } }

        [SerializeField] private int defenseIncrease;

        public override void Execute()
        {
            if (HasEnoughStamina())
            {
                BattleSectionManager.Instance.InTurn.IncreaseDefense(actionInput, defenseIncrease);
                BattleSectionManager.Instance.InTurn.DecreaseStamina(requiredStamina);
                base.Execute();
            }
        }

        public override bool WouldHaveEffect()
        {
            float healthPercentage = BattleSectionManager.Instance.InTurn.Stats.Health / BattleSectionManager.Instance.InTurn.Stats.BaseHealth;
            bool lowHP = BattleSectionManager.Instance.InTurn.Stats.Defense[actionInput] <= 0;
            bool posMakesSense = false;
            Vector2Int currentPositionDelta = BattleSectionManager.Instance.NotInTurn.CurrentPosition - BattleSectionManager.Instance.InTurn.CurrentPosition;
            if (actionInput == ActionInput.Up && currentPositionDelta.y > 0)
                posMakesSense = true;
            else if (actionInput == ActionInput.Left && currentPositionDelta.x < 0)
                posMakesSense = true;
            else if (actionInput == ActionInput.Right && currentPositionDelta.x > 0)
                posMakesSense = true;
            else if (actionInput == ActionInput.Down && currentPositionDelta.y < 0)
                posMakesSense = true;

            return lowHP || posMakesSense;
        }

        public override List<Vector2Int> TargetDirections
        {
            get
            {
                return new List<Vector2Int>();
            }
        }
    }
}
