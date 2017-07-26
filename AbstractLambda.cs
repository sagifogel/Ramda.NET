using System;
using System.Dynamic;

namespace Ramda.NET
{
    /// <summary>
    /// Provides a base class for specifying dynamic invocation behavior at run time
    /// </summary>
    public abstract class AbstractLambda : DynamicDelegate
    {
        /// <summary>
        /// The function
        /// </summary>
        protected readonly dynamic fn;

        /// <summary>
        /// Provides a base class for specifying dynamic invocation behavior at run time
        /// </summary>
        public AbstractLambda(DynamicDelegate fn, int? length = null) {
            this.fn = fn;
            Length = length ?? fn.Length;
        }

        /// <summary>
        /// Provides the implementation for operations that invoke an object. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as invoking an object or a delegate.
        /// </summary>
        /// <param name="binder">Provides information about the invoke operation.</param>
        /// <param name="args">The arguments that are passed to the object during the invoke operation. For example, for the sampleObject(100) operation, where sampleObject is derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, <paramref name="args[0]" /> is equal to 100.</param>
        /// <param name="result">The result of the object invocation.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a language-specific run-time exception is thrown.
        /// </returns>
        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result) {
            result = TryInvoke(binder, args);
            return true;
        }

        /// <summary>
        /// Provides the implementation for operations that invoke an object. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as invoking an object or a delegate.
        /// </summary>
        /// <param name="binder">The binder.</param>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        protected abstract object TryInvoke(InvokeBinder binder, object[] args);


        internal override Delegate Unwrap() {
            return fn.Unwrap();
        }
    }
}
