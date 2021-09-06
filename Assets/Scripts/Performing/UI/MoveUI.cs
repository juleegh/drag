using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoveUI : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image sphere;
    [SerializeField] private Image buff;
    [SerializeField] private Image timeProgress;
    [SerializeField] private TextMeshProUGUI typeText;

    [SerializeField] private Color empty;
    [SerializeField] private Color full;
    [SerializeField] private Color right;
    [SerializeField] private Color wrong;
    [SerializeField] private Color transparent;
    [SerializeField] private MovesProperties properties;

    private MoveBuff moveBuff;

    public void MarkAsEmpty()
    {
        timeProgress.fillAmount = 0f;
        background.color = empty;
        sphere.color = transparent;
        typeText.text = "";
        buff.sprite = null;
        buff.color = transparent;
    }

    public void MarkAsBuff(MoveBuff theBuff)
    {
        moveBuff = theBuff;

        if (moveBuff == MoveBuff.None)
        {
            MarkAsEmpty();
            return;
        }

        timeProgress.fillAmount = 0f;
        background.color = empty;
        sphere.color = transparent;
        typeText.text = "";

        switch (theBuff)
        {
            case MoveBuff.None:
                buff.sprite = null;
                buff.color = transparent;
                break;
            case MoveBuff.Double:
                buff.sprite = properties.SpriteByBuff[MoveBuff.Double];
                buff.color = Color.white;
                break;
            case MoveBuff.Half:
                buff.sprite = properties.SpriteByBuff[MoveBuff.Half];
                buff.color = Color.white;
                break;
        }
    }

    public void MarkAsMove(MoveType moveType)
    {
        if (moveBuff == MoveBuff.None)
        {
            MarkAsEmpty();
            return;
        }

        background.color = full;
        switch (moveType)
        {
            case MoveType.Score:
                sphere.color = PerformSystem.Instance.MovesProperties.ColorByMove[MoveType.Score];
                typeText.text = "A";
                break;
            case MoveType.Defense:
                sphere.color = PerformSystem.Instance.MovesProperties.ColorByMove[MoveType.Defense];
                typeText.text = "B";
                break;
            case MoveType.Attack:
                sphere.color = PerformSystem.Instance.MovesProperties.ColorByMove[MoveType.Attack];
                typeText.text = "X";
                break;
        }
    }

    public void MarkCompleted(bool correct)
    {
        if (moveBuff == MoveBuff.None)
            return;

        if (correct)
            background.color = right;
        else
            background.color = wrong;
    }

    public void MarkProgress(float progress)
    {
        timeProgress.fillAmount = progress;

        if (TempoCounter.Instance.IsOnPreTempo || TempoCounter.Instance.IsOnPostTempo)
            timeProgress.color = Color.green;
        else
            timeProgress.color = Color.red;
    }
}
