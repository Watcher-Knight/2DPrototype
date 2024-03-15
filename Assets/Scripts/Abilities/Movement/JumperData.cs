using UnityEngine;

[CreateAssetMenu(fileName = "Jumper", menuName = AssetPaths.JumperData, order = 0)]
public class JumperData : ScriptableObject
{
    [field: SerializeField] public float Force { get; private set; } = 30f;
    [field: SerializeField][field: Range(0f, 1f)] public float JumpCut { get; private set; } = 0.75f;
}