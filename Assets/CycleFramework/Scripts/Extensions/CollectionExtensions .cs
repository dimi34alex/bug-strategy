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
}
