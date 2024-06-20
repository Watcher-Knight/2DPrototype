using System;

public class PlayerModeStateMachine
{
    private PlayerMode b_CurrentState = PlayerMode.Attack;
    public PlayerMode CurrentState
    {
        get => b_CurrentState;
        set
        {
            b_CurrentState = value;
            OnStateChange?.Invoke(value);
        }
    }

    public Action<PlayerMode> OnStateChange;

    public void ToAttack() => CurrentState = PlayerMode.Attack;
    public void ToMagnet() => CurrentState = PlayerMode.Magnet;
    public void ToGrapple() => CurrentState = PlayerMode.Grapple;
    public void ToDash() => CurrentState = PlayerMode.Dash;
}

public enum PlayerMode
{
    Attack,
    Magnet,
    Grapple,
    Dash
}