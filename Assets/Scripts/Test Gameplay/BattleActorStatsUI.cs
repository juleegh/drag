using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleActorStatsUI : MonoBehaviour
    {
        [SerializeField] private Image actorHealthBar;
        [SerializeField] private UIStatsBar staminaBar;
        private int totalHealth;
        private int totalStamina;

        public void InitializeHealth(int health)
        {
            totalHealth = health;
            actorHealthBar.fillAmount = 1;
        }

        public void InitializeStamina(int stamina)
        {
            totalStamina = stamina;
            staminaBar.Setup(stamina);
        }

        public void SetHealth(int fill)
        {
            float percentage = (float) fill / (float) totalHealth;
            actorHealthBar.fillAmount = percentage;
        }

        public void SetStamina(int fill)
        {
            staminaBar.Fill(fill);
        }
    }
}
