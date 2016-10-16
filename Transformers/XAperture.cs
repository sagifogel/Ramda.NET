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
            copy = Core.Concat(Core.Slice(acc, pos), Core.Slice(acc, 0, pos));

            return full ? xf.Step(result, copy) : result;
        }
    }
}
