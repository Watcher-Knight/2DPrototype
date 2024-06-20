using System;
using UnityEngine;

public class MovementStateMachine
{
    public bool CanMove => CurrentState switch
    {
        MovementState.Crouch => false,
        MovementState.Grapple => false,
        MovementState.Magnet => false,
        _ => true
    };
    public bool CanAim => CurrentState switch
    {
        MovementState.Grapple => false,
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
            if (CanMove) OnMovementEnabled?.Invoke();
            OnStateChange?.Invoke(value);
        }
    }
    public Action<MovementState> OnStateChange;
    public Action OnMovementEnabled;

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
    public bool ToGrapple(BoxCollider2D collider, LayerMask platformLayer)
    {
        switch (CurrentState)
        {
            case MovementState.Default:
            case MovementState.Jump:
            if (!IsGrounded(collider, platformLayer))
            {
                CurrentState = MovementState.Grapple;
                return true;
            }
            break;
        }
        return false;
    }
    public bool ToMagnet()
    {
        switch (CurrentState)
        {
            case MovementState.Default:
            case MovementState.Jump:
                CurrentState = MovementState.Magnet;
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
    Grapple,
    Magnet
}