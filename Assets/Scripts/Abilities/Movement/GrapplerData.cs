using UnityEngine;

[CreateAssetMenu(fileName = "Grappler", menuName = AssetPaths.GrapplerData, order = 0)]
public class GrapplerData : ScriptableObject
{
    [field: Header("Grappling")]
    [field: SerializeField] public float SwingForce { get; private set; } = 10f;
    [field: SerializeField] public float ClimbSpeed { get; private set; } = 10f;
    [field: SerializeField] public float ReleaseForceMultiplier { get; private set; } = 1f;
    [field: SerializeField] public float MaxLength { get; private set; } = 10f;
    [field: SerializeField] public float MinLength { get; private set; } = 1f;

    [field: Header("Targeting")]
    // [field: SerializeField] public float Range { get; private set; } = 10;
    [field: SerializeField] public LayerMask TargetLayer { get; private set; }
    [field: SerializeField] public EventTag SelectionTag { get; private set; }
    [field: SerializeField] public EventTag DeselectionTag { get; private set; }
}