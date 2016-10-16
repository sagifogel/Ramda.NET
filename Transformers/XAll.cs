using System;
using static Ramda.NET.Currying;

namespace Ramda.NET
{
    internal class XAll : AbstractXAnyOrAll
    {
        internal XAll(Func<object, bool> f, ITransformer xf) : base(f, xf, true, all => all, step => step == false) {
        }
    }
}
