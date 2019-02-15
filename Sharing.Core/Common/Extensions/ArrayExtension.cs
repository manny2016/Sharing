


namespace Sharing.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    public static class ArrayExtension
    {
        public static IEnumerable<List<T>> Split<T>(this IEnumerable<T> array, int size)
        {
            if (array == null || array.Count().Equals(0))
            {
                yield break;
            }
            var skip = 0;
            var take = size;
            while (skip < array.Count())
            {
                yield return array.Skip<T>(skip).Take<T>(take).ToList();
                skip += size;
            }
        }
    }
}
