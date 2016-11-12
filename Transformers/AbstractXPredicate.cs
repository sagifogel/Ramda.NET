using System;

namespace Ramda.NET
{
    internal abstract class AbstractXPredicate : AbstractXFn<bool>
    {
        internal AbstractXPredicate(DynamicDelegate f, ITransformer xf) : base(f, xf) {
        }
    }
}
