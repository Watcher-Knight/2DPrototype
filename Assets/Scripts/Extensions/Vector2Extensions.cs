using UnityEngine;

public static class Vector2Extensions
{
    public static RaycastHit2D[] ConeCastAll(this Vector2 position, Vector2 direction, float angle, float distance, LayerMask layer)
    {
        RaycastHit2D[] circleCasts = Physics2D.CircleCastAll(position, 0.1f, direction, distance, layer);
        return circleCasts;
    }
}