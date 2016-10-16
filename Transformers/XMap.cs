using System;
using static Ramda.NET.Core;

namespace Ramda.NET
{
    internal class XMap : AbstractXTransform
    {
        internal XMap(Func<object, object> f, ITransformer xf) : base(f, xf) {
        }

        public override object Step(object result, object input) {
            return xf.Step(result, f(input));
        }
    }
}
