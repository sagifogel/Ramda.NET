using System;

namespace Ramda.NET
{
    internal abstract class AbstractXFind : AbstractXPredicate
    {
        private bool found = false;
        protected abstract object defaultValue { get; }

        internal AbstractXFind(Func<object, bool> f, ITransformer xf) : base(f, xf) {
        }

        public override object Result(object result) {
            if (!found) {
                result = this.xf.Step(result, defaultValue);
            }

            return base.Result(result);
        }

        public override object Step(object result, object input) {
            if (f(input)) {
                found = true;
                result = Core.Reduced(this.xf.Step(result, GetStepInputValue(input)));
            }

            return result;
        }

        protected abstract object GetStepInputValue(object input);
    }
}
