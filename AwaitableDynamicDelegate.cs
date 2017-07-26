using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Collections.Generic;
using static Ramda.NET.ReflectionExtensions;
using System.Threading;

namespace Ramda.NET
{
    /// <summary>
    /// Provides a class for specifying dynamic async invocation behavior at run time
    /// </summary>
    public class AwaitableDynamicDelegate : DynamicDelegate
    {
        /// <summary>
        /// The arguments
        /// </summary>
        protected object[] arguments;
        private readonly Delegate fn;

        /// <summary>
        /// Initializes a new instance of the <see cref="AwaitableDynamicDelegate"/> class.
        /// </summary>
        /// <param name="fn">The function.</param>
        public AwaitableDynamicDelegate(Delegate fn) {
            this.fn = fn;
            Length = fn.Method.GetParameters().Length;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwaitableDynamicDelegate"/> class.
        /// </summary>
        /// <param name="fn">The function.</param>
        internal AwaitableDynamicDelegate(Func<dynamic[], Task<dynamic>> fn) {
            this.fn = fn;
        }

        /// <summary>
        /// Provides the implementation for operations that invoke an object. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as invoking an object or a delegate.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="result">The result.</param>
        /// <returns></returns>
        public override bool TryInvoke(InvokeBinder binder, object[] arguments, out object result) {
            this.arguments = arguments;
            result = this;

            return true;
        }

        /// <summary>
        /// Gets the awaiter.
        /// </summary>
        /// <returns></returns>
        public dynamic GetAwaiter() {
            dynamic task = fn.Invoke(arguments);

            return task.GetAwaiter();
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public dynamic Result => GetAwaiter().GetResult();

        internal override Delegate Unwrap() {
            return null;
        }
    }
}
