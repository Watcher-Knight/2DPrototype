using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = AssetPaths.ProjectileData, order = 0)]
public class ProjectileData : ScriptableObject
{
    [field: SerializeField] public float Force { get; private set; } = 300f;
    [field: SerializeField] public float Lifetime { get; private set; } = 3f;
}