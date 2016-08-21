using System;

namespace Ramda.NET
{
    public static partial class R
    {
        public static RamdaPlaceHolder __ = new RamdaPlaceHolder();

        public class RamdaPlaceHolder
        {
            internal RamdaPlaceHolder() { }
        }

        private static bool IsPlaceHolder(object param) {
            return param != null && __.Equals(param);
        }

        private static TArg CastTo<TArg>(this object arg) {
            return (TArg)Convert.ChangeType(arg, typeof(TArg));
        }
    }
}
