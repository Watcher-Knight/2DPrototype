public interface IEventListener { void Invoke(EventTag tag); }
public interface IEventListener<T> { void Invoke(EventTag tag, T arg); }