using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu(ComponentPaths.Dasher)]
public class DasherBehaviour : MonoBehaviour
{
    [SerializeField] private DasherData Data;
    [SerializeField] [AutoAssign] private Transform Origin;

    private Rigidbody2D Rigidbody;
    private float StartTime;
    private float holdDurationTime;
    public bool CanDash { get; private set; }
    private bool IsDashing = false;
    public Action OnFinish;

    private void OnEnable()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        holdDurationTime = Data.dashCooldown;
    }
    public void Update()
    {
        if (IsDashing || (IsDashing && !CanDash)) Cancel();
        if(Time.time >= StartTime + holdDurationTime)
        {
            CanDash = true;
        }
    }

    public void Dash()
    {
        if (CanDash) {
            StartTime = Time.time;
            Rigidbody.AddForce(Data.DashForce * Rigidbody.velocity, ForceMode2D.Impulse);
            IsDashing = true;
            CanDash = false;
        }
    }

    public void Cancel()
    {
            Rigidbody.AddForce(Data.DragForce * Vector2.left);
            Finish();
    }
    private void Finish()
    {
        OnFinish?.Invoke();
        IsDashing = false;
    }

}
