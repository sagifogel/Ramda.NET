using System;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal static class DictionaryExtensions
    {
        private static object syncLock = new object();

        internal static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Func<TValue> factory) {
            TValue value;

            if (!source.TryGetValue(key, out value)) {
                lock (syncLock) {
                    if (!source.TryGetValue(key, out value)) {
                        source.Add(key, value = factory());
                    }
                }
            }

            return value;
        }
    }
}
