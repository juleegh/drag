using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AIC Has No Defense")]
    public class AICHasNoDefense : AICondition
    {
        [SerializeField] private float percentageAllowed;

        public override bool MeetsRequirement()
        {
            return BattleAIInput.Instance.DefenseLogic.ShouldDefendFromPlayer();
        }
    }
}
