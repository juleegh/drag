using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleCharacterAnimation : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Sprite idle;
        [SerializeField] Sprite move;
        [SerializeField] Sprite attack;
        private float delay = 0.35f;

        public void MarkTempo()
        {
            //transform.DORotate(transform.eulerAngles + Vector3.right * 180 + Vector3.forward * 180, 0.1f);
            transform.eulerAngles = transform.eulerAngles + (Vector3.right + Vector3.forward) * 180;

            spriteRenderer.sprite = move;
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(delay);
            sequence.AppendCallback(() => { spriteRenderer.sprite = idle; });
            sequence.Play();
        }

        public void Attack()
        {
            spriteRenderer.sprite = attack;
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(delay);
            sequence.AppendCallback(() => { spriteRenderer.sprite = idle; });
            sequence.Play();
        }

        public void Move()
        {
            spriteRenderer.sprite = move;
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(delay);
            sequence.AppendCallback(() => { spriteRenderer.sprite = idle; });
            sequence.Play();
        }
    }
}
