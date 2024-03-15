using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu(ComponentPaths.Mover)]
public class MoverBehavior : MonoBehaviour
{
    [SerializeField] private MoverData Data;

    private Rigidbody2D Rigidbody;
    private float Direction { get; set; } = 0f;

    private void OnEnable()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Move(float value) => Direction = value;

    private void FixedUpdate()
    {
        float targetSpeed = Math.Sign(Direction) * Data.Speed;
        float speedDifference = targetSpeed - Rigidbody.velocity.x;
        float accelerationPercent = (Mathf.Abs(targetSpeed) > 0) ? Data.Acceleration : Data.Deceleration;
        float accelerationRate = accelerationPercent / Time.fixedDeltaTime;
        float force = speedDifference * accelerationRate;
        Rigidbody.AddForce(Vector2.right * force);
    }
}