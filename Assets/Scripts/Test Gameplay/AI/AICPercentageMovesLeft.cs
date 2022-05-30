using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AIC Percentage Moves Left")]
    public class AICPercentageMovesLeft : AICondition
    {
        [SerializeField] private float percentageAllowed;

        public override bool MeetsRequirement()
        {
            int turnsLeft = BattleSectionManager.Instance.TemposRemaining;
            float remainingPercentage = (float) turnsLeft / (float) BattleSectionManager.Instance.TemposPerPlayer;
            return remainingPercentage <= percentageAllowed;
        }
    }
}
