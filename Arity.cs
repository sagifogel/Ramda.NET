using System;
using System.Linq;
using static Ramda.NET.Lambda;
using System.Collections.Generic;
using System.Reflection;

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
                if (!object.Equals(arguments[i], null)) {
                    break;
                }

                i--;
            }

            result = (object[])SliceInternal(arguments, 0, ++i);

            return result.Length == 0 ? null : result;
        }

        internal static int Arity(this Delegate @delegate) {
            return @delegate.GetFunctionArity();
        }

        internal static int Arity(this MethodInfo methodInfo) {
            return methodInfo.GetParameters().Length;
        }

        internal static object[] Pad(params object[] arguments) {
            return Pad(arguments, 10);
        }

        internal static object[] Pad(this object[] arguments, int length = 10) {
            var copied = new object[length];

            if (arguments.IsNotNull()) {
                var to = Math.Min(arguments.Length, copied.Length);

                Array.Copy(arguments, 0, copied, 0, to);
            }

            return copied;
        }

        internal static object[] Pad(this object[] arguments, Delegate @delegate) {
            return arguments.Pad(@delegate.Method.GetParameters().Length);
        }

        internal static object[] Pad(this List<object> arguments) {
            return Pad(arguments.ToArray());
        }

        internal static Delegate Arity(int length, Delegate fn) {
            if (length <= 10) {
                return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                    return fn.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                });
            }
            else {
                throw new ArgumentOutOfRangeException("length", "First argument to Arity must be a non-negative integer no greater than ten");
            }
        }
    }
}
