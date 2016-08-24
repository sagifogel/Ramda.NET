using System;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class R
    {
        public static Currying.RamdaPlaceholder __ = new Currying.RamdaPlaceholder();

        public static dynamic CurryN(int length, Delegate fn) {
            return Currying.CurryN(length, fn);
        }

        public static dynamic Add(int arg1, int arg2) {
            return Currying.Add(arg1, arg2);
        }

        public static dynamic Add(double arg1, double arg2) {
            return Currying.Add(arg1, arg2);
        }

        public static dynamic Add(string arg1, string arg2) {
            return Currying.Add(arg1, arg2);
        }

        public static dynamic Adjust<TValue>(Func<TValue, TValue> fn, int idx, IList<TValue> list) {
            return Currying.Adjust(fn, idx, list);
        }
    }
}
