using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BattleIconUI : MonoBehaviour
{
    [SerializeField] private SpriteRenderer icon;
    [SerializeField] private Color visible;
    [SerializeField] private Color notVisible;

    public void Initialize(bool vis)
    {
        icon.color = vis ? visible : notVisible;
    }

    public void Toggle(bool vis, bool positive)
    {
        Color target = vis ? visible : notVisible;
        if (target != icon.color)
            icon.color = positive ? Color.cyan : Color.red;
        icon.DOColor(target, 0.65f).SetEase(Ease.InElastic);
    }
}
