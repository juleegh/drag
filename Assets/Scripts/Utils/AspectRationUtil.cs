using UnityEngine;

public static class AspectRatioUtil
{
    private static float GetDimension(Sprite sprite)
    {
        return sprite.rect.width > sprite.rect.height ? sprite.rect.width : sprite.rect.height;
    }

    public static float GetSpriteAspectRatio(Sprite sprite)
    {
        float dimension = GetDimension(sprite);

        if (dimension <= 30)
            return 0.2f;
        else if (dimension <= 100)
            return 0.3f;
        else if (dimension <= 250)
            return 0.5f;
        return 1f;
    }

    public static float GetColliderAspectRatio(Sprite sprite)
    {
        float dimension = GetDimension(sprite);

        if (dimension <= 30)
            return 0.2f;
        else if (dimension <= 400)
            return dimension * 0.01f;
        else
            return 0.5f;
    }
}