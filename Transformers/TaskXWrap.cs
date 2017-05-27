using System;
using System.Threading.Tasks;

namespace Ramda.NET
{
    internal class TaskXWrap
    {
        private readonly Func<AwaitableDynamicDelegate, AwaitableDynamicDelegate, AwaitableDynamicDelegate> f;
        
        internal TaskXWrap(Func<AwaitableDynamicDelegate, AwaitableDynamicDelegate, AwaitableDynamicDelegate> pipe) {
            f = pipe;
        }

        public dynamic Init() {
            throw new NotImplementedException("init not implemented on TaskXWrap");
        }

        public dynamic Result(object acc) {
            return acc;
        }

        public AwaitableDynamicDelegate Step(AwaitableDynamicDelegate acc, AwaitableDynamicDelegate x) {
            return f(acc, x);
        }
    }
}
