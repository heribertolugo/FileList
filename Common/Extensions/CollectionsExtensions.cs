using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    public static class CollectionsExtensions
    {
        public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> collection, int count)
        {
            return collection.Reverse().Take(count).Reverse();
        }

        public static int IndexOf<T>(this SortedSet<T> set, T item)
        {
            try
            {
                // which has better performance??
                //return set.Select((o, i) => new { o, i }).First(o => o.o.Equals(item)).i;
                return set.TakeWhile((o, i) => !o.Equals(item)).Count() - 1;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
    }
}
