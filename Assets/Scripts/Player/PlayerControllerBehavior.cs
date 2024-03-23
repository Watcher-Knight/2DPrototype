using UnityEngine;
using UnityEngine.SceneManagement;

[UpdateEditor]
[SelectionBase]
[RequireComponent(typeof(MoverBehavior))]
[RequireComponent(typeof(JumperBehavior))]
[RequireComponent(typeof(CroucherBehavior))]
[RequireComponent(typeof(GrapplerBehavior))]
[RequireComponent(typeof(TargeterBehavior))]
[AddComponentMenu(ComponentPaths.PlayerController)]
public class PlayerControllerBehavior : MonoBehaviour, IEventListener
{
    [SerializeField] BoxCollider2D Collider;
    [SerializeField] LayerMask PlatformLayer;
    [SerializeField] EventTag ExitTag;
    [SerializeField] string Menu = "Menu";
    [DisplayPlayMode] MovementState CurrentState => StateMachine.CurrentState;
    private MovementStateMachine StateMachine;

    private void OnEnable()
    {
        StateMachine = new();
        InitializeControls();
    }
    private void InitializeControls()
    {
        GameInput.PlayerActions controls = new GameInput().Player;
        controls.Enable();

        MoverBehavior mover = GetComponent<MoverBehavior>();
        controls.Move.performed += c => { if (StateMachine.CanMove) mover.Move(c.ReadValue<float>()); };
        controls.Move.canceled += c => mover.Move(0f);
        StateMachine.OnStateChange += s => { if (s == MovementState.Crouch) mover.Move(0f); };

        JumperBehavior jumper = GetComponent<JumperBehavior>();
        controls.Jump.performed += c => { if (StateMachine.ToJump(Collider, PlatformLayer)) jumper.Jump(); };
        controls.Jump.canceled += c => jumper.Cancel();
        jumper.OnFinish += StateMachine.ToDefault;

        CroucherBehavior croucher = GetComponent<CroucherBehavior>();
        controls.Crouch.performed += c => { if (StateMachine.ToCrouch(Collider, PlatformLayer)) croucher.Crouch(); };
        controls.Crouch.canceled += c => croucher.Cancel();
        croucher.OnFinish += StateMachine.ToDefault;

        TargeterBehavior targeter = GetComponent<TargeterBehavior>();
        controls.Aim.performed += c => targeter.Aim(c.ReadValue<Vector2>());
        controls.Aim.canceled += c => targeter.Aim(Vector2.zero);

        GrapplerBehavior grappler = GetComponent<GrapplerBehavior>();
        controls.Grapple.performed += c =>
        {
            if (StateMachine.ToGrapple(targeter))
            {
                grappler.Grapple(targeter.Target.transform.position);
                mover.Control = 0f;
            }
        };
        controls.Grapple.canceled += c =>
        {
            grappler.Cancel();
            mover.Control = 1f;
        };
        grappler.OnFinish += StateMachine.ToDefault;
    }

    public void Invoke(EventTag tag)
    {
        if (tag == ExitTag)
        {
            SceneManager.LoadScene(Menu);
        }
    }
}
