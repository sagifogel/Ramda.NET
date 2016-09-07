using System;
using static Ramda.NET.Lambda;

namespace Ramda.NET
{
    internal static partial class Currying
    {
        internal static int Arity(this object[] arguments) {
            if (arguments.IsNull()) {
                return 0;
            }

            int i = arguments.Length - 1;

            while (i >= 0) {
                if (arguments[i].IsNotNull()) {
                    break;
                }

                i--;
            }

            return i++;
        }

        internal static int Arity(this Delegate @delegate) {
            return @delegate.Method.GetParameters().Arity();
        }

        private static dynamic Arity(int length, Delegate fn) {
            if (length <= 10) {
                return new LambdaN(arguments => {
                    return fn.DynamicInvoke(new[] { arguments });
                });
            }
            else {
                throw new ArgumentOutOfRangeException("length", "First argument to Arity must be a non-negative integer no greater than ten");
            }
        }
    }
}
