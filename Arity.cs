using System;
using System.Linq;

namespace Ramda.NET
{
    public static partial class Currying
    {
        internal static int Arity(this object[] arguments) {
            return arguments != null ? arguments.Length : 0;
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
