using UnityEngine;

public static class BoundsExtensions
{
    public static Vector2 BottomCenter(this Bounds bounds) =>
        bounds.center - new Vector3(0, bounds.extents.y, 0);

    public static void Draw(this Bounds bounds) =>
        Gizmos.DrawWireCube(bounds.center, bounds.size);
}