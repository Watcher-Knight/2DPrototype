using System;
using UnityEngine;

public class CroucherBehavior : MonoBehaviour, IEventListener
{
    [SerializeField] AnimatorBoolParameter Parameter;
    [SerializeField] EventTag ResetTag;

    public Action OnFinish;

    public void Crouch()
    {
        Parameter.Value = true;
    }

    public void Cancel()
    {
        Parameter.Value = false;
    }

    private void Finish()
    {
        OnFinish?.Invoke();
    }

    public void Invoke(EventTag tag)
    {
        if (tag == ResetTag) Finish();
    }
}