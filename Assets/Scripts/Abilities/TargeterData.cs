using UnityEngine;

[CreateAssetMenu(fileName = "Targeter", menuName = AssetPaths.TargeterData, order = 0)]
public class TargeterData : ScriptableObject
{
    [field: SerializeField] public float Range { get; private set; }
    [field: SerializeField] public float Angle { get; private set; }
    [field: SerializeField] public ContactFilter2D Filter { get; private set; }
    [field: SerializeField] public EventTag SelectionTag { get; private set; }
    [field: SerializeField] public EventTag DeselectionTag { get; private set; }
}