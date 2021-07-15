using UnityEngine;

public static class ColorConversion
{
    public static Vector3 VectorFromColor(Color color)
    {
        return new Vector3(color.r, color.g, color.b);
    }

    public static Color ColorFromVector(Vector3 color)
    {
        return new Color(color.x, color.y, color.z, 1);
    }
}