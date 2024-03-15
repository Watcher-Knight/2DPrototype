using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using System.Linq;

public abstract class AnimatorParameterDrawer : AnimatorItemDrawer
{
    protected abstract AnimatorControllerParameterType ParameterType { get; }

    protected override void OnAfterGUI(Rect position, SerializedProperty property, Animator animator)
    {
        SerializedProperty name = property.FindPropertyRelative("Name");

        AnimatorController controller = animator.runtimeAnimatorController as AnimatorController;

        if (controller != null)
        {
            int index;
            string[] parameters = controller.parameters.Where(p => p.type == ParameterType).Select(parameter => parameter.name).ToArray();
            if (parameters.Length > 0)
            {
                index = parameters.Contains(name.stringValue) ? parameters.IndexOf(name.stringValue) : 0;
                index = EditorGUI.Popup(position, index, parameters);
                name.stringValue = parameters[index];
            }
            else NoParameterGUI(position);
        }
        else NoParameterGUI(position);
    }

    private void NoParameterGUI(Rect position) => EditorGUI.LabelField(position, "None");
}

[CustomPropertyDrawer(typeof(AnimatorBoolParameter))]
public class AnimatorBoolParameterDrawer : AnimatorParameterDrawer
{
    protected override AnimatorControllerParameterType ParameterType => AnimatorControllerParameterType.Bool;
}

[CustomPropertyDrawer(typeof(AnimatorFloatParameter))]
public class AnimatorFloatParameterDrawer : AnimatorParameterDrawer
{
    protected override AnimatorControllerParameterType ParameterType => AnimatorControllerParameterType.Float;
}

[CustomPropertyDrawer(typeof(AnimatorIntParameter))]
public class AnimatorIntParameterDrawer : AnimatorParameterDrawer
{
    protected override AnimatorControllerParameterType ParameterType => AnimatorControllerParameterType.Int;
}

[CustomPropertyDrawer(typeof(AnimatorTriggerParameter))]
public class AnimatorTriggerParameterDrawer : AnimatorParameterDrawer
{
    protected override AnimatorControllerParameterType ParameterType => AnimatorControllerParameterType.Trigger;
}
