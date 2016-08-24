namespace Ramda.NET
{
    public static partial class Currying
    {
        public class RamdaPlaceholder
        {
            internal RamdaPlaceholder() { }
        }

        private static bool IsPlaceholder(object param) {
            return param != null && R.__.Equals(param);
        }
    }
}
