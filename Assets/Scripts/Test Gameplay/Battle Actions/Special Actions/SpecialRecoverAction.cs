using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "Special Recover")]
    public class SpecialRecoverAction : SpecialAction
    {
        [SerializeField] protected int health;

        public override void Execute()
        {
            if (HasEnoughStamina())
            {
                //foreach (Vector2Int position in positionsDelta)
                //  BattleGridManager.Instance.CharacterAttacked(position, damage);
                BattleSectionManager.Instance.InTurn.IncreaseHealth(health);
                BattleSectionManager.Instance.InTurn.DecreaseStamina(requiredStamina);
                base.Execute();
            }
        }

        public override List<Vector2Int> TargetDirections
        {
            get
            {
                /*
                List<Vector2Int> positionContainer = new List<Vector2Int>();
                if (actionInput == ActionInput.Up)
                    positionContainer.Add(Vector2Int.up);
                if (actionInput == ActionInput.Down)
                    positionContainer.Add(Vector2Int.down);
                if (actionInput == ActionInput.Left)
                    positionContainer.Add(Vector2Int.left);
                if (actionInput == ActionInput.Right)
                    positionContainer.Add(Vector2Int.right);
                return positionContainer;
                */
                return new List<Vector2Int>();
            }
        }

        public override bool WouldHaveEffect()
        {
            return BattleSectionManager.Instance.InTurn.Stats.BaseHealth - BattleSectionManager.Instance.InTurn.Stats.Health >= health;
        }
    }
}