using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


namespace Nikusoft.CheckPoints
{

    /// <summary>
    /// Basic editor with examples for Designer Parameter Encapsulation
    /// </summary>
    [CanEditMultipleObjects]
    [CustomEditor(typeof(CheckPointData))]
    public class CheckPointEditor : Editor
    {
        SerializedObject so;
        SerializedProperty propType;
        SerializedProperty propShape;
        SerializedProperty propScale;
        SerializedProperty propRadius;
        SerializedProperty propGizmosColor;
        private void OnEnable()
        {
            so = serializedObject;
            propShape = so.FindProperty("checkPointShape");
            propType = so.FindProperty("checkPointType");
            propScale = so.FindProperty("scale");
            propRadius = so.FindProperty("radius");
            propGizmosColor = so.FindProperty("gizmoColor");
        }

        /// <summary>
        /// OnInspectorChanges Saves The Modified Selected Object Validates and Repaints the scene
        /// </summary>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            so.Update();
            EditorGUILayout.PropertyField(propType);
            
            CheckPointData checkPoint = target as CheckPointData;

            EditorGUILayout.PropertyField(propRadius);
            EditorGUILayout.PropertyField(propScale);
            EditorGUILayout.PropertyField(propGizmosColor);

            if (so.ApplyModifiedProperties())
            {
                CheckPointManager.ValidateCheckPoints();
                SceneView.RepaintAll();
            }
        }
    }
}