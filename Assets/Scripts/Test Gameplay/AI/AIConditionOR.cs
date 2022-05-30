using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    [CreateAssetMenu(fileName = "AIC OR")]
    public class AIConditionOR : AICondition
    {
        [SerializeField] private AICondition conditionOne;
        [SerializeField] private AICondition conditionTwo;

        public override bool MeetsRequirement()
        {
            return conditionOne.MeetsRequirement() || conditionTwo.MeetsRequirement();
        }
    }
}

