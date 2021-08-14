using UnityEngine;
using UnityEngine.UI;

public class ImageColorDisplay : ColorDisplay
{
    [SerializeField] private Image imageColor;

    public override void ChangeDisplayColor(Color color)
    {
        imageColor.color = color;
    }
}
