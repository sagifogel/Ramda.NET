using System;

namespace Ramda.NET
{
    internal class XFindLastIndex : AbstractXFindLast
    {
        private int idx;
        protected override object last { get; set; } = -1;

        internal XFindLastIndex(Func<object, bool> f, ITransformer xf) : base(f, xf) {
            idx = -1;
        }

        public override object Step(object result, object input) {
            idx += 1;

            return base.Step(result, input);
        }

        protected override object GetStepLastValue(object input) => idx;
    }
}
