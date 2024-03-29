using System;
using UnityEngine;

public class MovementStateMachine
{
    public bool CanMove => CurrentState switch
    {
        MovementState.Crouch => false,
        _ => true
    };
    private bool IsGrounded(BoxCollider2D collider, LayerMask platformLayer) =>
        collider.IsTouching(Vector2.down, platformLayer);
    private MovementState b_CurrentSTate = MovementState.Default;
    public MovementState CurrentState
    {
        get => b_CurrentSTate;
        private set
        {
            b_CurrentSTate = value;
            OnStateChange?.Invoke(value);
        }
    }
    public Action<MovementState> OnStateChange;

    public bool ToJump(BoxCollider2D collider, LayerMask platformLayer)
    {
        switch (CurrentState)
        {
            case MovementState.Default:
                if (IsGrounded(collider, platformLayer))
                {
                    CurrentState = MovementState.Jump;
                    return true;
                }
                break;
            case MovementState.Grapple:
                CurrentState = MovementState.Jump;
                return true;
        }
        return false;
    }
    public bool ToCrouch(BoxCollider2D collider, LayerMask platformLayer)
    {
        switch (CurrentState)
        {
            case MovementState.Default:
                if (IsGrounded(collider, platformLayer))
                {
                    CurrentState = MovementState.Crouch;
                    return true;
                }
                break;
        }
        return false;
    }
    public bool ToGrapple()
    {
        switch (CurrentState)
        {
            case MovementState.Default:
            case MovementState.Jump:
                CurrentState = MovementState.Grapple;
                return true;
        }
        return false;
    }

    public void ToDefault() => CurrentState = MovementState.Default;
}

public enum MovementState
{
    Default,
    Jump,
    Crouch,
    Grapple
}