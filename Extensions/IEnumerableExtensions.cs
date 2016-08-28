using System;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal static class IEnumerableExtensions
    {
        internal static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action) {
            foreach (var item in source) {
                action(item);
            }
        }
    }
}
