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
        private int totalHealth;

        public virtual void InitializeHealth(int health)
        {
            totalHealth = health;
            actorHealthBar.fillAmount = 1;
        }

        public virtual void SetHealth(int fill)
        {
            float percentage = (float) fill / (float) totalHealth;
            actorHealthBar.fillAmount = percentage;
        }
    }
}
