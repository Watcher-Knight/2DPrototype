using UnityEngine;

[CreateAssetMenu(fileName = "Mover", menuName = AssetPaths.MoverData, order = 0)]
public class MoverData : ScriptableObject
{
    [field: SerializeField] public float Speed { get; private set; } = 10f;
    [field: SerializeField][field: Range(0f, 1f)] public float Acceleration { get; private set; } = 0.75f;
    [field: SerializeField][field: Range(0f, 1f)] public float Deceleration { get; private set; } = 0.75f;
    [field: SerializeField] public float RegainControlStrength { get; private set; } = 1f;
}