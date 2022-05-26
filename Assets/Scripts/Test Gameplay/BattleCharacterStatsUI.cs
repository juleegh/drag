using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleCharacterStatsUI : BattleActorStatsUI
    {
        [SerializeField] private UIStatsBar healthBar;
        [SerializeField] private UIStatsBar staminaBar;

        public override void InitializeHealth(int health)
        {
            healthBar.Setup(health);
        }

        public void InitializeStamina(int stamina)
        {
            staminaBar.Setup(stamina);
        }

        public override void SetHealth(int fill)
        {
            healthBar.Fill(fill);
        }

        public void SetStamina(int fill)
        {
            staminaBar.Fill(fill);
        }
    }
}
