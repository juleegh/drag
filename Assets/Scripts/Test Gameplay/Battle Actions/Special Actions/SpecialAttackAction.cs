using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "Special Attack")]
    public class SpecialAttackAction : SpecialAction
    {
        [SerializeField] protected List<Vector2Int> positionsDelta;
        [SerializeField] protected int damage;
        public override List<Vector2Int> TargetDirections { get { return positionsDelta; } }

        public override void Execute()
        {
            if (HasEnoughStamina())
            {
                foreach (Vector2Int position in TargetDirections)
                    BattleGridManager.Instance.CharacterAttacked(position, damage);
                BattleSectionManager.Instance.InTurn.DecreaseStamina(requiredStamina);
                base.Execute();
            }
        }

        public override bool WouldHaveEffect()
        {
            return OpponentInTargetPosition();
        }
    }
}