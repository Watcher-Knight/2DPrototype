// using UnityEngine;
// using UnityEditor;

// [CustomPropertyDrawer(typeof(SceneReference))]
// public class SceneReferenceDrawer : PropertyDrawer
// {
//     public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//     {
//         SerializedProperty nameProperty = property.FindPropertyRelative("Name");
//         var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(nameProperty.stringValue);
        
//         SceneAsset newScene = EditorGUILayout.ObjectField("scene", oldScene, typeof(SceneAsset), false) as SceneAsset;
//         string newName = newScene?.name;

//         nameProperty.stringValue = newName;
//     }
// }