using System;
using System.Linq;
using UnityEngine;

public class RangeTargeter : Targeter
{
    public Collider2D CalculateTarget(Vector2 origin, Vector2 direction,float range, float angle, LayerMask targetLayer)
    {
        Collider2D newTarget = null;

        if (direction != Vector2.zero)
        {
            Collider2D[] colliders = origin.OverlapSemicircle(direction, range, angle, targetLayer);
            float distanceFromLine(Collider2D c)
            {
                float radius = Vector2.Distance(origin, c.transform.position);
                float angle = Vector2.Angle(direction, (Vector2) c.transform.position - origin);
                return radius * angle * Mathf.Deg2Rad;
            }
            newTarget = colliders.OrderBy(distanceFromLine).FirstOrDefault();
        }

        if (Target != newTarget) Target = newTarget;
        return newTarget;
    }

    public void Draw(Vector2 origin, Vector2 direction, float range, float angle)
    {
        Gizmos.color = Color.red;
        origin.DrawSemicircle(direction, range, angle);
    }
}