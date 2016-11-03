using System;

namespace Ramda.NET
{
    public static partial class R
    {
        public class Functor
        {
            public object Value { get; set; }
            public Delegate Map { get; set; }
        }
    }
}
