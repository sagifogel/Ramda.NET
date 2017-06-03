using System;

namespace Ramda.NET
{
    internal abstract class AbstractXFn<TReturn> : XFBase<ITransformer>, ITransformer
    {
        protected DynamicDelegate f;

        internal AbstractXFn(DynamicDelegate f, ITransformer xf) : base(xf) {
            this.f = f;
        }

        public abstract object Step(object result, object input);
    }
}
