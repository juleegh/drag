using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleCharacter : GridActor
    {
        protected int initialHealth = 15;
        protected int initialStamina = 3;

        protected BattleStats stats;
        public BattleStats Stats { get { return stats; } }

        [SerializeField] protected BattleDefenseIndicator defenseUI;
        [SerializeField] protected BattleActorStatsUI actorStatsUI;
        [SerializeField] protected BattleCharacterAnimation animations;
        [SerializeField] protected AudioSource shieldUp;
        [SerializeField] protected AudioSource shieldHit;
        [SerializeField] protected AudioSource hurt;

        protected virtual BattleActorStatsUI StatsUI { get { return actorStatsUI; } }
        public bool RequiresStamina { get { return initialStamina > stats.Stamina; } }

        public override void Initialize()
        {
            base.Initialize();
            stats = new BattleStats(initialHealth, initialStamina);
            defenseUI.UpdateDefense(stats.Defense);
            StatsUI.InitializeHealth(initialHealth);
            StatsUI.InitializeStamina(initialStamina);
            BattleActionTempo.OnTempoNotification += IddleAnimation;
        }

        public override void ReceiveDamage(Vector2Int origin, Vector2Int destination, int damage)
        {
            if (currentPosition == destination)
            {
                int original = stats.Health;
                stats.ReceiveDamage(origin - destination, damage);
                defenseUI.UpdateDefense(stats.Defense);
                StatsUI.SetHealth(stats.Health);
                if (original > stats.Health)
                {
                    if (stats.Health > 0)
                    {
                        hurt.Play();
                    }
                    animations.Hurt();
                }
                else
                {
                    shieldHit.Play();
                }
            }
        }

        public void IncreaseHealth(int heal)
        {
            stats.IncreaseHealth(heal);
            StatsUI.SetHealth(stats.Health);
        }

        public void IncreaseDefense(ActionInput direction, int defense)
        {
            shieldUp.Play();
            stats.BoostDefense(direction, defense);
            defenseUI.UpdateDefense(stats.Defense);
        }

        public virtual void DecreaseStamina(int stamina)
        {
            stats.DecreaseStamina(stamina);
            StatsUI.SetStamina(stats.Stamina);
        }

        public virtual void ResetStats()
        {
            stats.ResetBoosts();
            defenseUI.UpdateDefense(stats.Defense);
        }

        public virtual void ResetStamina()
        {
            stats.ResetStamina();
            StatsUI.SetStamina(stats.Stamina);
        }

        private void IddleAnimation()
        {
            animations.MarkTempo();
        }
    }
}
