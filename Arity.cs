using System;
using System.Reflection;
using System.Collections.Generic;
using Reflection = Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    internal static partial class Currying
    {
        internal static object[] Arguments(params object[] arguments) {
            return Reflection.Arity(arguments);
        }

        internal static int Arity(params object[] arguments) {
            return Arguments(arguments)?.Length ?? 0;
        }

        internal static int Arity(this DynamicDelegate @delegate) {
            return @delegate.Length;
        }

        internal static int Arity(this Delegate @delegate) {
            return Reflection.FunctionArity(@delegate);
        }

        internal static int Arity(dynamic @delegate) {
            return @delegate.GetFunctionArity();
        }

        internal static int Arity(this MethodInfo methodInfo) {
            return methodInfo.GetParameters().Length;
        }

        internal static object[] Clip(params object[] arguments) {
            return Clip(arguments, 10);
        }

        internal static object[] Clip(this object[] arguments, int length = 10) {
            var copied = new object[length];

            if (arguments.IsNotNull()) {
                var to = Math.Min(arguments.Length, copied.Length);

                Array.Copy(arguments, 0, copied, 0, to);
            }

            return copied;
        }

        internal static object[] Clip(this object[] arguments, Delegate @delegate) {
            return arguments.Clip(@delegate.Method.GetParameters().Length);
        }

        internal static object[] Clip(this List<object> arguments) {
            return Clip(arguments.ToArray());
        }

        internal static DynamicDelegate Arity(int length, Delegate fn) {
            return Arity(length, new DelegateDecorator(fn));
        }

        internal static DynamicDelegate Arity(int length, DynamicDelegate fn) {
            dynamic @delegate = fn;

            switch (length) {
                case 0:
                    return Delegate(new Func<object>(() => @delegate()));
                case 1:
                    return Delegate(new Func<object, object>(arg1 => @delegate(arg1)));
                case 2:
                    return Delegate(new Func<object, object, object>((arg1, arg2) => @delegate(arg1, arg2)));
                case 3:
                    return Delegate(new Func<object, object, object, object>((arg1, arg2, arg3) => @delegate(arg1, arg2, arg3)));
                case 4:
                    return Delegate(new Func<object, object, object, object, object>((arg1, arg2, arg3, arg4) => @delegate(arg1, arg2, arg3, arg4)));
                case 5:
                    return Delegate(new Func<object, object, object, object, object, object>((arg1, arg2, arg3, arg4, arg5) => @delegate(arg1, arg2, arg3, arg4, arg5)));
                case 6:
                    return Delegate(new Func<object, object, object, object, object, object, object>((arg1, arg2, arg3, arg4, arg5, arg6) => @delegate(arg1, arg2, arg3, arg4, arg5, arg6)));
                case 7:
                    return Delegate(new Func<object, object, object, object, object, object, object, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7) => @delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7)));
                case 8:
                    return Delegate(new Func<object, object, object, object, object, object, object, object, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => @delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)));
                case 9:
                    return Delegate(new Func<object, object, object, object, object, object, object, object, object, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9) => @delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9)));
                case 10:
                    return Delegate(new Func<object, object, object, object, object, object, object, object, object, object, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => @delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)));
                default:
                    throw new ArgumentOutOfRangeException("Length", "First argument to Arity must be a non - negative integer no greater than ten");
            }
        }
    }
}
