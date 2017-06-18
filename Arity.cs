using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using Reflection = Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    internal static partial class Currying
    {
        private static readonly Type typeofList = typeof(IList);
        private static readonly Type typeofString = typeof(string);
        private static readonly Type typeofObject = typeof(object);
        private static readonly string[] emptyArray = new string[0];
        private static readonly Type typeofObjectArray = typeof(object[]);

        internal static IList Arguments(params object[] arguments) {
            return Reflection.Arity(arguments);
        }

        internal static int Arity(params object[] arguments) {
            return Arguments(arguments)?.Count ?? 0;
        }

        internal static int Arity(this DynamicDelegate @delegate) {
            return @delegate.Length;
        }

        internal static int Arity(this Delegate @delegate) {
            return Reflection.FunctionArity(@delegate);
        }

        internal static int Arity(this MethodInfo methodInfo) {
            return methodInfo.GetParameters().Length;
        }

        internal static object[] Copy(int length, Array arguments) {
            var copied = new object[length];

            if (arguments.IsNotNull()) {
                var to = Math.Min(arguments.Length, copied.Length);

                Array.Copy(arguments, 0, copied, 0, to);
            }

            return copied;
        }

        internal static object[] Clip(this object[] arguments, Delegate @delegate) {
            var @params = @delegate.Method.GetParameters();
            var clipped = Copy(@params.Length, arguments);

            if (arguments.Length == 1 && clipped.Length == 1) {
                object item = clipped[0];

                if (item.IsNotNull()) {
                    var itemType = item.GetType();
                    var paramType = @params[0].ParameterType;

                    if (itemType.IsArray && paramType.IsArray && !paramType.Equals(itemType)) {
                        var typedItem = item as IList;

                        if (paramType.IsObjectArray()) {
                            clipped[0] = Copy(typedItem.Count, (Array)clipped[0]);
                        }
                        else {
                            clipped[0] = typedItem.CopyToNewArray(typedItem.Count, paramType.GetElementType());
                        }
                    }
                }
            }

            return clipped;
        }

        internal static DynamicDelegate Arity(int length, DynamicDelegate fn) {
            dynamic @delegate = fn;

            if (length <= 10)
                return DelegateN(new Func<object, object, object, object, object, object, object, object, object, object, object>((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => @delegate(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10)), length);
            else {
                throw new ArgumentOutOfRangeException("Length", "First argument to Arity must be a non - negative integer no greater than ten");
            }
        }
    }
}
