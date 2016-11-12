using System;

namespace Ramda.NET
{
    internal abstract class AbstractXTransform : AbstractXFn<object>
    {
        internal AbstractXTransform(DynamicDelegate f, ITransformer xf) : base(f, xf) {
        }
    }
}