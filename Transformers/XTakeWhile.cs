using System;
using static Ramda.NET.Core;

namespace Ramda.NET
{
    internal class XTakeWhile : AbstractXPredicate
    {
        internal XTakeWhile(Func<object, bool> f, ITransformer xf) : base(f, xf) {
        }

        public override object Step(object result, object input) {
            return f(input) ? xf.Step(result, input) : Reduced(result);
        }
    }
}
