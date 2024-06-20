using System;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateEditor]
[SelectionBase]
[RequireComponent(typeof(MoverBehavior))]
[RequireComponent(typeof(JumperBehavior))]
[RequireComponent(typeof(CroucherBehavior))]
[RequireComponent(typeof(ShooterBehavior))]
[RequireComponent(typeof(GrapplerBehavior))]
[RequireComponent(typeof(MagnetBehavior))]
[RequireComponent(typeof(PlayerAnimator))]
[AddComponentMenu(ComponentPaths.PlayerController)]
public class PlayerControllerBehavior : MonoBehaviour
{
    [SerializeField] BoxCollider2D Collider;
    [SerializeField] LayerMask PlatformLayer;
    [SerializeField] EventTag ExitTag;

    [DisplayPlayMode] MovementState CurrentState => MovementStateMachine.CurrentState;
    [DisplayPlayMode] PlayerMode CurrentMode => ModeStateMachine.CurrentState;

    private MovementStateMachine MovementStateMachine;
    private PlayerModeStateMachine ModeStateMachine;
    private GameInput.PlayerActions Controls;

    //private Action<Vector2> Aim;

    private void OnEnable()
    {
        MovementStateMachine = new();
        ModeStateMachine = new();

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

        PlayerAnimator animator = GetComponent<PlayerAnimator>();
        InitializeAimCallback(animator.Aim);

        MoverBehavior mover = GetComponent<MoverBehavior>();
        InitializeMove(mover);

        JumperBehavior jumper = GetComponent<JumperBehavior>();
        InitializeJump(jumper);

        // CroucherBehavior croucher = GetComponent<CroucherBehavior>();
        // InitializeCrouch(croucher);

        ShooterBehavior shooter = GetComponent<ShooterBehavior>();
        InitializeShoot(shooter);

        GrapplerBehavior grappler = GetComponent<GrapplerBehavior>();
        InitializeGrapple(grappler, mover);

        MagnetBehavior magnet = GetComponent<MagnetBehavior>();
        InitializeMagnet(magnet, mover);

        DasherBehaviour dasher = GetComponent<DasherBehaviour>();
        InitializeDasher(dasher, mover);

        InitializeModeSet();
    }

    private void InitializeDasher(DasherBehaviour dasher, MoverBehavior mover)
    {
        Controls.Dash.performed += c => { if (MovementStateMachine.ToDash(Collider, PlatformLayer)) dasher.Dash(); };
        Controls.Dash.canceled += c => dasher.Cancel();
        dasher.OnFinish += MovementStateMachine.ToDefault;
    }

    private void InitializeMove(MoverBehavior mover)
    {
        Controls.Move.performed += c => { if (MovementStateMachine.CanMove) mover.Move(Math.Sign(c.ReadValue<Vector2>().x)); };
        Controls.Move.canceled += c => mover.Move(0f);
        MovementStateMachine.OnStateChange += s => { if (s == MovementState.Crouch) mover.Move(0f); };
        MovementStateMachine.OnMovementEnabled += () => { mover.Move(Controls.Move.ReadValue<Vector2>().x); };
    }

    private void InitializeJump(JumperBehavior jumper)
    {
        Controls.Jump.performed += c => { if (MovementStateMachine.ToJump(Collider, PlatformLayer)) jumper.Jump(); };
        Controls.Jump.canceled += c => jumper.Cancel();
        jumper.OnFinish += MovementStateMachine.ToDefault;
    }

    private void InitializeCrouch(CroucherBehavior croucher)
    {
        Controls.Crouch.performed += c => { if (MovementStateMachine.ToCrouch(Collider, PlatformLayer)) croucher.Crouch(); };
        Controls.Crouch.canceled += c => croucher.Cancel();
        croucher.OnFinish += MovementStateMachine.ToDefault;
    }

    private void InitializeShoot(ShooterBehavior shooter)
    {
        Controls.Shoot.performed += shoot;
        //Aim += shooter.Aim;

        void shoot(InputAction.CallbackContext context) => shooter.Shoot();
        Controls.Shoot.performed += shoot;

        // ModeStateMachine.OnStateChange += s =>
        // {
        //     if (s == PlayerMode.Attack)
        //     {
        //         Controls.Shoot.performed += shoot;
        //         //Aim += shooter.Aim;
        //     }
        //     else
        //     {
        //         Controls.Shoot.performed -= shoot;
        //         //Aim -= shooter.Aim;
        //     }
        // };
    }

