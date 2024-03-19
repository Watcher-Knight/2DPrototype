using System;
using UnityEngine;

[RequireComponent(typeof(DistanceJoint2D))]
[AddComponentMenu(ComponentPaths.Grappler)]
public class GrapplerBehavior : MonoBehaviour
{
    private DistanceJoint2D b_Jiont;
    private DistanceJoint2D Joint => b_Jiont ??= GetComponent<DistanceJoint2D>();
    public Action OnFinish;

    private Vector2 TargetPosition = Vector2.zero;

    private void Awake()
    {
        Joint.enabled = false;
    }

    private void Update()
    {

    }

    public void Grapple()
    {

    }
    public void Cancel()
    {

    }
}