using System.Collections.Generic;
using UnityEngine;

namespace Nikusoft.CheckPoints
{
    [CreateAssetMenu(fileName = "CheckPointData", menuName = "Data/Checkpoint", order = 0)]
    public class CheckPointData : ScriptableObject
    {
        [Header("CheckPointBase")]
        [Space]

        [SerializeField]
        public CheckPointType checkPointType = CheckPointType.Check;
        [SerializeField]
        public CheckPointShape checkPointShape = CheckPointShape.Circle;
        [SerializeField]
        public Color gizmoColor = Color.red;

        [SerializeField]
        public float radius;
        [SerializeField]
        public Vector2 size;
        [SerializeField]
        public Vector3 scale;

        [Header("Event")]
        [SerializeField]
        public bool isMultipleChecks;

        [SerializeField]
        public List<EventTag> checkPointEventTags;

        [SerializeField]
        public List<LayerMask> validAgentsLayer;
    }
}