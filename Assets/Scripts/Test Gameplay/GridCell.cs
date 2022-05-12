using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

namespace TestGameplay
{
    public class GridCell : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer cellInfo;
        [SerializeField] private CellTempoCounter counter;

        private Color moveColor { get { return BattleColorsUtil.Instance.Move - alpha; } }
        private Color attackColor { get { return BattleColorsUtil.Instance.Attack - alpha; } }
        private Color defendColor { get { return BattleColorsUtil.Instance.Defend - alpha; } }

        float colorDelay = 0.3f;
        Color transparent = new Color(0, 0, 0, 0);
        Color alpha = new Color(0, 0, 0, 0.4f);
        Tween colorTween;

        private Vector2Int position;
        public Vector2Int Position { get { return position; } }

        void Start()
        {
            int posSum = (int)transform.position.x + (int)transform.position.z;
            position = new Vector2Int((int)transform.position.x, (int)transform.position.z);
            counter.SetupStart(posSum % 2 == 0);
        }

        public void Clean()
        {
            if (colorTween != null)
                colorTween.Kill();
            cellInfo.color = transparent;
            return;
            //if (cellInfo.color != transparent)
            //  cellInfo.DOColor(transparent, colorDelay);
        }

        public void PaintAction(BattleActionType input)
        {
            switch (input)
            {
                case BattleActionType.Attack:
                    if (cellInfo.color != attackColor)
                        colorTween = cellInfo.DOColor(attackColor, colorDelay);
                    break;
                case BattleActionType.Move:
                    if (cellInfo.color != moveColor)
                        colorTween = cellInfo.DOColor(moveColor, colorDelay);
                    break;
                case BattleActionType.Defend:
                    if (cellInfo.color != defendColor)
                        colorTween = cellInfo.DOColor(defendColor, colorDelay);
                    break;
            }
        }
    }
}
