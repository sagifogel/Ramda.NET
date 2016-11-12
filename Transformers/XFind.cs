using System;

namespace Ramda.NET
{
    internal class XFind : AbstractXFind
    {
        protected override object defaultValue { get; } = null;

        internal XFind(DynamicDelegate f, ITransformer xf) : base(f, xf) {
        }

        protected override object GetStepInputValue(object input) => input;
    }
}
