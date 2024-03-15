using System.Linq;
using UnityEngine;

public static class AnimatorExtensions
{
    public static void ResetParameter(this Animator animator, string name)
    {
        AnimatorControllerParameter parameter = animator.GetParameter(name);
        switch (parameter.type)
        {
            case AnimatorControllerParameterType.Bool: animator.SetBool(parameter.name, parameter.defaultBool); break;
            case AnimatorControllerParameterType.Float: animator.SetFloat(parameter.name, parameter.defaultFloat); break;
            case AnimatorControllerParameterType.Int: animator.SetInteger(parameter.name, parameter.defaultInt); break;
            case AnimatorControllerParameterType.Trigger: animator.ResetTrigger(parameter.name); break;
        }
    }

    public static void ResetParameters(this Animator animator) =>
        animator.parameters.ForEach(p => animator.ResetParameter(p.name));

    public static AnimatorControllerParameter GetParameter(this Animator animator, string name) =>
        animator.parameters.FirstOrDefault(p => p.name == name);

    public static bool HasParameter(this Animator animator, string name) =>
        animator.parameters.Any(p => p.name == name);
    public static bool HasBool(this Animator animator, string name) =>
        animator.parameters.Where(p => p.type == AnimatorControllerParameterType.Bool).Any(p => p.name == name);
    public static bool HasFloat(this Animator animator, string name) =>
        animator.parameters.Where(p => p.type == AnimatorControllerParameterType.Float).Any(p => p.name == name);
    public static bool HasInt(this Animator animator, string name) =>
        animator.parameters.Where(p => p.type == AnimatorControllerParameterType.Int).Any(p => p.name == name);
    public static bool HasTrigger(this Animator animator, string name) =>
        animator.parameters.Where(p => p.type == AnimatorControllerParameterType.Trigger).Any(p => p.name == name);
}