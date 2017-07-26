using System;

namespace Ramda.NET
{
    /// <summary>
    ///  Equivalent to null
    /// </summary>
    public class Nothing
    {
        internal Nothing() {
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) {
            if (obj is Nothing) {
                return true;
            }

            return Equals(obj, null);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        /// <summary>
        /// Performs an explicit conversion from <see cref="Nothing"/> to <see cref="System.Boolean"/>.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>
        /// The result of the conversion.
        /// </returns>
        public static explicit operator bool(Nothing x) {
            return false;
        }
    }
}
