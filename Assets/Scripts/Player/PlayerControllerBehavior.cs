using UnityEngine;

[UpdateEditor]
[SelectionBase]
[RequireComponent(typeof(MoverBehavior))]
[RequireComponent(typeof(JumperBehavior))]
[RequireComponent(typeof(CroucherBehavior))]
[RequireComponent(typeof(GrapplerBehavior))]
//[RequireComponent(typeof(TargeterBehavior))]
[AddComponentMenu(ComponentPaths.PlayerController)]
public class PlayerControllerBehavior : MonoBehaviour
{
    [SerializeField] BoxCollider2D Collider;
    [SerializeField] LayerMask PlatformLayer;
    [SerializeField] EventTag ExitTag;
    [DisplayPlayMode] MovementState CurrentState => StateMachine.CurrentState;
    private MovementStateMachine StateMachine;
    private GameInput.PlayerActions Controls;

    private void OnEnable()
    {
        StateMachine = new();
        InitializeControls();
    }
    private void OnDisable()
    {
        Controls.Disable();
    }
    private void InitializeControls()
    {
        Controls = new GameInput().Player;
        Controls.Enable();

        MoverBehavior mover = GetComponent<MoverBehavior>();
        Controls.Move.performed += c => { if (StateMachine.CanMove) mover.Move(c.ReadValue<float>()); };
        Controls.Move.canceled += c => mover.Move(0f);
        StateMachine.OnStateChange += s => { if (s == MovementState.Crouch) mover.Move(0f); };

        JumperBehavior jumper = GetComponent<JumperBehavior>();
        Controls.Jump.performed += c => { if (StateMachine.ToJump(Collider, PlatformLayer)) jumper.Jump(); };
        Controls.Jump.canceled += c => jumper.Cancel();
        jumper.OnFinish += StateMachine.ToDefault;

        CroucherBehavior croucher = GetComponent<CroucherBehavior>();
        Controls.Crouch.performed += c => { if (StateMachine.ToCrouch(Collider, PlatformLayer)) croucher.Crouch(); };
        Controls.Crouch.canceled += c => croucher.Cancel();
        croucher.OnFinish += StateMachine.ToDefault;

        GrapplerBehavior grappler = GetComponent<GrapplerBehavior>();
        Controls.Grapple.performed += c =>
        {
            if (grappler.CanGrapple && StateMachine.ToGrapple())
            {
                grappler.Grapple();
                mover.Control = 0f;
            }
        };
        Controls.Grapple.canceled += c =>
        {
            grappler.Cancel();
        };
        Controls.Aim.performed += c => grappler.Aim(c.ReadValue<Vector2>());
        Controls.Aim.canceled += c => grappler.Aim(Vector2.zero);
        grappler.OnFinish += () =>
        {
            StateMachine.ToDefault();
            mover.Control = 1f;
        };
        StateMachine.OnStateChange += s =>
        {
            if (s == MovementState.Grapple)
            {
                Controls.Move.performed += c => grappler.Swing(c.ReadValue<float>());
                Controls.Move.canceled += c => grappler.Swing(0f);
                Controls.Climb.performed += c => grappler.Climb(c.ReadValue<float>());
                Controls.Climb.canceled += c => grappler.Climb(0f);
                Controls.Jump.performed += c => grappler.Cancel();
            }
            else
            {
                Controls.Move.performed -= c => grappler.Swing(c.ReadValue<float>());
                Controls.Move.canceled -= c => grappler.Swing(0f);
                Controls.Climb.performed -= c => grappler.Climb(c.ReadValue<float>());
                Controls.Climb.canceled -= c => grappler.Climb(0f);
                Controls.Jump.performed -= c => grappler.Cancel();
            }
        };
    }

    private void SetMoveControl(float value, float speed)
    {

    }

    private void Update()
    {

    }
}
