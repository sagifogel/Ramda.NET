using System;

namespace Ramda.NET
{
    internal abstract class AbstractXFindLast : AbstractXPredicate
    {
        protected abstract object last { get; set; }

        internal AbstractXFindLast(DynamicDelegate f, ITransformer xf) : base(f, xf) {
        }

        public override object Result(object result) {
            return xf.Result(xf.Step(result, this.last));
        }

        public override object Step(object result, object input) {
            if (f(input)) {
                last = GetStepLastValue(input);
            }

            return result;
        }

        protected abstract object GetStepLastValue(object input);
    }
}
