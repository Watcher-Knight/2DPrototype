using UnityEngine;

[CreateAssetMenu(fileName = "ShooterData", menuName = AssetPaths.ShooterData, order = 0)]
public class ShooterData : ScriptableObject
{
    [field: SerializeField] public ProjectileBehavior Projectile { get; private set; }
    [field: SerializeField] public float ProjectileForce { get; private set; } = 300f;
    [field: SerializeField] public float ProjectileLifetime { get; private set; } = 3f;
}