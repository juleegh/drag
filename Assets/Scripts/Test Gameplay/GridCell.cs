using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace TestGameplay
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cellInfo;
        [SerializeField] private Color moveColor;
        [SerializeField] private Color attackColor;
        float colorDelay = 0.3f;
        Color transparent = new Color(0, 0, 0, 0);

        public void Clean()
        {
            if (cellInfo.color != transparent)
                cellInfo.DOColor(transparent, colorDelay);
        }

        public void PaintAttack()
        {
            if (cellInfo.color != attackColor)
                cellInfo.DOColor(attackColor, colorDelay);
        }

        public void PaintMove()
        {
            if (cellInfo.color != moveColor)
                cellInfo.DOColor(moveColor, colorDelay);
        }
    }
}
