using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace TestGameplay
{
    public class UIActionPreview : MonoBehaviour
    {
        [SerializeField] private Image mainIcon;
        [SerializeField] private Image staminaIcon;
        [SerializeField] private TextMeshProUGUI staminaDescription;
        [SerializeField] private Image background;

        private Color moveColor { get { return BattleColorsUtil.Instance.Move; } }
        private Color attackColor { get { return BattleColorsUtil.Instance.Attack; } }
        private Color defendColor { get { return BattleColorsUtil.Instance.Defend; } }

        private BattleAction battleAction;
        private Color regularAlpha = new Color(0, 0, 0, 0.1f);
        private Color blockedAlpha = new Color(0.8f, 0.8f, 0.8f, 0.1f);
        private Color regularAction = Color.white;
        private Color blockedAction = new Color(0.5f, 0.5f, 0.5f, 0.9f);

        private Sprite ActionSprite { get { return battleAction != null ? battleAction.Sprite : null; } }
        private string ActionDescription { get { return battleAction != null ? battleAction.ShortDescription : ""; } }
        private int ActionStamina { get { return battleAction != null ? battleAction.RequiredStamina : 0; } }

        private Vector3 regularScale;
        private Vector3 increasedScale;

        void Start()
        {
            regularScale = background.transform.localScale;
            increasedScale = regularScale * 1.2f;
        }

        public void PaintAction(BattleAction newAction)
        {
            battleAction = newAction;
            if (battleAction == null)
            {
                SetAsEmpty();
                return;
            }

            PaintDescription();
            background.color = GetBackgroundColor();
            mainIcon.color = GetAvailableColor();
        }

        private void PaintDescription()
        {
            background.gameObject.SetActive(true);
            mainIcon.gameObject.SetActive(ActionSprite != null);
            staminaIcon.gameObject.SetActive(ActionStamina > 0);
            staminaDescription.gameObject.SetActive(ActionStamina > 0);
            mainIcon.sprite = ActionSprite;
            staminaDescription.text = ActionStamina.ToString();
            staminaDescription.color = GetStaminaColor();
        }

        private void SetAsEmpty()
        {
            background.gameObject.SetActive(false);
            mainIcon.gameObject.SetActive(false);
            staminaIcon.gameObject.SetActive(false);
            staminaDescription.gameObject.SetActive(false);
        }

        private Color GetActionColor()
        {
            Color color = Color.black;
            switch (battleAction.ActionType)
            {
                case BattleActionType.Attack:
                    color = attackColor;
                    break;
                case BattleActionType.Defend:
                    color = defendColor;
                    break;
                case BattleActionType.Move:
                    color = moveColor;
                    break;
            }

            return color;
        }

        private Color GetBackgroundColor()
        {
            Color color = GetActionColor();

            //if (battleAction.WouldHaveEffect() && battleAction.HasEnoughStamina())
            if (battleAction.HasEnoughStamina())
                color -= regularAlpha;
            else
                color -= blockedAlpha;

            return color;
        }

        private Color GetStaminaColor()
        {
            if (!battleAction.HasEnoughStamina())
                return Color.red;
            else
                return Color.white;
        }

        private Color GetAvailableColor()
        {
            //if (battleAction.WouldHaveEffect() && battleAction.HasEnoughStamina())
            if (battleAction.HasEnoughStamina())
                return regularAction;
            else
                return blockedAction;
        }

        public void HighlightSelected()
        {
            background.color = GetBackgroundColor();
            background.transform.localScale = increasedScale;
            background.transform.DOScale(increasedScale, 0.45f);
        }
    }
}
