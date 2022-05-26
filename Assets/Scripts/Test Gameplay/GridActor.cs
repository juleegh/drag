using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

namespace TestGameplay
{
    public class GridActor : MonoBehaviour
    {
        [SerializeField] private float durability = 1;
        protected Vector2Int currentPosition;
        public Vector2Int CurrentPosition { get { return currentPosition; } }
        private bool activeInGrid = true;
        public bool ActiveInGrid { get { return activeInGrid; } }
        float delay = 0.5f;

        public virtual void Initialize()
        {
            Vector3Int roundedPos = Vector3Int.RoundToInt(transform.position);
            roundedPos.y = 0;
            transform.position = roundedPos;

            currentPosition = new Vector2Int(roundedPos.x, roundedPos.z);
            DOTween.KillAll();
        }

        public void Move(Vector2Int position, Action MoveCallback)
        {
            currentPosition += position;
            transform.DOMove(BattleGridManager.Instance.ConvertPosition(currentPosition), delay);
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(delay / 2);
            sequence.AppendCallback(() => { MoveCallback(); });
            sequence.Play();
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

        public virtual bool BlocksCell()
        {
            return true;
        }
    }
}
