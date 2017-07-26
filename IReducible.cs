using System;

namespace Ramda.NET
{
    public static partial class R
    {
        /// <summary>
        /// Returns a single item by iterating through the list, successively calling the iterator function and passing it an accumulator value and the current value from the array, and then passing the result to the next call.
        /// </summary>
        public interface IReducible
        {
            /// <summary>
            /// Reduces the specified step.
            /// </summary>
            /// <param name="step">The step.</param>
            /// <param name="acc">The acc.</param>
            /// <returns></returns>
            object Reduce(Func<object, object, object> step, object acc);
        }
    }
}
