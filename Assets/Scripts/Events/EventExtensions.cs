using UnityEngine;

public static class EventExtensions
{
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
    
    public static void SendMessage(this Component component, EventTag tag) =>
        component.gameObject.SendMessage(tag);
    public static void SendMessage<T>(this Component component, EventTag tag, T arg) =>
        component.gameObject.SendMessage(tag, arg);
}