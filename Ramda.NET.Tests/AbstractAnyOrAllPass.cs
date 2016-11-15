using System;

namespace Ramda.NET.Tests
{
    public abstract class AbstractAnyOrAllPass
    {
        protected Func<int, bool> gt5 = n => n > 5;
        protected Func<int, bool> lt5 = n => n < 5;
        protected Func<int, bool> lt20 = n => n < 20;
        protected Func<int, bool> gt20 = n => n > 20;
        protected Func<int, bool> odd = n => n % 2 != 0;
        protected Func<int, int, int, int, bool> plusEq = (w, x, y, z) => w + x == y + z;
    }
}
