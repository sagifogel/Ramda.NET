using System;
using static Ramda.NET.Currying;

namespace Ramda.NET
{
    class XTake : XFBase<ITransformer>, ITransformer
    {
        private int n;
        private int i = 0;

        internal XTake(int n, ITransformer xf) : base(xf) {
            this.n = n;
        }

        public object Step(object result, object input) {
            object ret;

            i += 1;
            ret = n == 0 ? result : xf.Step(result, input);

            return i >= n ? ReducedInternal(ret) : ret;
        }
    }
}