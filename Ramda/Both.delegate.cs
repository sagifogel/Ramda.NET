using System;

namespace Ramda.NET
{
    public static partial class R
    {
        public static dynamic Both(Delegate f, Delegate g) {
            return Currying.Both(new DelegateDecorator(f), new DelegateDecorator(g));
        }

        public static dynamic Both(dynamic f, Delegate g) {
            return Currying.Both(f, new DelegateDecorator(g));
        }

        public static dynamic Both(Delegate f, dynamic g) {
            return Currying.Both(new DelegateDecorator(f), g);
        }
    }
}
