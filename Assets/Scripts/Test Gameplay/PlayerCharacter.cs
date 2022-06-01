using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestGameplay
{
    public class PlayerCharacter : BattleCharacter
    {
        public override void ReceiveDamage(Vector2Int origin, Vector2Int destination, int damage)
        {
            base.ReceiveDamage(origin, destination, damage);

            if (stats.Health <= 0)
            {
                BattleRespawn.Instance.RespawnCharacters();
            }
        }
    }
}
