using System;

namespace Ramda.NET
{
    public static partial class R
    {
        public delegate dynamic Lambda1(object arg = null);

        private static dynamic Curry1<TArg1, TResult>(Func<TArg1, TResult> fn) {
            return new Lambda1(arg1 => {
                if (__.Equals(arg1) || arg1 == null) {
                    return Curry1(fn);
                }

                return fn(arg1.CastTo<TArg1>());
            });
        }
    }
}
