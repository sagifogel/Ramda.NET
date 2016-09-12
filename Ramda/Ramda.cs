using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace Ramda.NET
{
    public static partial class R
    {
        public readonly static RamdaPlaceholder __ = new RamdaPlaceholder();

        public static dynamic Length<TValue>(IList list) {
            return Currying.Length(list);
        }

        public static dynamic CurryN(int length, Delegate fn) {
            return Currying.CurryN(length, fn);
        }
        public static dynamic Nth<TValue>(int offset, IList<TValue> list) {
            return Currying.Nth(offset, list);
        }

        public static dynamic Nth<TValue>(RamdaPlaceholder offset, IList<TValue> list) {
            return Currying.Nth(offset, list);
        }

        public static dynamic NthArg<TValue>(int offset, RamdaPlaceholder list = null) {
            return Currying.NthArg(offset, list);
        }

        public static dynamic NthArg<TValue>(RamdaPlaceholder offset = null, RamdaPlaceholder list = null) {
            return Currying.NthArg(offset, list);
        }

        public static dynamic Evolve<TTarget>(object transformations, TTarget obj) {
            return Currying.Evolve(transformations, obj);
        }

        public static dynamic FormPairs<TValue>(IEnumerable<KeyValuePair<string, TValue>> pairs) {
            return Currying.FromPairs(pairs.Select(pair => new object[] { pair.Key, pair.Value }).ToArray());
        }
    }
}