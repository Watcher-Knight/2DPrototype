using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "Dasher", menuName = AssetPaths.DasherData, order = 0)]
public class DasherData : ScriptableObject
{
    [field: Header("Dasher")]

    [field: SerializeField]
    [field: Range(5f, 20f)]
    public float DashDistance = 10f;

    [field: SerializeField]
    [field: Range(0.1f, 0.2f)]
    public float DashDuration = 0.15f;

    [field: Tooltip("Cooldown between dashes (seconds)")]
    [field: SerializeField]
    public float DashCooldown { get; private set; } = 1f;

    [field: Tooltip("Show/Hide Trail Effect")]
    [field: SerializeField]
    public bool ShowTrail { get; private set; } = true;
}
