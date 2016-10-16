using System;

namespace Ramda.NET
{
    public static partial class R
    {
        public class IdentityObj
        {
            public object Value { get; set; }
            public Delegate Map { get; set; }
        }
    }
}
