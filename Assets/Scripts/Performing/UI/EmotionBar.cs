using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class EmotionBar : MonoBehaviour
{
    [SerializeField] private Image expected;
    [SerializeField] private Image filled;
    [SerializeField] private TextMeshProUGUI buttonCode;

    public void SetEmotion(MoveType moveType, Color color)
    {
        Color mild = color;
        mild.a = 0.5f;
        expected.color = mild;
        filled.color = color;

        switch (moveType)
        {
            case MoveType.AType:
                buttonCode.text = "A";
                break;
            case MoveType.BType:
                buttonCode.text = "B";
                break;
            case MoveType.XType:
                buttonCode.text = "X";
                break;
            case MoveType.YType:
                buttonCode.text = "Y";
                break;
        }
    }

    public void SetExpected(float percent)
    {
        DOTween.To(FillExpected, expected.fillAmount, percent, 0.3f);
    }

    private void FillExpected(float percent)
    {
        expected.fillAmount = percent;
    }

    public void SetFilled(float percent)
    {
        DOTween.To(FillFilled, filled.fillAmount, percent, 0.3f);
    }

    private void FillFilled(float percent)
    {
        filled.fillAmount = percent;
    }

}
