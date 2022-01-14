using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TestGameplay
{
    public class UIHealthBar : MonoBehaviour
    {
        [SerializeField] private Image bar;
        private float fillDelay;

        void Awake()
        {
            bar.fillAmount = 1;
        }

        public void Fill(float newFill)
        {
            bar.DOFillAmount(newFill, fillDelay);
        }
    }
}
