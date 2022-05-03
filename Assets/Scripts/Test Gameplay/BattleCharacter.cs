using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleCharacter : MonoBehaviour
    {
        private int initialHealth = 10;
        private int initialStamina = 10;
        private Vector2Int currentPosition;
        public Vector2Int CurrentPosition { get { return currentPosition; } }
        private BattleStats stats;
        public BattleStats Stats { get { return stats; } }

        [SerializeField] private BattleCharacterStatsUI statsUI;
        [SerializeField] private BattleDefenseIndicator defenseUI;

        public void Initialize(Vector2Int initialPosition)
        {
            currentPosition = initialPosition;
            transform.position = BattleGridManager.Instance.ConvertPosition(initialPosition);
            stats = new BattleStats(initialHealth, initialStamina);
            statsUI.Initialize(initialHealth, initialStamina);
            defenseUI.UpdateDefense(stats.Defense);
        }

        public void Move(Vector2Int position, float delay)
        {
            currentPosition += position;
            transform.DOMove(BattleGridManager.Instance.ConvertPosition(currentPosition), delay);
        }

        public void ReceiveDamage(Vector2Int origin, Vector2Int destination, int damage)
        {
            if (currentPosition == destination)
            {
                stats.ReceiveDamage(origin - destination, damage);
                statsUI.SetHealth(stats.Health);
                defenseUI.UpdateDefense(stats.Defense);
            }
        }

        public void IncreaseHealth(int heal)
        {
            stats.IncreaseHealth(heal);
            statsUI.SetHealth(stats.Health);
        }

        public void DecreaseStamina(int stamina)
        {
            stats.DecreaseStamina(stamina);
            statsUI.SetStamina(stats.Stamina);
        }

        public void IncreaseDefense(ActionInput direction, int defense)
        {
            stats.BoostDefense(direction, defense);
            defenseUI.UpdateDefense(stats.Defense);
        }

        public void ResetStats()
        {
            stats.ResetBoosts();
            statsUI.SetStamina(stats.Stamina);
        }
    }
}
