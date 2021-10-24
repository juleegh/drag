using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutfitColorRule : OutfitRule
{
    [SerializeField] private Color expectedColor;
    [SerializeField] private float colorFlexibility;
    [SerializeField] private float percentageInColor;

    public override void Evaluate(List<Decoration> decorations)
    {
        float inColor = 0;
        float totalColors = 0;

        float expectedHue;
        float S;
        float V;
        Color.RGBToHSV(expectedColor, out expectedHue, out S, out V);

        foreach (Decoration decoration in decorations)
        {
            float colorHue;
            Color.RGBToHSV(decoration.SpriteRenderer.color, out colorHue, out S, out V);

            if (InBetweenLimits(expectedHue, colorHue))
                inColor++;

            totalColors++;
        }

        progress = (inColor / totalColors) / percentageInColor;
    }

    private float GetLowerColorLimit(float expectedHue)
    {
        float lowerLimit = expectedHue - colorFlexibility;
        if (lowerLimit < 0)
            lowerLimit = 1 - lowerLimit;

        return lowerLimit;
    }

    private float GetUpperColorLimit(float expectedHue)
    {
        float upperLimit = expectedHue + colorFlexibility;
        if (upperLimit > 1)
            upperLimit = upperLimit - 1;

        return upperLimit;
    }

    private bool InBetweenLimits(float expectedHue, float hue)
    {
        if (GetLowerColorLimit(expectedHue) < GetUpperColorLimit(expectedHue))
        {
            if (hue >= GetLowerColorLimit(expectedHue) && hue <= GetUpperColorLimit(expectedHue))
                return true;
        }
        else
        {
            if (hue >= 0 && hue <= GetUpperColorLimit(expectedHue))
                return true;
            else if (hue <= 1 && hue >= GetLowerColorLimit(expectedHue))
                return true;
        }

        return false;
    }
}
