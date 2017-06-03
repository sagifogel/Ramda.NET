using System;

namespace Ramda.NET
{
    internal class XMap : AbstractXTransform
    {
        internal XMap(DynamicDelegate f, ITransformer xf) : base(f, xf) {
        }

        public override object Step(object result, object input) {
            return xf.Step(result, f.DynamicInvoke(input));
        }
    }
}
