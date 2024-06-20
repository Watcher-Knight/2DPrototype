using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu(ComponentPaths.PlayerAnimator)]
public class PlayerAnimator : Animator2D
{
    [SerializeField] private SpriteRenderer Body;
    [SerializeField] private AnimationClip BodyIdle;
    [SerializeField] private SpriteRenderer Legs;
    [SerializeField] private AnimationClip LegsWalk;
    [SerializeField] private AnimationClip LegsIdle;
    [SerializeField] private SpriteRenderer Arm;
    [Tooltip("In frames per second")]
    [SerializeField] private float Speed = 12f;
    [SerializeField][AutoAssign] private Collider2D Collider;

    public float BodyDirection { get; private set; } = 1;
    public Vector2 AimDirection { get; private set; } = Vector2.zero;

    [Button]
    public void Aim(Vector2 direction)
    {
        AimDirection = direction;
        direction = direction == Vector2.zero ? Vector2.right * BodyDirection : direction;
        Vector2 up = direction.Rotate(-90);
        Arm.transform.rotation = Quaternion.LookRotation(Vector3.forward, up);
        switch (direction.x)
        {
            case > 0:
                Body.flipX = false;
                if (Arm.transform.localScale.y < 0) Arm.FlipY();
                break;
            case < 0:
                Body.flipX = true;
                if (Arm.transform.localScale.y > 0) Arm.FlipY();
                break;

        }
        // if (!Body.flipX && Arm.transform.right.x < 0) Body.flipX = true;
        // if (Body.flipX && Arm.transform.right.x > 0) Body.flipX = false;
    }
    private void Start()
    {
        Idle();
    }


    [Button]
    public void Idle()
    {
        PlayClip(Legs, LegsIdle, Speed);
        PlayClip(Body, BodyIdle, Speed);
    }

    [Button]
    public void Walk()
    {
        if (GetClip(Legs) != LegsWalk)
            PlayClip(Legs, LegsWalk, Speed);
    }

    [Button]
    public void TurnRight()
    {
        Legs.flipX = false;
        BodyDirection = 1;
        Aim(AimDirection);
    }

    [Button]
    public void TurnLeft()
    {
        Legs.flipX = true;
        BodyDirection = -1;
        Aim(AimDirection);
    }

    private void Update()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        if (Collider.IsTouching(Vector2.down, Physics.AllLayers))
        {
            if (Mathf.Abs(rigidbody.velocity.x) > 0.1f)
                Walk();
                else Idle();
        }
        else Idle();
    }
}