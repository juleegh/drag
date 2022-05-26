using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class PlayerCharacter : BattleCharacter
    {
        private BattleCharacterStatsUI playerStatsUI;
        protected override BattleActorStatsUI StatsUI { get { return playerStatsUI; }  }

        public override void Initialize()
        {
            playerStatsUI = actorStatsUI as BattleCharacterStatsUI;
            base.Initialize();
            playerStatsUI.InitializeStamina(initialStamina);
        }

        public override void ReceiveDamage(Vector2Int origin, Vector2Int destination, int damage)
        {
            base.ReceiveDamage(origin, destination, damage);

            if (stats.Health <= 0)
            {
                BattleRespawn.Instance.RespawnCharacters();
            }
        }

        public override void DecreaseStamina(int stamina)
        {
            base.DecreaseStamina(stamina);
            playerStatsUI.SetStamina(stats.Stamina);
        }

        public override void ResetStats()
        {
            base.ResetStats();
            playerStatsUI.SetStamina(stats.Stamina);
        }
    }
}
