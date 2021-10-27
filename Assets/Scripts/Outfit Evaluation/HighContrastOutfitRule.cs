using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighContrastOutfitRule : OutfitRule
{
    [SerializeField] private float contrastGoal;
    public override string Prompt { get { return "Color contrast"; } }

    public override void Evaluate(List<Decoration> decorations)
    {
        List<float> colorValues = new List<float>();
        foreach (Decoration decoration in decorations)
        {
            float h; float s; float v;
            Color.RGBToHSV(decoration.SpriteRenderer.color, out h, out s, out v);
            colorValues.Add(v);
        }

        progress = FindDeviation(colorValues) / contrastGoal;

        if (progress > 1f)
            progress = 1f;
    }

    private float FindDeviation(List<float> nums)
    {
        float varianceValue = FindVariance(nums);
        return standardDeviation(varianceValue);
    }

    private float FindVariance(List<float> nums)
    {
        if (nums.Count > 1)
        {
            float avg = getAverage(nums);
            float sumOfSquares = 0.0f;

            foreach (int num in nums)
                sumOfSquares += Mathf.Pow((num - avg), 2.0f);

            return sumOfSquares / (float)(nums.Count - 1);
        }
        else { return 0.0f; }
    }

    private float standardDeviation(float variance)
    {
        return Mathf.Sqrt(variance);
    }

    private float getAverage(List<float> nums)
    {
        float sum = 0;
        if (nums.Count > 1)
        {
            foreach (int num in nums)
                sum += num;

            return sum / (float)nums.Count;
        }
        else { return (float)nums[0]; }
    }

}
