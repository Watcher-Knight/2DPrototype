using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu(ComponentPaths.Jumper)]
public class JumperBehavior : MonoBehaviour
{
    [SerializeField] private JumperData Data;

    private Rigidbody2D Rigidbody;
    private bool IsJumping = false;

    public Action OnFinish;

    private void OnEnable()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        if (IsJumping && Rigidbody.velocity.y < 0f) Cancel();
    }

    public void Jump()
    {
        Rigidbody.velocity = Vector2.right * Rigidbody.velocity.x;
        Rigidbody.AddForce(Vector2.up * Data.Force, ForceMode2D.Impulse);
        IsJumping = true;
    }
    public void Cancel()
    {
        if (IsJumping)
        {
            Rigidbody.AddForce(Data.JumpCut * Rigidbody.velocity.y * Vector2.down, ForceMode2D.Impulse);
            Finish();
        }
    }

    private void Finish()
    {
        OnFinish?.Invoke();
        IsJumping = false;
    }
}