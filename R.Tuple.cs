using Sys = System;

namespace Ramda.NET
{
    public static partial class R
    {
        public class Tuple
        {
            public static Sys.Tuple<object, object> Create(object item1, object item2) {
                return Sys.Tuple.Create(item1, item2);
            }
        }
    }
}
