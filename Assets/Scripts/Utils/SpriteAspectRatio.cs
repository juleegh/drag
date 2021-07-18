using UnityEngine;

public static class SpriteAspectRatio
{
    public static float GetDimension(Sprite sprite)
    {
        return sprite.rect.width > sprite.rect.height ? sprite.rect.width : sprite.rect.height;
    }

    public static float GetAspectRatio(float dimension)
    {
        if (dimension <= 30)
            return 0.2f;
        else if (dimension <= 100)
            return 0.3f;
        else if (dimension <= 250)
            return 0.5f;
        return 1f;
    }
}