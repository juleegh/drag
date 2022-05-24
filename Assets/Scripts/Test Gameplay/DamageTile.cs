using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using RotaryHeart.Lib.SerializableDictionary;
using DG.Tweening;

namespace TestGameplay
{
    public class DamageTile : GridActor, IGridDanger
    {
        [SerializeField] private ActionInput attackDirection;
        [SerializeField] private int damage = 1;
        [SerializeField] private SpriteRenderer icon;

        [Serializable]
        public class IconsDictionary : SerializableDictionaryBase<ActionInput, Sprite> { }

        [SerializeField] protected IconsDictionary icons;
        Color transparent = new Color(0, 0, 0, 0);
        Color attack = new Color(1, 0, 0, 0.65f);
        private float colorDelay = 0.15f;
        private float movementDelay = 0.45f;
        private Vector3 center;

        void Awake()
        {
            BattleActionTempo.OnTempoNotification += TurnOn;
            BattleActionTempo.PostTempoNotification += TurnOff;

            icon.sprite = icons[attackDirection];
            icon.color = transparent;
            center = icon.transform.position;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public Vector2Int GetCurrentPosition()
        {
            return currentPosition;
        }

        public Vector2Int GetDelta()
        {
            switch (attackDirection)
            {
                case ActionInput.Up:
                    return Vector2Int.up;
                case ActionInput.Down:
                    return Vector2Int.down;
                case ActionInput.Left:
                    return Vector2Int.left;
                case ActionInput.Right:
                    return Vector2Int.right;
            }
            return Vector2Int.zero;
        }

        private void TurnOn()
        {
            icon.transform.position = center;
            icon.DOColor(attack, colorDelay);
            icon.transform.DOMove(center + GetMovementDelta(), movementDelay);
        }

        private void TurnOff()
        {
            icon.DOColor(transparent, colorDelay);
        }

        private Vector3 GetMovementDelta()
        {
            switch (attackDirection)
            {
                case ActionInput.Up:
                    return Vector3.forward;
                case ActionInput.Down:
                    return -Vector3.forward;
                case ActionInput.Right:
                    return Vector3.right;
                case ActionInput.Left:
                    return -Vector3.right;
            }
            return Vector3.zero;
        }

        public Vector2Int GetAffectedArea()
        {
            return CurrentPosition + GetDelta();
        }

        public int GetDamage()
        {
            return damage;
        }

        public override bool BlocksCell()
        {
            return false;
        }

        public override void ReceiveDamage(Vector2Int origin, Vector2Int destination, int damage)
        {
            // No
        }
    }

}

