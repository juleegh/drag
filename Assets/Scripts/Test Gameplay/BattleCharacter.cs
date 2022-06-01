using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleCharacter : GridActor
    {
        protected int initialHealth = 20;
        protected int initialStamina = 3;

        protected BattleStats stats;
        public BattleStats Stats { get { return stats; } }

        [SerializeField] protected BattleDefenseIndicator defenseUI;
        [SerializeField] protected BattleActorStatsUI actorStatsUI;
        [SerializeField] protected BattleCharacterAnimation animations;

        protected virtual BattleActorStatsUI StatsUI { get { return actorStatsUI; }  }
        
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
                stats.ReceiveDamage(origin - destination, damage);
                defenseUI.UpdateDefense(stats.Defense);
                StatsUI.SetHealth(stats.Health);
            }
        }

        public void IncreaseHealth(int heal)
        {
            stats.IncreaseHealth(heal);
            StatsUI.SetHealth(stats.Health);
        }

        public void IncreaseDefense(ActionInput direction, int defense)
        {
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
