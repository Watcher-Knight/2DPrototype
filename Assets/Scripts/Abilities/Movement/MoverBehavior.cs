using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
[AddComponentMenu(ComponentPaths.Mover)]
public class MoverBehavior : MonoBehaviour
{
    [SerializeField][AutoAssign] private MoverData Data;
    [SerializeField][AutoAssign] private Collider2D Collider;

    private float b_Control = 1;
    private float Control
    {
        get => b_Control;
        set => b_Control = Mathf.Clamp01(value);
    }
    public void Disable()
    {
        StopAllCoroutines();
        Control = 0;
    }
    public void Enable()
    {
        StopAllCoroutines();
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        IEnumerator task()
        {
            while (Control < 1)
            {
                float difference = Mathf.Abs(rigidbody.velocity.x - Data.Speed * Direction);
                float setSpeed = Time.deltaTime / difference * Data.RegainControlStrength;
                Control = Mathf.MoveTowards(Control, 1, setSpeed);
                if (Collider.IsTouching(Vector2.down, Physics.AllLayers)) Control = 1;
                yield return null;
            }
        }
        StartCoroutine(task());
    }

    private Mover Mover;
    private float Direction { get; set; } = 0f;

    private void OnEnable()
    {
        Mover = new()
        {
            Rigidbody = GetComponent<Rigidbody2D>(),
            Speed = Data.Speed,
            Acceleration = Data.Acceleration,
            Deceleration = Data.Deceleration
        };
    }

    public void Move(float direction)
    {
        Direction = direction;

        if (Control > 0)
        {
            PlayerAnimator animator = GetComponent<PlayerAnimator>();
            switch (direction)
            {
                case > 0: animator.TurnRight(); break;
                case < 0: animator.TurnLeft(); break;
            }
        }
    }

    private void FixedUpdate()
    {
        Mover.Move(Direction, Control);
    }
}