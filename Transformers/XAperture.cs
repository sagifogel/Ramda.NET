using System;

namespace Ramda.NET
{
    internal class XAperture : AbstractXAperture
    {
        internal XAperture(int n, ITransformer xf) : base(n, xf) {
        }

        public override object Step(object result, object input) {
            object copy;

            Store(input);
            copy = acc.Slice(pos).Concat(acc.Slice(0, pos));

            return full ? xf.Step(result, copy) : result;
        }
    }
}
