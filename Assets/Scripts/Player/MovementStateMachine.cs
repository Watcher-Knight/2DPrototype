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
        collider.IsTouching(Direction.Down, platformLayer);
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

    public MovementStateMachine(JumperBehavior jumper, CroucherBehavior croucher)
    {
        jumper.OnFinish = ToDefault;
        croucher.OnFinish = ToDefault;
    }

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

    private void ToDefault() => CurrentState = MovementState.Default;
}

public enum MovementState
{
    Default,
    Jump,
    Crouch
}