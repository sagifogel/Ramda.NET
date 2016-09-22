using System;
using System.Collections.Generic;
using System.Linq;
using static Ramda.NET.Lambda;

namespace Ramda.NET
{
    internal static partial class Currying
    {
        internal static object[] Arity(params object[] arguments) {
            object[] result;

            if (arguments.IsNull() || arguments.Length == 0) {
                return null;
            }

            int i = arguments.Length - 1;

            while (i >= 0) {
                if (arguments[i].IsNotNull()) {
                    break;
                }

                i--;
            }

            result = (object[])Core.Slice(arguments, 0, ++i);

            return result.Length == 0 ? null : result;
        }

        internal static int Arity(this Delegate @delegate) {
            return @delegate.Method.GetParameters().Length;
        }

        internal static object[] Pad(params object[] arguments) {
            var copied = new object[10];

            arguments.CopyTo(copied, 0);

            return copied;
        }

        internal static object[] Pad(this List<object> arguments) {
            return Pad(arguments.ToArray());
        }

        private static Delegate Arity(int length, Delegate fn) {
            if (length <= 10) {
                return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                    return fn.DynamicInvoke(Pad(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
                });
            }
            else {
                throw new ArgumentOutOfRangeException("length", "First argument to Arity must be a non-negative integer no greater than ten");
            }
        }
    }
}
