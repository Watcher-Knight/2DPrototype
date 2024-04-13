using System;
using UnityEngine;

public class MagnetBehavior : MonoBehaviour
{
    [SerializeField] private MagnetData Data;
    Rigidbody2D b_Rigidbody;
    private Rigidbody2D Rigidbody => b_Rigidbody ??= GetComponent<Rigidbody2D>();

    private bool IsMagnetized = false;
    private Collider2D TargetCollider;
    private Vector2 AimDirection;
    private float MoveDirection;
    private Mover Mover;
    private float DefaultGravity = 0;
    public Action OnFinish;

    public bool CanMagnetize => !IsMagnetized && TargetCollider != null;

    private void ChangeTarget(Collider2D newTarget)
    {
        TargetCollider?.SendMessage(Data.DeselectionTag);
        TargetCollider = newTarget;
        TargetCollider?.SendMessage(Data.SelectionTag);
    }

    private void OnEnable()
    {
        DefaultGravity = Rigidbody.gravityScale;
        Mover = new()
        {
            Speed = Data.Mover.Speed,
            Acceleration = Data.Mover.Acceleration,
            Deceleration = Data.Mover.Deceleration,
            Rigidbody = Rigidbody
        };
    }
    private void Update()
    {
        if (!IsMagnetized) CalculateTarget();
    }
    private void FixedUpdate()
    {
        if (IsMagnetized)
        {
            MoveTowards();
            if (Rigidbody.IsTouching(TargetCollider)) MoveAlong();
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        Mover.Axis = -Vector2.Perpendicular(other.GetContact(0).normal);
    }

    private void CalculateTarget()
    {
        Collider2D newCollider = null;
        if (AimDirection != Vector2.zero)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, AimDirection, Data.Range, Data.MagnetLayer);
            newCollider = hit.collider;
        }
        if (TargetCollider != newCollider) ChangeTarget(newCollider);
    }
    private void MoveAlong()
    {
        Mover.Move(MoveDirection);
    }
    private void MoveTowards()
    {
        Rigidbody.AddForce((TargetCollider.ClosestPoint(transform.position) - (Vector2) transform.position).normalized * Data.Force, ForceMode2D.Force);
    }


    public void Pull()
    {
        if (!CanMagnetize) return;

        IsMagnetized = true;
        Rigidbody.gravityScale = 0;
    }
    public void Release()
    {
        if (!IsMagnetized) return;

        IsMagnetized = false;
        Rigidbody.gravityScale = DefaultGravity;
        OnFinish?.Invoke();
    }
    public void Slide(float direction)
    {
        MoveDirection = direction;
    }
    public void Aim(Vector2 direction) => AimDirection = direction;

    private void OnDrawGizmosSelected()
    {
        if (Data == null) return;
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + AimDirection * Data.Range);
    }
}