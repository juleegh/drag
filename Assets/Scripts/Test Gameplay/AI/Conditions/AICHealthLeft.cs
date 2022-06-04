using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "AIC Percentage Health Left")]
    public class AICHealthLeft : AICondition
    {
        [SerializeField] private float percentageAllowed;

        public override bool MeetsRequirement()
        {
            float currentHealth = (float) BattleSectionManager.Instance.InTurn.Stats.Health;
            float maxHealth = (float) BattleSectionManager.Instance.InTurn.Stats.BaseHealth;
            float remainingPercentage = currentHealth / maxHealth;
            return remainingPercentage <= percentageAllowed;
        }
    }
}
