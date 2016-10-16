using System;

namespace Ramda.NET
{
    internal class XDropLast : AbstractXAperture
    {
        internal XDropLast(int n, ITransformer xf) : base(n, xf) {
        }

        public override object Step(object result, object input) {
            if (full) {
                result = xf.Step(result, this.acc[this.pos]);
            }

            Store(input);

            return result;
        }
    }
}
