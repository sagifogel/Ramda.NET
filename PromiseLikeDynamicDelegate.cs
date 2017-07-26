using System;
using System.Dynamic;
using System.Threading.Tasks;
using Reflection = Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    /// <summary>
    /// Provides a class for specifying dynamic invocation of promise like behavior at run time
    /// </summary>
    /// <seealso cref="Ramda.NET.DynamicDelegate" />
    public class PromiseLikeDynamicDelegate : DynamicDelegate
    {
        private object[] arguments;
        private readonly DynamicDelegate f;

        internal PromiseLikeDynamicDelegate(AwaitableDynamicDelegate fn) {
            f = fn;
            Length = 1;
        }

        /// <summary>
        /// Provides the implementation for operations that invoke an object. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as invoking an object or a delegate.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public override bool TryInvoke(InvokeBinder binder, object[] arguments, out object result) {
            result = this;
            this.arguments = arguments;

            return true;
        }

        /// <summary>
        /// Creates a continuation that executes asynchronously when the target Dynamic Method completes.
        /// </summary>
        /// <param name="continuation"></param>
        /// <returns></returns>
        public PromiseContinuation Then(Func<dynamic, dynamic> continuation) {
            return new PromiseContinuation(f.DynamicInvoke<dynamic>(arguments).Result).Then(continuation);
        }

        internal override Delegate Unwrap() {
            throw new NotImplementedException();
        }
    }
}
