using System.Linq;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(EventTag))]
    public class EventTagDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return -2f;
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty idProperty = property.FindPropertyRelative("Id");
            EditorEventTag[] tags = EventData.GetTags();
            
            int index = 0;
            if (tags.Any(tag => tag.Id == idProperty.intValue))
                index = tags.IndexOf(tag => tag.Id == idProperty.intValue);
            else
            {
                EditorEventTag notSetOption = new("Undefined", idProperty.intValue);
                tags = tags.Append(notSetOption).ToArray();
                index = tags.IndexOf(notSetOption);
            }
            
            string[] tagOptions = tags.Select(tag => tag.Name).ToArray();
            index = EditorGUILayout.Popup(label, index, tagOptions);

            idProperty.intValue = tags[index].Id;

            //if (oldId != newId) EditorUtility.SetDirty(property.serializedObject.targetObject);
        }
    }