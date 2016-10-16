using System;

namespace Ramda.NET
{
    internal abstract class AbstractXFn<TReturn> : XFBase<ITransformer>, ITransformer
    {
        protected Func<object, TReturn> f;

        internal AbstractXFn(Func<object, TReturn> f, ITransformer xf) : base(xf) {
            this.f = f;
        }

        public abstract object Step(object result, object input);
    }
}
