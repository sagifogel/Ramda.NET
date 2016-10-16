using System;

namespace Ramda.NET
{
    internal class XDropWhile : AbstractXPredicate
    {
        internal XDropWhile(Func<object, bool> f, ITransformer xf) : base(f, xf) {
            this.f = f;
        }

        public override object Step(object result, object input) {
            if (f.IsNotNull()) {
                if (f(input)) {
                    return result;
                }

                f = null;
            }

            return base.xf.Step(result, input);
        }
    }
}
