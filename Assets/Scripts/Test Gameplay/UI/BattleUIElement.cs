using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace TestGameplay
{
    public class BattleUIElement : MonoBehaviour
    {
        private Color highlited;
        private Color obscured;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
            highlited = image.color;
            obscured = highlited - Color.white * 0.4f;
            image.color = obscured;
        }

        public void Hightlight(bool highlights)
        {
            if (highlights && image.color == obscured)
            {
                image.color = (highlited);
            }
            else if (!highlights && image.color == highlited)
            {
                image.color = (obscured);
            }
        }

        public void ToggleVisible(bool visible)
        {
            image.gameObject.SetActive(visible);
        }
    }
}
