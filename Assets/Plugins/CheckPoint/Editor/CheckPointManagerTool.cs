using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;
namespace Nikusoft.CheckPoints
{
    public class CheckPointManagerTool : EditorWindow
    {
        [MenuItem("NikusoftTools/CheckPointTool")]
        public static void OpenTheView() => GetWindow<CheckPointManagerTool>();

        private CheckPointType defaultCheckPointType = CheckPointType.Check;
        private CheckPointType filterCheckPointType;
        private CheckPointType creationCheckPointType = CheckPointType.Check;
        private CheckPointShape creationCheckPointShape = CheckPointShape.Circle;
        private CheckPointData creationCheckPointData;
        private List<CheckPointBase> objectsInManager;

        SerializedObject so;
        SerializedProperty propertyCreation;

        private void OnEnable()  {
            so = new SerializedObject(this);
            propertyCreation = so.FindProperty("creationCheckPointData");
            Selection.selectionChanged += Repaint;
            objectsInManager = CheckPointManager.ReturnCheckPoints();
            filterCheckPointType = defaultCheckPointType;
            creationCheckPointType = defaultCheckPointType;
        }
        private void OnDisable()
        {
            Selection.selectionChanged -= Repaint;
            filterCheckPointType = defaultCheckPointType;
            creationCheckPointType = defaultCheckPointType;
        }


        public void SpawnCheckPoint()
        {

        }
        private void OnGUI()
        {
            so.Update();         
            SelectAllCheckPoints();
            ListAllChecksByType();
            ValidateAllCheckPoints();
            ShowList();

            SpawnNewCheckPoint();
            so.ApplyModifiedProperties();
        }

        public void ShowList()
        {
            foreach(CheckPointBase checkPoint in objectsInManager)
            {
                GUILayout.TextArea(checkPoint.name);
            }
        }

        public void SelectAllCheckPoints()
        {
            if(GUILayout.Button("List All CheckPoints"))
            {
                objectsInManager = CheckPointManager.ReturnCheckPoints();
                Debug.Log("Checkpoint Count : " + objectsInManager.Count());
            }
        }
        public void ValidateAllCheckPoints()
        {
            if(GUILayout.Button("Validate All CheckPoints"))
            {
                CheckPointManager.ValidateCheckPoints();
            }
        }
        public void ListAllChecksByType()
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();

            filterCheckPointType = (CheckPointType)EditorGUILayout.EnumPopup("Group by Type", filterCheckPointType);
            GUILayout.Space(20);
            if(GUILayout.Button("Select By Type"))
            {
                objectsInManager = CheckPointManager
                .ReturnCheckPoints()
                .Where(x => x.checkPointData
                .checkPointType == filterCheckPointType)
                .ToList();
            }

            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
        public void SpawnNewCheckPoint()
        {
            GUILayout.Label("Create a new one");

            creationCheckPointShape = (CheckPointShape)EditorGUILayout.EnumPopup("Current Shape", creationCheckPointShape);
            creationCheckPointType = (CheckPointType)EditorGUILayout.EnumPopup("Current Type", creationCheckPointType);
            creationCheckPointData = (CheckPointData) EditorGUILayout.ObjectField(creationCheckPointData, typeof(CheckPointData));
            if (GUILayout.Button("Spawn By Type And Shape And Data"))
            {
               
               
            }
            GUILayout.BeginVertical();
            GUILayout.EndVertical();
        }
    }

}