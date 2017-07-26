using Sys = System;

namespace Ramda.NET
{
    public static partial class R
    {
        /// <summary>
        ///  Provides static methods for creating tuple objects. To browse the .NET Framework
        /// </summary>
        public class Tuple
        {
            internal static Sys.Tuple<object, object> Create(object item1, object item2) {
                return Sys.Tuple.Create(item1, item2);
            }
        }
    }
}
