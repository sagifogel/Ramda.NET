using System;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class R
    {
        public delegate dynamic LambdaN(params object[] arguments);
        public static RamdaPlaceHolder __ = new RamdaPlaceHolder();

        public readonly static dynamic CurryN = Curry2<int, Delegate, dynamic>((length, fn) => {
            if (length == 1) {
                return Curry1(new Func<object[], dynamic>(fn.DynamicInvoke));
            }

            return Arity(length, InternalCurryN(length, new object[0], fn));
        });

        public class RamdaPlaceHolder
        {
            internal RamdaPlaceHolder() { }
        }

        private static bool IsPlaceholder(object param) {
            return param != null && __.Equals(param);
        }

        private static TArg CastTo<TArg>(this object arg) {
            if (typeof(IConvertible).IsAssignableFrom(arg.GetType())) {
                return (TArg)Convert.ChangeType(arg, typeof(TArg));
            }

            return (TArg)arg;
        }

        private static LambdaN InternalCurryN(int length, object[] received, Delegate fn) {
            return new LambdaN(arguments => {
                var argsIdx = 0;
                var left = length;
                var combinedIdx = 0;
                var combined = new List<object>();

                while (combinedIdx < received.Length || argsIdx < arguments.Length) {
                    object result = null;

                    if (combinedIdx < received.Length && (!IsPlaceholder(received[combinedIdx]) || argsIdx >= arguments.Length)) {
                        result = received[combinedIdx];
                    }
                    else {
                        result = arguments[argsIdx];
                        argsIdx += 1;
                    }

                    combined.Insert(combinedIdx, result);

                    if (!IsPlaceholder(result)) {
                        left -= 1;
                    }

                    combinedIdx += 1;
                }

                return left <= 0 ? fn.DynamicInvoke(combined.ToArray()) : Arity(left, InternalCurryN(length, combined.ToArray(), fn));
            });
        }
    }
}
