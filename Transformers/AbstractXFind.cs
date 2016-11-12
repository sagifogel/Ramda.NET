using System;
using static Ramda.NET.Currying;

namespace Ramda.NET
{
    internal abstract class AbstractXFind : AbstractXPredicate
    {
        private bool found = false;
        protected abstract object defaultValue { get; }

        internal AbstractXFind(DynamicDelegate f, ITransformer xf) : base(f, xf) {
        }

        public override object Result(object result) {
            if (!found) {
                result = xf.Step(result, defaultValue);
            }

            return base.Result(result);
        }

        public override object Step(object result, object input) {
            if (f(input)) {
                found = true;
                result = ReducedInternal(xf.Step(result, GetStepInputValue(input)));
            }

            return result;
        }

        protected abstract object GetStepInputValue(object input);
    }
}
