using System;
using UnityEngine;

public abstract class Targeter
{
    private Collider2D b_Target;
    public Collider2D Target
    {
        get => b_Target;
        protected set
        {
            if (value != b_Target)
            {
                ChangeTarget(b_Target, value);
                b_Target = value;
            }
        }
    }
    public Action<Collider2D> OnTargetChange;
    private void ChangeTarget(Collider2D oldTarget, Collider2D newTarget)
    {
        OnTargetChange?.Invoke(newTarget);
    }
}