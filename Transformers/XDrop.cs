using System;

namespace Ramda.NET
{
    internal class XDrop : XFBase<ITransformer>, ITransformer
    {
        private int n;

        internal XDrop(int n, ITransformer xf) : base(xf) {
            this.n = n;
        }

        public object Step(object result, object input) {
            if (n > 0) {
                n -= 1;

                return result;
            }

            return xf.Step(result, input);
        }
    }
}
