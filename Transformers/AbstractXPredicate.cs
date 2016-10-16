using System;

namespace Ramda.NET
{
    internal abstract class AbstractXPredicate : AbstractXFn<bool>
    {
        internal AbstractXPredicate(Func<object, bool> f, ITransformer xf) : base(f, xf) {
        }
    }
}
