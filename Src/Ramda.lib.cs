using System;
using System.Collections;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class R
    {
        private static TArg CastTo<TArg>(this object arg) {
            if (typeof(IConvertible).IsAssignableFrom(arg.GetType())) {
                return (TArg)Convert.ChangeType(arg, typeof(TArg));
            }

            return (TArg)arg;
        }

        private static Func<object[], bool> Complement(Func<object[], bool> fn) {
            return (arguments => !fn.Invoke(arguments));
        }

        private static IList InternalConcat(IList set1 = null, IList set2 = null) {
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
    }
}
