using System;
using UnityEngine;

[Serializable]
public abstract class AnimatorParameter
{
    [SerializeField] protected Animator Animator;
    [SerializeField] protected string Name;
    public abstract void Reset();
}

[Serializable]
public class AnimatorBoolParameter : AnimatorParameter
{
    public void SetValue(bool value) { if (Animator.HasBool(Name)) Animator.SetBool(Name, value); }
    public bool GetValue() => Animator.HasBool(Name) && Animator.GetBool(Name);
    public override void Reset() => SetValue(Default);
    public bool Default => Animator.GetParameter(Name)?.defaultBool ?? false;
    public bool Value
    {
        get => GetValue();
        set => SetValue(value);
    }
}
[Serializable]
public class AnimatorFloatParameter : AnimatorParameter
{
    public void SetValue(float value) { if (Animator.HasFloat(Name)) Animator.SetFloat(Name, value); }
    public float GetValue() => Animator.HasFloat(Name) ? Animator.GetFloat(Name) : 0f;
    public float Default => Animator.GetParameter(Name)?.defaultFloat ?? 0f;
    public override void Reset() => SetValue(Default);
    public float Value
    {
        get => GetValue();
        set => SetValue(value);
    }
}
[Serializable]
public class AnimatorIntParameter : AnimatorParameter
{
    public void SetValue(int value) { if (Animator.HasInt(Name)) Animator.SetInteger(Name, value); }
    public int GetValue() => Animator.HasInt(Name) ? Animator.GetInteger(Name) : 0;
    public override void Reset() => SetValue(Default);
    public int Default => Animator.GetParameter(Name)?.defaultInt ?? 0;
    public int Value
    {
        get => GetValue();
        set => SetValue(value);
    }
}
[Serializable]
public class AnimatorTriggerParameter : AnimatorParameter
{
    public void Activate() { if (Animator.HasTrigger(Name)) Animator.SetTrigger(Name); }
    public override void Reset() { if (Animator.HasTrigger(Name)) Animator.ResetTrigger(Name); }
}