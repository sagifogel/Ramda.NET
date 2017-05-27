using System;
using System.Dynamic;
using System.Threading.Tasks;

namespace Ramda.NET
{
    public class PromiseLikeDynamicDelegate : DynamicDelegate
    {
        private readonly dynamic f;
        private object[] arguments;

        internal PromiseLikeDynamicDelegate(AwaitableDynamicDelegate fn) {
            f = fn;
            Length = 1;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] arguments, out object result) {
            result = this;
            this.arguments = arguments;

            return true;
        }

        public PromiseContinuation Then(Func<dynamic, dynamic> continuation) {
            return new PromiseContinuation(f(arguments[0]).Result).Then(continuation);
        }

        public override Delegate Unwrap() {
            throw new NotImplementedException();
        }
    }
}
