using System;
using System.Linq;
using System.Collections;
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

        internal static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action) {
            var count = 0;

            foreach (var item in source) {
                action(item, count++);
            }
        }

        internal static void ForEach(this IList source, Action<object, int> action) {
            var count = 0;

            foreach (var item in source) {
                action(item, count++);
            }
        }

        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> predicate) {
            var keys = new HashSet<TKey>();

            return source.Where(element => keys.Add(predicate(element)));
        }
    }
}
