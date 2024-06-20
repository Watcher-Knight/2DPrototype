using UnityEngine;

[AddComponentMenu(ComponentPaths.Shooter)]
public class ShooterBehavior : MonoBehaviour
{
    [SerializeField][AutoAssign] private ShooterData Data;
    [SerializeField][AutoAssign] private Transform Origin;

    //private Vector2 Direction;

    public void Shoot()
    {
        //Vector2 direction = Direction != Vector2.zero ? Direction : (Vector2.right * transform.localScale.x).normalized;
        Data.Projectile.Shoot(Origin.position, Origin.right);
    }

    //public void Aim(Vector2 direction) => Direction = direction;
}