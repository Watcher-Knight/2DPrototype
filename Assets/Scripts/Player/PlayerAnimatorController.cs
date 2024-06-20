using UnityEngine;

[AddComponentMenu(ComponentPaths.PlayerAnimatorController)]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimatorController : MonoBehaviour
{
    [SerializeField][AutoAssign] private Collider2D Collider;

    private void Update()
    {
        PlayerAnimator animator = GetComponent<PlayerAnimator>();
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();

        if (Mathf.Abs(rigidbody.velocity.x) > 0.1f && Collider.IsTouching(Vector2.down, Physics.AllLayers))
        {
            animator.Walk();
        }
        else
        {
            animator.Idle();
        }
    }
}