using System;
using static Ramda.NET.Currying;

namespace Ramda.NET
{
    internal class XTakeWhile : AbstractXPredicate
    {
        internal XTakeWhile(DynamicDelegate f, ITransformer xf) : base(f, xf) {
        }

        public override object Step(object result, object input) {
            return f(input) ? xf.Step(result, input) : ReducedInternal(result);
        }
    }
}
