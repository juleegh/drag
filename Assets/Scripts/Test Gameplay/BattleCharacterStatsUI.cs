using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleCharacterStatsUI : MonoBehaviour
    {
        [SerializeField] private UIStatsBar healthBar;
        [SerializeField] private UIStatsBar staminaBar;

        public void Initialize(int health, int stamina)
        {
            healthBar.Fill(health);
            staminaBar.Fill(stamina);
        }

        public void SetHealth(int fill)
        {
            healthBar.Fill(fill);
        }

        public void SetStamina(int fill)
        {
            staminaBar.Fill(fill);
        }
    }
}
