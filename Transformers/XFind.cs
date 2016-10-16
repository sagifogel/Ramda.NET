using System;
using static Ramda.NET.Core;

namespace Ramda.NET
{
    internal class XFind : AbstractXFind
    {
        protected override object defaultValue { get; } = null;

        internal XFind(Func<object, bool> f, ITransformer xf) : base(f, xf) {
        }

        protected override object GetStepInputValue(object input) => input;
    }
}
