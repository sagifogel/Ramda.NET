using System;

namespace Ramda.NET
{
    internal abstract class AbstractXAperture : XFBase<ITransformer>, ITransformer
    {
        protected int pos;
        protected bool full;
        protected object[] acc;

        internal AbstractXAperture(int n, ITransformer xf) : base(xf) {
            pos = 0;
            full = false;
            acc = new object[n];
        }

        public override object Result(object result) {
            this.acc = null;

            return base.Result(result);
        }

        public abstract object Step(object result, object input);

        protected void Store(object input) {
            acc[pos] = input;
            pos += 1;

            if (pos == acc.Length) {
                pos = 0;
                full = true;
            }
        }
    }
}
