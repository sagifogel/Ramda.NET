using System;

namespace Ramda.NET
{
    /// <summary>
    ///  Provides a continuation context as a result of calling to <see cref="PromiseLikeDynamicDelegate" />Then method
    /// </summary>
    public class PromiseContinuation
    {
        private dynamic value;

        /// <summary>
        /// Initializes a new instance of the <see cref="PromiseContinuation"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public PromiseContinuation(dynamic value) {
            this.value = value;
        }

        /// <summary>
        /// Gets a continuation callback and invokes it.
        /// </summary>
        /// <param name="continuation">The continuation.</param>
        /// <returns></returns>
        public PromiseContinuation Then(Func<dynamic, dynamic> continuation) {
            value = continuation(value);

            return this;
        }
    }
}
