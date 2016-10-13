using System;

namespace Ramda.NET
{
    internal class XWrap : ITransducer
    {
        private readonly Delegate f;

        public XWrap(Delegate fn) {
            f = fn;
        }

        public dynamic Init() {
            throw new NotImplementedException("init not implemented on XWrap");
        }

        public dynamic Result(object acc) {
            return acc;
        }

        public dynamic Step(object acc, object x) {
            return f.Invoke(acc, x);
        }
    }
}
