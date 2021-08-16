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

    public static Color ColorFromString(string color)
    {
        string[] components = color.Split(',');
        return new Color(float.Parse(components[0]), float.Parse(components[1]), float.Parse(components[2]), 1);
    }

    public static string StringFromColor(Color color)
    {
        return color.r + "," + color.g + "," + color.b;
    }
}