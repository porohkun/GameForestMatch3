using System.Collections.Generic;

namespace GameForestMatch3
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> SingleItemAsEnumerable<T>(this T item)
        {
            yield return item;
        }
    }
}