using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class AIDefenseLogic
    {
        private BattleAIInput config;

        public AIDefenseLogic(BattleAIInput configuration)
        {
            config = configuration;
        }

        public BattleAction PickAbilityToUse()
        {
            foreach (BattleAction battleAction in config.DefenseActions.Values)
            {
                if (!battleAction.HasEnoughStamina())
                    continue;

                if (battleAction.WouldHaveEffect())
                {
                    return battleAction;
                }
            }

            return null;
        }
    }
}
