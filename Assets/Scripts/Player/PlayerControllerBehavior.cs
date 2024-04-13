using System.Collections;
using UnityEngine;

[UpdateEditor]
[SelectionBase]
[RequireComponent(typeof(MoverBehavior))]
[RequireComponent(typeof(JumperBehavior))]
[RequireComponent(typeof(CroucherBehavior))]
[RequireComponent(typeof(GrapplerBehavior))]
[RequireComponent(typeof(MagnetBehavior))]
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

        MagnetBehavior magnet = GetComponent<MagnetBehavior>();
        Controls.Magnet.performed += c =>
        {
            if (magnet.CanMagnetize && StateMachine.ToMagnet())
            {
                magnet.Pull();
                SetControl(mover, 0f, 0f);
            }
        };
        Controls.Magnet.canceled += c => magnet.Release();
        Controls.Aim.performed += c => magnet.Aim(c.ReadValue<Vector2>());
        Controls.Aim.canceled += c => magnet.Aim(Vector2.zero);
        magnet.OnFinish += () =>
        {
            StateMachine.ToDefault();
            SetControl(mover, 1f, 0f);
        };
        StateMachine.OnStateChange += s =>
        {
            if (s == MovementState.Magnet)
            {
                Controls.Move.performed += c => magnet.Slide(c.ReadValue<float>());
                Controls.Move.canceled += c => magnet.Slide(0f);
            }
            else
            {
                Controls.Move.performed -= c => magnet.Slide(c.ReadValue<float>());
                Controls.Move.canceled -= c => magnet.Slide(0f);
            }
        };
    }

    private void SetControl(MoverBehavior mover, float value, float time)
    {
        StopCoroutine(ControlSetRoutine(mover, value, time));
        StartCoroutine(ControlSetRoutine(mover, value, time));
    }

    private IEnumerator ControlSetRoutine(MoverBehavior mover, float value, float time)
    {
        if (time == 0f) 
        {
            mover.Control = value;
            yield break;
        }
        float speed = (mover.Control - value) / 2;
        while (mover.Control != value)
        {
            mover.Control = Mathf.MoveTowards(mover.Control, value, speed * Time.deltaTime);
            yield return null;
        }
    }
}
