using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleUIElement : MonoBehaviour
    {
        [SerializeField] private Color highlited;
        [SerializeField] private Color obscured;

        private float colorDelay = 0.3f;
        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
            image.color = obscured;
        }

        public void Hightlight(bool highlights)
        {
            if (highlights && image.color == obscured)
            {
                image.DOColor(highlited, colorDelay);
            }
            else if (!highlights && image.color == highlited)
            {
                image.DOColor(obscured, colorDelay);
            }
        }

        public void Twinkle()
        {
            image.DOColor(highlited, colorDelay / 2).OnComplete(() => { image.DOColor(obscured, colorDelay / 2); });
        }

        public void Cooldown()
        {
            image.color = highlited;
            image.DOColor(obscured, colorDelay);
        }
    }
}
