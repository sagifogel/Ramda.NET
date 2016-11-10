using System;

namespace Ramda.NET
{
    internal class XWrap : ITransformer
    {
        private readonly dynamic f;

        internal XWrap(DynamicDelegate fn) {
            f = fn;
        }

        public dynamic Init() {
            throw new NotImplementedException("init not implemented on XWrap");
        }

        public dynamic Result(object acc) {
            return acc;
        }

        public dynamic Step(object acc, object x) {
            return f(acc, x);
        }
    }
}
