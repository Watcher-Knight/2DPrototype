using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu(ComponentPaths.Mover)]
public class MoverBehavior : MonoBehaviour
{
    [SerializeField] private MoverData Data;
    private float b_Control = 1;
    public float Control
    {
        get => b_Control;
        set => b_Control = Mathf.Clamp01(value);
    }

    private Mover Mover;
    //private Rigidbody2D Rigidbody;
    private float Direction { get; set; } = 0f;

    private void OnEnable()
    {
        //Rigidbody = GetComponent<Rigidbody2D>();
        Mover = new()
        {
            Rigidbody = GetComponent<Rigidbody2D>(),
            Speed = Data.Speed,
            Acceleration = Data.Acceleration,
            Deceleration = Data.Deceleration
        };
    }

    public void Move(float value) => Direction = value;

    private void FixedUpdate()
    {
        Mover.Move(Direction, Control);
    }
}