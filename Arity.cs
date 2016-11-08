using System;
using System.Reflection;
using static Ramda.NET.Lambda;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal static partial class Currying
    {
        internal static object[] Arguments(params object[] arguments) {
            return ReflectionExtensions.Arity(arguments);
        }

        internal static int Arity(this DynamicDelegate @delegate) {
            return @delegate.Length;
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

        internal static DynamicDelegate Arity(int length, Delegate fn) {
            return Arity(length, new DelegateDecorator(fn));
        }

        internal static DynamicDelegate Arity(int length, DynamicDelegate fn) {
            if (length <= 10) {
                return new CurriedDelegate(length, fn);
            }
            else {
                throw new ArgumentOutOfRangeException("length", "First argument to Arity must be a non-negative integer no greater than ten");
            }
        }
    }
}
