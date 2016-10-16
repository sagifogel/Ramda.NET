using System;

namespace Ramda.NET
{
    internal abstract class AbstractXTransform : AbstractXFn<object>
    {
        internal AbstractXTransform(Func<object, object> f, ITransformer xf) : base(f, xf) {
        }
    }
}