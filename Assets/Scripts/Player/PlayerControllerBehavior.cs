using UnityEngine;

[UpdateEditor]
[SelectionBase]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(MoverBehavior))]
[RequireComponent(typeof(JumperBehavior))]
[RequireComponent(typeof(CroucherBehavior))]
[AddComponentMenu(ComponentPaths.PlayerController)]
public class PlayerControllerBehavior : MonoBehaviour, IEventListener
{
    [SerializeField] private AnimatorBoolParameter CrouchParameter;
    [SerializeField] LayerMask PlatformLayer;
    [SerializeField] EventTag ExitTag;
    [DisplayPlayMode] MovementState CurrentState => StateMachine.CurrentState;
    private MovementStateMachine StateMachine;

    private void OnEnable()
    {
        MoverBehavior mover = GetComponent<MoverBehavior>();
        JumperBehavior jumper = GetComponent<JumperBehavior>();
        CroucherBehavior croucher = GetComponent<CroucherBehavior>();

        StateMachine = new MovementStateMachine(jumper, croucher);
        InitializeControls(mover, jumper, croucher);
    }
    private void InitializeControls(MoverBehavior mover, JumperBehavior jumper, CroucherBehavior croucher)
    {
        GameInput.PlayerActions controls = new GameInput().Player;
        BoxCollider2D collider = GetComponent<BoxCollider2D>();
        controls.Enable();

        controls.Move.performed += c => { if (StateMachine.CanMove) mover.Move(c.ReadValue<float>()); };
        controls.Move.canceled += c => mover.Move(0f);
        StateMachine.OnStateChange += s => {if (s == MovementState.Crouch) mover.Move(0f); };

        controls.Jump.performed += c => { if (StateMachine.ToJump(collider, PlatformLayer)) jumper.Jump(); };
        controls.Jump.canceled += c => jumper.Cancel();

        controls.Crouch.performed += c => { if (StateMachine.ToCrouch(collider, PlatformLayer)) croucher.Crouch(); };
        controls.Crouch.canceled += c => croucher.Cancel();
    }

    public void Invoke(EventTag tag)
    {
        if (tag == ExitTag)
        {
            Application.Quit();
            Debug.Log("Exit");
        }
    }
}
