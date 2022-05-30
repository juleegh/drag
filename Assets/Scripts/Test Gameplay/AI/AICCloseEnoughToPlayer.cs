using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AIC Close Enough To Player")]
    public class AICCloseEnoughToPlayer : AICondition
    {
        [SerializeField] private int stepsRequired;

        public override bool MeetsRequirement()
        {
            Vector2Int distance = BattleSectionManager.Instance.Player.CurrentPosition - BattleSectionManager.Instance.Opponent.CurrentPosition;
            int steps = Mathf.Abs(distance.x) + Mathf.Abs(distance.y);
            return distance.magnitude <= stepsRequired;
        }
    }
}


