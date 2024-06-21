using System;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Nikusoft.CheckPoints {
    [RequireComponent(typeof(SpriteRenderer))]
    [ExecuteAlways]
    [Serializable]
    public class CheckPointBase : MonoBehaviour, ICheckPoint
    {
        [Header("CheckPoint")]
        [SerializeField]
        private Collider2D checkPointArea; //TODO : TO ASSIGN : Via Inspector (Prefab To be made
        [SerializeField]
        public CheckPointData checkPointData; //TODO : TO ASSIGN : Via Inspector (Prefab To be made
        [SerializeField]
        private SpriteRenderer spriteRender;
        [HideInInspector]
        public CheckPointType checkPointType { get; private set; }
        public CheckPointShape checkPointShape { get; private set; }
        private List<EventTag> checkPointEventTags;
        [HideInInspector]
        public List<LayerMask> validAgentsLayer { get; private set; }

        [SerializeField]
        private bool hasAlreadyPinged = false;
        private bool isMultipleChecks = false;

        private CheckPointManager manager;
        private void Awake()
        {
            ValidateCheckPoint();
            manager = GetComponentInParent<CheckPointManager>();
            spriteRender = GetComponent<SpriteRenderer>();
            InitArea();
        }
        private void OnEnable() => manager.Register(this);
        
        private void OnDisable()=> manager.UnRegister(this);

        public void ValidateCheckPoint()
        {
            if (checkPointData == null)
            {
                Debug.LogError("No data for checkpointBase");
            }
            checkPointType = checkPointData.checkPointType;
            checkPointShape = checkPointData.checkPointShape;
            isMultipleChecks = checkPointData.isMultipleChecks;
            checkPointEventTags = checkPointData.checkPointEventTags;
            validAgentsLayer = checkPointData.validAgentsLayer;
        }

        public void InitArea()
        {
            ChooseShape();   
        }
        private void ChooseShape() {
            //TODO: ADD CUSTOM SHAPES TO CheckPointColliders if needed
            switch (checkPointShape)
            {
                case CheckPointShape.Circle:
                    CircleCollider2D circleCollider2D = gameObject.GetComponent<CircleCollider2D>() != null ? gameObject.GetComponent < CircleCollider2D>() : gameObject.AddComponent<CircleCollider2D>();
                    circleCollider2D.radius = checkPointData.radius;
                    checkPointArea = circleCollider2D;
                    break;
                case CheckPointShape.Box:
                    BoxCollider2D boxCollider2D = gameObject.GetComponent<BoxCollider2D>() != null ? gameObject.GetComponent<BoxCollider2D>() : gameObject.AddComponent<BoxCollider2D>();
                    boxCollider2D.size = checkPointData.scale;
                    checkPointArea = boxCollider2D;
                    break;
                default:
                    break;
            }
            checkPointArea.isTrigger = true;
        }

        /// <summary>
        /// Method to DrawGizmos On Selected Item without having to enable the edit collider
        /// It also adds a line to the manager to better visualize element distribution
        /// </summary>
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = checkPointData.gizmoColor;
            spriteRender.color = checkPointData.gizmoColor;
            switch (checkPointShape)
            {
                case CheckPointShape.Circle:
                    Gizmos.DrawWireSphere(checkPointArea.transform.position, checkPointData.radius);
                    break;
                case CheckPointShape.Box:
                    Gizmos.DrawWireCube(checkPointArea.transform.position, checkPointData.scale);
                    break;
                default:
                    break;
            }
            Gizmos.color = Color.white;
            Handles.DrawAAPolyLine(manager.transform.position, this.transform.position);
        }
#endif

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!hasAlreadyPinged && isMultipleChecks)
            {
                manager.ManageCheckPointHit(this, collision);
            }
        }

        public List<EventTag> TriggerEvents()
        {
            foreach(EventTag x in checkPointEventTags)
            {
                Debug.Log("Launch" + x.ToString()); //TODO : ActualInvoke
            }
            return checkPointEventTags;
        }
    }
}