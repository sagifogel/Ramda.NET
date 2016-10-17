using System;

namespace Ramda.NET
{
    internal class XFindIndex : AbstractXFind
    {
        private int idx;
        protected override object defaultValue { get; } = -1;

        internal XFindIndex(Func<object, bool> f, ITransformer xf) : base(f, xf) {
            idx = -1;
        }

        public override object Step(object result, object input) {
            idx += 1;

            return base.Step(result, input);
        }

        protected override object GetStepInputValue(object input) => idx;
    }
}
