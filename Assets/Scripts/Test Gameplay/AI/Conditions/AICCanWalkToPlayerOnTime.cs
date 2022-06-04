using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    //[CreateAssetMenu(fileName = "AIC Can Walk To Player On Time")]
    public class AICCanWalkToPlayerOnTime : AICondition
    {
        public override bool MeetsRequirement()
        {
            Vector2Int distance = BattleSectionManager.Instance.Player.CurrentPosition - BattleSectionManager.Instance.Opponent.CurrentPosition;
            int steps = Mathf.Abs(distance.x) + Mathf.Abs(distance.y) - 1;
            int turnsLeft = BattleSectionManager.Instance.TemposRemaining;
            return turnsLeft - 1 >= steps;
        }
    }
}


