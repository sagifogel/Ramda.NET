using System;

namespace Ramda.NET
{
    public static partial class R
    {
        public interface IReducible
        {
            object Reduce(Func<object, object, object> step, object acc);
        }
    }
}
