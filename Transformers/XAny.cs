using System;
using static Ramda.NET.Currying;

namespace Ramda.NET
{
    internal class XAny : AbstractXAnyOrAll
    {
        internal XAny(Func<object, bool> f, ITransformer xf) : base(f, xf, false, any => any == false, step => step) {
        }
    }
}
