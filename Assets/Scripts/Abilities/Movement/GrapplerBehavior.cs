using System;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(PlayerAnimator))]
[AddComponentMenu(ComponentPaths.Grappler)]
public class GrapplerBehavior : MonoBehaviour
{
    [SerializeField] private GrapplerData Data;
    [SerializeField][AutoAssign] private Transform Origin;

    public bool CanGrapple => !IsGrappling && TargetCollider != null;

    private DistanceJoint2D b_Joint;
    private DistanceJoint2D Joint => b_Joint ??= GetComponent<DistanceJoint2D>();
    private LineRenderer b_Line;
    private LineRenderer Line => b_Line ??= GetComponent<LineRenderer>();
    private Rigidbody2D b_Rigidbody;
    private Rigidbody2D Rigidbody => b_Rigidbody ??= GetComponent<Rigidbody2D>();

    public Action OnFinish;

    private bool IsGrappling = false;
    private float ClimbDirection = 0f;
    private float SwingDirection = 0f;
    //private Vector2 AimDirection = Vector2.zero;
    private Collider2D TargetCollider;
    private Vector2 TargetPoint = Vector2.zero;
    private SpriteRenderer TargetIcon;

    private void ChangeTarget(Collider2D newTarget)
    {
        TargetCollider?.SendMessage(Data.DeselectionTag);
        TargetCollider = newTarget;
        TargetCollider?.SendMessage(Data.SelectionTag);
    }

    private void Awake()
    {
        Joint.enabled = false;
        Line.enabled = false;
    }

    private void Update()
    {
        if (IsGrappling)
        {
            if (SwingDirection == 0) UpdateClimb();
            DrawLine();
            AimTowardTarget();
        }
        else CalculateTarget();

        if (Rigidbody.IsTouching(Vector2.down, Physics2D.AllLayers)) Cancel();
    }
    private void FixedUpdate()
    {
        if (IsGrappling)
        {
            UpdateSwing();
        }
    }

    private void UpdateSwing()
    {
        //if (Vector2.Angle(Joint.connectedAnchor - (Vector2) transform.position, Vector2.up) < 90f)
        Rigidbody.AddForce(SwingDirection * Data.SwingForce * Vector2.right);
    }
    private void UpdateClimb()
    {
        Joint.distance = Mathf.Clamp(Joint.distance + -ClimbDirection * Data.ClimbSpeed * Time.deltaTime, Data.MinLength, Data.MaxLength);
    }

    private void DrawLine()
    {
        Line.SetPosition(0, Origin.position);
        Line.SetPosition(1, Joint.connectedAnchor);
    }

    private void AimTowardTarget()
    {
        PlayerAnimator animator = GetComponent<PlayerAnimator>();
        animator.Aim(TargetPoint - (Vector2)transform.position);
    }

    private void CalculateTarget()
    {
        Collider2D newCollider;
        RaycastHit2D hit = Physics2D.Raycast(Origin.position, Origin.right, Data.MaxLength, Data.TargetLayer);
        if (hit && hit.collider.TryGetComponent(out GrapplePointBehavior _))
        {
            newCollider = hit.collider;
            TargetPoint = hit.point;
            if (TargetIcon == null) TargetIcon = Instantiate(Data.TargetIcon, TargetPoint, Quaternion.identity);
            else TargetIcon.transform.position = TargetPoint;
        }
        else
        {
            newCollider = null;
            if (TargetIcon != null) Destroy(TargetIcon.gameObject);
        }
        if (TargetCollider != newCollider) ChangeTarget(hit.collider);
    }

    private void Finish()
    {
        OnFinish?.Invoke();
    }

    public void Grapple()
    {
        if (CanGrapple)
        {
            IsGrappling = true;
            Joint.enabled = true;
            Line.enabled = true;
            Joint.connectedAnchor = TargetPoint;
        }
    }
    public void Cancel()
    {
        if (IsGrappling)
        {
            Joint.enabled = false;
            Line.enabled = false;
            IsGrappling = false;
            Finish();
        }
    }

    public void Swing(float direction)
    {
        SwingDirection = direction;

        PlayerAnimator animator = GetComponent<PlayerAnimator>();
        switch (direction)
        {
            case > 0: animator.TurnRight(); break;
            case < 0: animator.TurnLeft(); break;
        }
    }
    public void Climb(float direction)
    {
        ClimbDirection = direction;
    }
    // public void Aim(Vector2 direction)
    // {
    //     AimDirection = direction;
    // }
    private void OnDrawGizmosSelected()
    {
        if (Data == null) return;
        Gizmos.color = Color.green;
        Gizmos.DrawLine(Origin.position, Origin.position + Origin.right * Data.MaxLength);
    }
}