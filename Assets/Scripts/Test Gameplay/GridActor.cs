using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TestGameplay
{
    public class GridActor : MonoBehaviour
    {
        [SerializeField] private float durability = 1;
        protected Vector2Int currentPosition;
        public Vector2Int CurrentPosition { get { return currentPosition; } }
        private bool activeInGrid = true;
        public bool ActiveInGrid { get { return activeInGrid; } }

        public virtual void Initialize()
        {
            Vector3Int roundedPos = Vector3Int.CeilToInt(transform.position);
            roundedPos.y = 0;
            transform.position = roundedPos;

            currentPosition = new Vector2Int(roundedPos.x, roundedPos.z);
        }

        public void Move(Vector2Int position, float delay = 0.5f)
        {
            currentPosition += position;
            transform.DOMove(BattleGridManager.Instance.ConvertPosition(currentPosition), delay);
        }

        public virtual void ReceiveDamage(Vector2Int origin, Vector2Int destination, int damage)
        {
            if (currentPosition == destination)
            {
                durability -= damage;
                if (durability <= 0)
                {
                    activeInGrid = false;
                    gameObject.SetActive(false);
                }
            }
        }
    }
}
