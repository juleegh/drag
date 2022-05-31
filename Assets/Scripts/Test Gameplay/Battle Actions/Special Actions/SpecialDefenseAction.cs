using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "Special Defense")]
    public class SpecialDefenseAction : SpecialAction
    {
        [SerializeField] private int defenseIncrease;

        public override void Execute()
        {
            if (HasEnoughStamina())
            {
                BattleSectionManager.Instance.InTurn.IncreaseDefense(ActionInput.Up, defenseIncrease);
                BattleSectionManager.Instance.InTurn.IncreaseDefense(ActionInput.Down, defenseIncrease);
                BattleSectionManager.Instance.InTurn.IncreaseDefense(ActionInput.Left, defenseIncrease);
                BattleSectionManager.Instance.InTurn.IncreaseDefense(ActionInput.Right, defenseIncrease);
                BattleSectionManager.Instance.InTurn.DecreaseStamina(requiredStamina);
                base.Execute();
            }
        }

        public override List<Vector2Int> TargetDirections
        {
            get
            {
                return new List<Vector2Int>();
            }
        }

        public override bool WouldHaveEffect()
        {
            return BattleSectionManager.Instance.InTurn.Stats.Defense[ActionInput.Up] == 0 &&
             BattleSectionManager.Instance.InTurn.Stats.Defense[ActionInput.Down] == 0 &&
             BattleSectionManager.Instance.InTurn.Stats.Defense[ActionInput.Left] == 0 &&
             BattleSectionManager.Instance.InTurn.Stats.Defense[ActionInput.Right] == 0;
        }
    }
}