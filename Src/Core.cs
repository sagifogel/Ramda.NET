using System;
using System.Collections;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class Core
    {
        internal static TArg CastTo<TArg>(this object arg) {
            if (typeof(IConvertible).IsAssignableFrom(arg.GetType())) {
                return (TArg)Convert.ChangeType(arg, typeof(TArg));
            }

            return (TArg)arg;
        }

        internal static Func<object[], bool> Complement(Func<object[], bool> fn) {
            return (arguments => !fn.Invoke(arguments));
        }

        internal static IList InternalConcat(IList set1 = null, IList set2 = null) {
            var result = new List<object>();

            if (set1 != null) {
                foreach (var item in set1) {
                    result.Add(item);
                }
            }

            if (set2 != null) {
                foreach (var item in set2) {
                    result.Add(item);
                }
            }

            return result;
        }

        private static bool ContainsWith(Func<object, object, bool> predicate, object x, IList list) {
            foreach (var item in list) {
                if (predicate(x, item)) {
                    return true;
                }
            }

            return false;
        }

        private static IList Filter(Func<object, bool> fn, IList list) {
            var result = new List<object>();

            foreach (var item in list) {
                if (fn(item)) {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}
