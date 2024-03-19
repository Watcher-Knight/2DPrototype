using System;
using System.Collections.Generic;
using System.Linq;

public static class EnumerableExtensions
{
    public static IEnumerable<T> Remove<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        List<T> list = source.ToList();
        for (int i = 0; i < list.Count; i++)
        {
            if (predicate(list[i]))
            {
                list.RemoveAt(i);
                break;
            }
        }
        return list;
    }
    public static IEnumerable<T> RemoveAll<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        List<T> list = source.ToList();
        for (int i = list.Count; i > 0; i--)
        {
            if (predicate(list[i])) list.RemoveAt(i);
        }
        return list;
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) { foreach (T item in source) action(item); }
    public static int IndexOf<T>(this IEnumerable<T> source, T item) => Array.IndexOf(source.ToArray(), item);
    public static int IndexOf<T>(this IEnumerable<T> source, Func<T, bool> predicate)
    {
        int index = source.TakeWhile(item => !predicate(item)).Count();
        if (index != source.Count()) return index;
        throw new IndexOutOfRangeException();
    }

    public static SerializableEnumerable<T> Serializable<T>(this IEnumerable<T> enumerable) => new(enumerable);
}

[Serializable]
public struct SerializableEnumerable<T>
{
    private List<T> Items;
    public SerializableEnumerable(IEnumerable<T> enumerable) => Items = new List<T>(enumerable);
    public IEnumerable<T> ToEnumerable() => Items;
}