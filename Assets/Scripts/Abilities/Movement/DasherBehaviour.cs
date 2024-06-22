using System;
using System.Collections;
using Unity.Plastic.Newtonsoft.Json.Bson;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent (typeof(TrailRenderer))]
[AddComponentMenu(ComponentPaths.Dasher)]
public class DasherBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerAnimator PlayerAnimator;
    [SerializeField] private TrailRenderer TrailRenderer;
    [SerializeField] private DasherData Data;

    public Action OnFinish;
    private Rigidbody2D Rigidbody;
    private bool IsDashing = false;
    private Vector2 Direction;

    private float Timer = 0f;
    private float LastDashTime = Mathf.NegativeInfinity;
    private bool CanDash => !IsDashing && Time.time >= LastDashTime + Data.DashCooldown;

    private void OnEnable()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (IsDashing)
        {
            Timer += Time.deltaTime;
            if (Timer < Data.DashDuration)
            {
                Rigidbody.velocity = Direction * (Data.DashDistance / Data.DashDuration);
            } else
            {
                EndDash();
                Finish();
            }
        }
    }

    public void Dash()
    {
        if (CanDash)
        {
            StartDash();
        } else
        {
            Finish();
        }
    }

    private void Finish() => OnFinish?.Invoke();

    private void StartDash()
    {
        IsDashing = true;
        if (PlayerAnimator.AimDirection != Vector2.zero)
        {
            Direction = PlayerAnimator.AimDirection.x >= 0 ? transform.right : -transform.right;
        } else
        {
            Direction = new Vector2(PlayerAnimator.BodyDirection, 0f);
        }
        if (Data.ShowTrail)
        {
            TrailRenderer.emitting = true;
        }
    }

    private void EndDash()
    {
        IsDashing = false;
        Rigidbody.velocity = Vector2.zero;
        Timer = 0f;
        LastDashTime = Time.time;
        if (Data.ShowTrail)
        {
            TrailRenderer.emitting = false;
            TrailRenderer.Clear();
        }
    }
}