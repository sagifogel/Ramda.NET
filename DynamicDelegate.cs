using System;
using System.Dynamic;
using Reflection = Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    /// <summary>
    /// Provides a base class for specifying dynamic invocation behavior at run time
    /// </summary>
    public abstract class DynamicDelegate : DynamicObject
    {   
        /// <summary>
        /// The Arity of the function
        /// </summary>
        public int Length { get; protected set; }

        internal TResult DynamicInvoke<TResult>(params object[] arguments) {
            return (TResult)Reflection.DynamicInvoke((dynamic)this, arguments);
        }

        internal object DynamicInvoke(params object[] arguments) {
            return Reflection.DynamicInvoke((dynamic)this, arguments);
        }

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject" /> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject" /> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result" />.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result) {
            if (binder.Name.Equals("Length")) {
                result = Length;
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        internal int Arity() {
            return Length;
        }

        internal abstract Delegate Unwrap();
    }
}
