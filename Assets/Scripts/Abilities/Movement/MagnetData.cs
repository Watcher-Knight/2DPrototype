using UnityEngine;

[CreateAssetMenu(fileName = "Magnet", menuName = AssetPaths.MagnetData, order = 0)]
public class MagnetData : ScriptableObject
{
    [field: Header("Magneting")]
    [field: SerializeField] public float Range { get; private set; } = 10f;
    [field: SerializeField] public float Force { get; private set; } = 30f;
    
    [field: Header("Targeting")]
    [field: SerializeField] public LayerMask MagnetLayer { get; private set; }
    [field: SerializeField] public EventTag SelectionTag { get; private set; }
    [field: SerializeField] public EventTag DeselectionTag { get; private set; }

    [field: Header("Movement")]
    [field: SerializeField] public MoverData Mover { get; private set; }
}