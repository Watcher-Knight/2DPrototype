using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace Nikusoft.CheckPoints
{
    public class CheckPointManager : MonoBehaviour
    {
        [SerializeField]
        private static List<CheckPointBase> checkPointBases = new List<CheckPointBase>();

        [SerializeField]
        private List<CheckPointBase> listInspector = new List<CheckPointBase>();
        public void Register(CheckPointBase checkPoint)
        {
            if (!checkPointBases.Contains(checkPoint)) { 
                checkPointBases.Add(checkPoint);
                Debug.Log("Registered checkpoint" + checkPoint.checkPointType);
            }
            else {
                Debug.LogWarning("Multiple Registration Going On In Checkpoint Manager");
            };
        }
        public void UnRegister(CheckPointBase checkPoint)
        {
            if (checkPointBases.Contains(checkPoint))
            {
                checkPointBases.Remove(checkPoint);
                Debug.Log("Unregistered checkpoint" + checkPoint.checkPointType);
            }
            else
            {
                Debug.LogWarning("CheckPoint tried multiple times to UnRegister!");
            }
        }

        private void OnEnable()
        {
            ValidateCheckPoints();
            listInspector = ReturnCheckPoints();
        }
        private void OnDisable()
        {
        }
        public static void ValidateCheckPoints()
        {
            if(checkPointBases.Count == 0)
            {
                Debug.LogWarning("Maybe we should fall back to singleton");
            }
            foreach(CheckPointBase checkPoint in checkPointBases)
            {
                checkPoint.ValidateCheckPoint();
            }
        }
        public static List<CheckPointBase> ReturnCheckPoints()
        {
            return checkPointBases;
        }


        public void ManageCheckPointHit(CheckPointBase checkPointBase, Collider2D collision)
        {
            if (collision.tag == "Player"){ //TODO: EXPAND ON BEHAVIOURS - EXAMPLETAG
 
                switch (checkPointBase.checkPointType)
                {
                    case CheckPointType.Spawn:
                        Debug.Log("Spawn Hitted!");
                        break;
                    case CheckPointType.Check:
                        Debug.Log("Check Hitted!");
                        break;
                    case CheckPointType.Cinematic:
                        break;
                    case CheckPointType.EnemySpawn:
                        break;
                    case CheckPointType.EnemyDefeat:
                        break;
                    case CheckPointType.Interactable:
                        break;
                    case CheckPointType.Mixed:
                        break;
                    default:
                        break;
                }
                
            }
        }


#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            foreach (CheckPointBase b in checkPointBases)
            {
                Gizmos.color = b.checkPointData.gizmoColor;
                Handles.DrawAAPolyLine(b.transform.position, this.transform.position);
            }
        }
#endif
    }

    public enum CheckPointShape
    {
        Circle,
        Box
    }
    public enum CheckPointType
    {
        Spawn,
        Check,
        Cinematic,
        EnemySpawn,
        EnemyDefeat,
        Interactable,
        Mixed
    }
}