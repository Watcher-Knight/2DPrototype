using System;
using System.Linq;
using UnityEngine;

public class TargeterBehavior : MonoBehaviour
{
    [SerializeField] private Collider2D Targeter;
    [SerializeField] private ContactFilter2D TargetFilter;
    [SerializeField] private EventTag SelectionTag;
    [SerializeField] private EventTag DeselectionTag;
    private Vector2 Direction = Vector2.zero;
    private Collider2D b_Target;
    public Collider2D Target
    {
        get => b_Target;
        private set
        {
            if (value != b_Target)
            {
                ChangeTarget(b_Target, value);
                b_Target = value;
            }
        }
    }
    public Action<Collider2D> OnTargetChange;
    private void ChangeTarget(Collider2D oldTarget, Collider2D newTarget)
    {
        OnTargetChange?.Invoke(newTarget);

        oldTarget?.SendMessage(DeselectionTag);
        newTarget?.SendMessage(SelectionTag);
    }

    private void Awake()
    {
        if (Targeter != null) Targeter.enabled = false;
    }

    private void Update()
    {
        CalculateTarget();
    }

    public void Aim(Vector2 direction)
    {
        if (Targeter == null) return;
        if (direction != Vector2.zero)
        {
            Targeter.transform.up = direction.normalized;
            Physics2D.SyncTransforms();
            if (!Targeter.enabled) Targeter.enabled = true;
        }
        else if (Targeter.enabled) Targeter.enabled = false;
    }

    private void CalculateTarget()
    {
        if (Targeter == null) return;

        Collider2D[] colliders = Targeter.OverlapCollider(TargetFilter);
        Collider2D newTarget = null;

        if (colliders.Length > 0)
        {
            Vector2 origin = Targeter.transform.position;
            Vector2 targetDirection = Targeter.transform.forward;
            float distanceFromLine(Collider2D c)
            {
                float radius = Vector2.Distance(origin, c.transform.position);
                float angle = Vector2.Angle(targetDirection, (Vector2) c.transform.position - origin);
                return radius * angle * Mathf.Deg2Rad;
            }
            newTarget = colliders.OrderBy(distanceFromLine).FirstOrDefault();
        }

        if (Target != newTarget) Target = newTarget;
    }
}