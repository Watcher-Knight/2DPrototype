using System;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
[AddComponentMenu(ComponentPaths.Grappler)]
public class GrapplerBehavior : MonoBehaviour
{
    [SerializeField] LayerMask GrapplePointLayer;
    [SerializeField] EventTag SelectionTag;
    [SerializeField] EventTag DeselectionTag;

    private DistanceJoint2D b_Joint;
    private DistanceJoint2D Joint => b_Joint ??= GetComponent<DistanceJoint2D>();
    public Action OnFinish;
    private Collider2D TargetCollider;
    private bool IsGrappling = false;

    private Vector2 TargetPosition = Vector2.zero;

    private void Awake()
    {
        Joint.enabled = false;
    }

    private void Update()
    {

    }

    private void Finish()
    {
        OnFinish?.Invoke();
    }

    public void Grapple(Vector2 target)
    {
        IsGrappling = true;
        Joint.enabled = true;
        Joint.connectedAnchor = target;
    }
    public void Cancel()
    {
        if (IsGrappling)
        {
            Joint.enabled = false;
            IsGrappling = false;
            Finish();
        }
    }

    public void Move()
    {

    }
    public void Jump()
    {

    }
}