    private void InitializeGrapple(GrapplerBehavior grappler, MoverBehavior mover)
    {
        void grapple(InputAction.CallbackContext context)
        {
            if (grappler.CanGrapple && MovementStateMachine.ToGrapple(Collider, PlatformLayer))
            {
                grappler.Grapple();
                mover.Disable();
            }
        }
        void cancel(InputAction.CallbackContext context) => grappler.Cancel();

        Controls.Grapple.performed += grapple;
        Controls.Grapple.canceled += cancel;

        // ModeStateMachine.OnStateChange += s =>
        // {
        //     if (s == PlayerMode.Grapple)
        //     {
        //         Controls.Shoot.performed += grapple;
        //         Controls.Shoot.canceled += cancel;
        //         //Aim += grappler.Aim;
        //     }
        //     else
        //     {
        //         Controls.Shoot.performed -= grapple;
        //         Controls.Shoot.canceled -= cancel;
        //         //Aim -= grappler.Aim;
        //         //grappler.Aim(Vector2.zero);
        //     }
        // };

        MovementStateMachine.OnStateChange += s =>
        {
            if (s == MovementState.Grapple)
            {
                Controls.Move.performed += c =>
                {
                    grappler.Swing(Math.Sign(c.ReadValue<Vector2>().x));
                    grappler.Climb(Math.Sign(c.ReadValue<Vector2>().y));
                };
                Controls.Move.canceled += c =>
                {
                    grappler.Swing(0f);
                    grappler.Climb(0f);
                };
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

        grappler.OnFinish += () =>
        {
            MovementStateMachine.ToDefault();
            mover.Enable();
        };
    }

    private void InitializeMagnet(MagnetBehavior magnet, MoverBehavior mover)
    {
        void magnetAction(InputAction.CallbackContext context)
        {
            if (magnet.CanMagnetize && MovementStateMachine.ToMagnet())
            {
                magnet.Pull();
                mover.Disable();
            }
        }
        void cancel(InputAction.CallbackContext context) => magnet.Release();

        Controls.Magnet.performed += magnetAction;
        Controls.Magnet.canceled += cancel;

        // ModeStateMachine.OnStateChange += s =>
        // {
        //     if (s == PlayerMode.Magnet)
        //     {
        //         Controls.Shoot.performed += magnetAction;
        //         Controls.Shoot.canceled += cancel;
        //         //Aim += magnet.Aim;
        //     }
        //     else
        //     {
        //         Controls.Shoot.performed -= magnetAction;
        //         Controls.Shoot.canceled -= cancel;
        //         //Aim -= magnet.Aim;
        //     }
        // };

        MovementStateMachine.OnStateChange += s =>
        {
            void slide(InputAction.CallbackContext context) => magnet.Slide(Math.Sign(context.ReadValue<Vector2>().x));
            void stop(InputAction.CallbackContext context) => magnet.Slide(0f);
            if (s == MovementState.Magnet)
            {
                Controls.Move.performed += slide;
                Controls.Move.canceled += stop;
            }
            else
            {
                Controls.Move.performed -= slide;
                Controls.Move.canceled -= stop;
            }
        };

        magnet.OnFinish += () =>
        {
            MovementStateMachine.ToDefault();
            mover.Enable();
        };
    }

    private void InitializeModeSet()
    {
        // Controls.AttackSet.performed += c => ModeStateMachine.ToAttack();
        // Controls.GrappleSet.performed += c => ModeStateMachine.ToGrapple();
        // Controls.MagnetSet.performed += c => ModeStateMachine.ToMagnet();
    }

    [Display] private bool UsingMouse { get; set; } = false;
    private void InitializeAimCallback(Action<Vector2> action)
    {
        Controls.Aim.performed += c => { if (MovementStateMachine.CanAim) action.Invoke(c.ReadValue<Vector2>()); };
        Controls.Aim.canceled += c => { if (MovementStateMachine.CanAim) action.Invoke(Vector2.zero); };

        Controls.Move.performed += c =>
        {
            if (MovementStateMachine.CanAim && Controls.Aim.ReadValue<Vector2>() == Vector2.zero && c.control.device is Gamepad)
                action.Invoke(c.ReadValue<Vector2>());
        };

        Controls.Move.canceled += c =>
        {
            if (MovementStateMachine.CanAim && Controls.Aim.ReadValue<Vector2>() == Vector2.zero && c.control.device is Gamepad)
                action.Invoke(Vector2.zero);
        };

        InputSystem.onEvent += (e, d) =>
        {
            if (d.device is Gamepad) UsingMouse = false;
            if (d.device is Mouse) UsingMouse = true;
        };

        OnUpdate += () =>
        {
            // if (Mouse.current.delta.value != Vector2.zero) UsingMouse = true;
            // if (Controls.Aim.ReadValue<Vector2>() != Vector2.zero) UsingMouse = false;

            if (!UsingMouse || !MovementStateMachine.CanAim) return;
            Vector2 aimDirection = MainCamera.Get().ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
            action?.Invoke(aimDirection.normalized);
        };
    }

    private void Update()
    {
        OnUpdate?.Invoke();
    }
    private Action OnUpdate;
}
