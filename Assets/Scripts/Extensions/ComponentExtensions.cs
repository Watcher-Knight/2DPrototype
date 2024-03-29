using UnityEngine;

public static class ComponentExtensions
{
    public static T GetComponentInGroup<T>(this Component origin) where T : Component => origin.gameObject.GetComponentInGroup<T>();
    public static bool TryGetComponentInGroup<T>(this Component origin, out T component) where T : Component => origin.gameObject.TryGetComponentInGroup(out component);
    public static bool TryGetParentComponent<T>(this Component origin, out T component) where T : Component =>
        origin.gameObject.TryGetParentComponent(out component);
    public static bool TryGetChildComponent<T>(this Component origin, out T component) where T : Component =>
        origin.gameObject.TryGetChildComponent(out component);

    public static T AddComponent<T>(this Component component) where T : Component =>
        component.gameObject.AddComponent<T>();
    public static Rigidbody2D AddRigidbody2D(this Component component, float gravityScale = 0) =>
        component.gameObject.AddRigidbody2D(gravityScale);
}