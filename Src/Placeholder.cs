
namespace Ramda.NET
{
    public static partial class R
    {
        public class RamdaPlaceHolder
        {
            internal RamdaPlaceHolder() { }
        }

        public static RamdaPlaceHolder __ = new RamdaPlaceHolder();

        private static bool IsPlaceholder(object param) {
            return param != null && __.Equals(param);
        }
    }
}
