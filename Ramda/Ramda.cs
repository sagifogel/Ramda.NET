using System;
using System.Linq;
using System.Dynamic;
using System.Collections;
using System.Threading.Tasks;
using static Ramda.NET.Currying;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ramda.NET
{
    public static partial class R
    {   
        /// <summary>
        /// Represents a null value. A Replacement for the null keyword.
        /// </summary>
        public readonly static Nothing @null = new Nothing();

        /// <summary>
        ///  A special placeholder value used to specify "gaps" within curried functions,
        ///  allowing partial application of any combination of arguments, regardless of
        ///  their positions.
        /// </summary>
        public readonly static RamdaPlaceholder __ = new RamdaPlaceholder();

        /// <summary>
        /// A function that always returns `true`. Any passed in parameters are ignored.
        /// <para />
        /// sig: * -> Boolean
        /// </summary>
        /// <returns>Function</returns>
        public static dynamic F = Delegate(() => Currying.F());

        /// <summary>
		/// A function that always returns `false`. Any passed in parameters are ignored.
		/// <para />
		/// sig: * -> Boolean
		/// </summary>
		/// <returns>Function</returns>
        public static dynamic T = Delegate(() => Currying.T());
    }
}