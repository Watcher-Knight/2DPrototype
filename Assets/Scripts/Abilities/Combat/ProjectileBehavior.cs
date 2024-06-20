using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[AddComponentMenu(ComponentPaths.Projectile)]
public class ProjectileBehavior : MonoBehaviour
{
    [SerializeField] private ProjectileData Data;

    private float DestroyTime = 0f;

    public void Shoot(Vector2 origin, Vector2 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        ProjectileBehavior projectile = Instantiate(gameObject, origin, rotation).GetComponent<ProjectileBehavior>();
        projectile.GetComponent<Rigidbody2D>().AddForce(direction * Data.Force, ForceMode2D.Impulse);
        projectile.DestroyTime = Time.time + Data.Lifetime;
    }

    private void Update()
    {
        if (Time.time > DestroyTime) Destroy();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy();
    }

    public void Destroy() => Destroy(gameObject);
}