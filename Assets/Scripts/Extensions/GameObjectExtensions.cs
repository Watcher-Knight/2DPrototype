using UnityEngine;

public static class GameObjectExtensions
{
    public static T GetComponentInGroup<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject.TryGetComponent(out T component)) return component;
        if (gameObject.TryGetParentComponent(out T parentComponent)) return parentComponent;
        if (gameObject.TryGetChildComponent(out T childComponent)) return childComponent;
        return null;
    }
    public static bool TryGetComponentInGroup<T>(this GameObject gameObject, out T component) where T : Component => (component = gameObject.GetComponentInGroup<T>()) != null;
    public static bool TryGetParentComponent<T>(this GameObject gameObject, out T component) where T : Component
    {
        component = gameObject.GetComponentInParent<T>();
        return component != null;
    }
    public static bool TryGetChildComponent<T>(this GameObject gameObject, out T component) where T : Component
    {
        component = gameObject.GetComponentInChildren<T>();
        return component != null;
    }
    public static Rigidbody2D AddRigidbody2D(this GameObject gameObject, float gravityScale = 0)
    {
        Rigidbody2D rigidbody = gameObject.AddComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;
        return rigidbody;
    }

    public static void SendMessage(this GameObject gameObject, EventTag tag)
    {
        IEventListener[] listeners = gameObject.GetComponents<IEventListener>();
        foreach (IEventListener listener in listeners) listener.Invoke(tag);
    }
    public static void SendMessage<T>(this GameObject gameObject, EventTag tag, T arg)
    {
        IEventListener<T>[] listeners = gameObject.GetComponents<IEventListener<T>>();
        foreach (IEventListener<T> listener in listeners) listener.Invoke(tag, arg);
    }
}