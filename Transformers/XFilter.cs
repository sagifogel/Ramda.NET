using System;

namespace Ramda.NET
{
    internal class XFilter : AbstractXPredicate
    {
        internal XFilter(DynamicDelegate f, ITransformer xf) : base(f, xf) {
        }

        public override object Step(object result, object input) {
            return this.f(input) ? this.xf.Step(result, input) : result;
        }
    }
}
