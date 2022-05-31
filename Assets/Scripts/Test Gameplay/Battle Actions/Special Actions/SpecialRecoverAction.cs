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
            BattleSectionManager.Instance.InTurn.IncreaseHealth(health);
            base.Execute();
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
            return BattleSectionManager.Instance.InTurn.Stats.BaseHealth - BattleSectionManager.Instance.InTurn.Stats.Health >= health;
        }
    }
}