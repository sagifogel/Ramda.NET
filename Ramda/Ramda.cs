using System;
using System.Linq;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class R
    {
        public readonly static RamdaPlaceholder __ = new RamdaPlaceholder();

        public static dynamic CurryN(int length, Delegate fn) {
            return Currying.CurryN(length, fn);
        }

        public static dynamic Evolve<TTarget>(object transformations, TTarget obj) {
            return Currying.Evolve(transformations, obj);
        }

        public static dynamic FormPairs<TValue>(IEnumerable<KeyValuePair<string, TValue>> pairs) {
            return Currying.FromPairs(pairs.Select(pair => new object[] { pair.Key, pair.Value }).ToArray());
        }
    }
}