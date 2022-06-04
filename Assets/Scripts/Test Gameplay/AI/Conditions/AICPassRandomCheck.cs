using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "AIC Pass Random Check")]
    class AICPassRandomCheck : AICondition
    {
        [SerializeField] private float randomCheck;

        public override bool MeetsRequirement()
        {
            float random = Random.Range(0f, 1f);
            return random <= randomCheck;
        }
    }
}
