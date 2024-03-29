using System.Linq;
using UnityEngine;

public static class Vector2Extensions
{
    public static Collider2D[] OverlapSemicircle(this Vector2 position, Vector2 direction, float distance, float angle, LayerMask layer)
    {
        if (direction == Vector2.zero) return new Collider2D[0];
        Collider2D[] cirlceHits = Physics2D.OverlapCircleAll(position, distance, layer);

        bool isInArea(Collider2D c) =>
            c.OverlapsLine(position, position + direction.Rotate(-angle / 2) * distance) ||
            c.OverlapsLine(position, position + direction.Rotate(angle / 2) * distance) ||
            Vector2.Angle(direction, c.ClosestPoint(position) - position) < angle / 2;

        return cirlceHits.Where(isInArea).ToArray();
    }
    public static Collider2D[] OverlapSemicircle(this Vector3 position, Vector2 direction, float distance, float angle, LayerMask layer) =>
        ((Vector2) position).OverlapSemicircle(direction, distance, angle, layer);

    public static void DrawSemicircle(this Vector2 position, Vector2 direction, float distance, float angle)
    {
        Vector2 point1 = position + direction.Rotate(-angle / 2) * distance;
        Vector2 point2 = position + direction.Rotate(angle / 2) * distance;
        Gizmos.DrawLine(position, point1);
        Gizmos.DrawLine(position, point2);
        Gizmos.DrawLine(point1, point2);
    }
    public static void DrawSemicircle(this Vector3 position, Vector2 direction, float distance, float angle) =>
        ((Vector2) position).DrawSemicircle(direction, distance, angle);


    public static Vector2 Rotate(this Vector2 vector, float degrees)
    {
        Quaternion rotation = Quaternion.Euler(0, 0, -degrees);
        return rotation * vector;
    }
}