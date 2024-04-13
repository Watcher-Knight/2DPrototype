using UnityEngine;

public class Mover
{
    public float Speed;
    public float Acceleration;
    public float Deceleration;
    public Vector2 Axis = Vector2.right;

    public Rigidbody2D Rigidbody;
    public void Move(float direction, float control = 1)
    {
        if (Rigidbody == null)
        {
            Debug.LogWarning("Rigidbody is null, cannot move");
            return;
        }
        float targetSpeed = direction * Speed;
        float speedDifference = targetSpeed - Rigidbody.velocity.DirectionalMagnitude(Axis);
        float accelerationPercent = (targetSpeed > 0) ? Acceleration : Deceleration;
        float accelerationRate = accelerationPercent / Time.fixedDeltaTime;
        float force = accelerationRate * Mathf.Clamp01(control) * speedDifference;
        Rigidbody.AddForce(Axis * force);
    }
}