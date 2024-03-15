using UnityEngine;
using UnityEditor;

public class AnimatorItemDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedObject parent = property.serializedObject;
        SerializedProperty animator = property.FindPropertyRelative("Animator");
        SerializedProperty name = property.FindPropertyRelative("Name");

        label = EditorGUI.BeginProperty(position, label, property);
        position = EditorGUI.PrefixLabel(position, label);

        Rect animatorPosition = new(position);
        animatorPosition.width = animatorPosition.width / 2 - 1;

        Rect selectionPosition = new(position);
        selectionPosition.width = selectionPosition.width / 2 - 1;
        selectionPosition.x += animatorPosition.width + 2;

        if (
            typeof(Component).IsAssignableFrom(parent.targetObject.GetType()) &&
            animator.objectReferenceValue == null &&
            (parent.targetObject as Component).TryGetComponentInGroup(out Animator newAnimatorValue)
        ) animator.objectReferenceValue = newAnimatorValue;
        animator.objectReferenceValue = EditorGUI.ObjectField(animatorPosition, animator.objectReferenceValue, typeof(Animator), true);

        if (animator.objectReferenceValue != null)
        {
            OnAfterGUI(selectionPosition, property, animator.objectReferenceValue as Animator);
        }
        else NoAnimatorGUI(selectionPosition);

        EditorGUI.EndProperty();
    }
    protected virtual void OnAfterGUI(Rect position, SerializedProperty property, Animator animator)
    {
        NoAnimatorGUI(position);
    }
    private void NoAnimatorGUI(Rect position) => EditorGUI.LabelField(position, "None");
}