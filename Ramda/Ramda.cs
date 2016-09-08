using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ramda.NET
{
    public static partial class R
    {
        public readonly static RamdaPlaceholder __ = new RamdaPlaceholder();

        public static dynamic CurryN(int length, Delegate fn) {
            return Currying.CurryN(length, fn);
        }
    }
}