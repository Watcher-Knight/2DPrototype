using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public static class PhysicsExtensions
{
    public static Collider2D[] GetContacts(this Collider2D collider, Vector2 direction, LayerMask layerMask)
    {
        ContactFilter2D filter = new() { useLayerMask = true, layerMask = layerMask };
        List<ContactPoint2D> contactList = new();
        collider.GetContacts(filter, contactList);

        ContactPoint2D[] contacts = contactList.Where(c => Vector2.Angle(-c.normal, direction) <= 45).ToArray();

        return contacts.Select(c => c.collider).Distinct().ToArray();
    }

    public static Collider2D[] GetContacts(this Rigidbody2D rigidbody, Vector2 direction, LayerMask layerMask)
    {
        List<Collider2D> colliders = new();
        rigidbody.GetAttachedColliders(colliders);

        return colliders.SelectMany(c => GetContacts(c, direction, layerMask)).Distinct().ToArray();
    }

    public static Collider2D GetContact(this Collider2D collider, Vector2 direction, LayerMask layerMask)
    {
        Collider2D[] colliders = GetContacts(collider, direction, layerMask);
        return colliders[0];
    }

    public static bool IsTouching(this Collider2D collider, Vector2 direction, LayerMask layerMask)
    {
        Collider2D[] colliders = GetContacts(collider, direction, layerMask);
        return colliders.Length > 0;
    }
    public static bool IsTouching(this Rigidbody2D rigidbody, Vector2 direction, LayerMask layerMask)
    {
        Collider2D[] colliders = GetContacts(rigidbody, direction, layerMask);
        return colliders.Length > 0;
    }


    public static Collider2D[] OverlapCollider(this Collider2D collider, ContactFilter2D contactFilter)
    {
        List<Collider2D> results = new();
        collider.OverlapCollider(contactFilter, results);
        return results.ToArray();
    }

    public static bool OverlapsLine(this Collider2D collider, Vector2 start, Vector2 end)
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(start, (end - start).normalized, (end - start).magnitude);
        return hits.Any(h => h.collider == collider);
    }
}