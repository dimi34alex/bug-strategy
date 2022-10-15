using System;
using System.Collections.Generic;
using System.Linq;

public static class CollectionExtensions
{
    public static TElement Find<TElement>(this IEnumerable<TElement> collection, Predicate<TElement> comparator)
    {
        if (collection is null)
            throw new ArgumentNullException("Input collection cannot be null");

        if (comparator is null)
            throw new NullReferenceException("Comparator cannot be null");

        foreach (TElement element in collection)
            if (comparator(element))
                return element;

        return default;
    }

    public static bool Contains<TElement>(this IEnumerable<TElement> collection, Predicate<TElement> comparator)
    {
        if (collection is null)
            throw new ArgumentNullException("Input collection cannot be null");

        if (comparator is null)
            throw new NullReferenceException("Comparator cannot be null");

        foreach (TElement element in collection)
            if (comparator(element))
                return true;

        return false;
    }

    public static int IndexOf<TElement>(this IEnumerable<TElement> collection, Predicate<TElement> comparison)
    {
        int index = 0;

        foreach (TElement e in collection)
        {
            if (comparison(e))
                return index;

            index++;
        }

        return -1;
    }

    public static TElement FindMin<TElement>(this IEnumerable<TElement> collection,
        Func<TElement, TElement, bool> nextElementMore)
    {
        TElement last = collection.FirstOrDefault();

        foreach (TElement e in collection)
        {
            if (nextElementMore(last, e))
                last = e;
        }

        return last;
    }
}
