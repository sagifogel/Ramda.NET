using System;

namespace Ramda.NET
{
    internal class XFindLast : AbstractXFindLast
    {
        protected override object last { get; set; }

        internal XFindLast(Func<object, bool> f, ITransformer xf) : base(f, xf) {
        }

        protected override object GetStepLastValue(object input) => input;
    }
}
