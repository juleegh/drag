using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleCharacter : MonoBehaviour
    {
        private float initialHealth = 100f;
        private Vector2Int currentPosition;
        public Vector2Int CurrentPosition { get { return currentPosition; } }
        private float health;

        [SerializeField] private UIHealthBar healthBar;

        public void Initialize(Vector2Int initialPosition)
        {
            currentPosition = initialPosition;
            transform.position = BattleGridManager.Instance.ConvertPosition(initialPosition);
            health = initialHealth;
            healthBar.Fill(1f);
        }

        public void Move(Vector2Int position, float delay)
        {
            currentPosition += position;
            transform.DOMove(BattleGridManager.Instance.ConvertPosition(currentPosition), delay);
        }

        public void ReceiveDamage(Vector2Int position, float damage)
        {
            if (currentPosition == position)
            {
                health -= damage;
                healthBar.Fill(health / initialHealth);
            }
        }
    }
}
