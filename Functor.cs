using System;

namespace Ramda.NET
{
    public static partial class R
    {
        /// <summary>
        /// Provides a class to map between categories
        /// </summary>
        public class Functor
        {
            /// <summary>
            /// Gets or sets the value.
            /// </summary>
            /// <value>
            /// The value.
            /// </value>
            public object Value { get; set; }
            /// <summary>
            /// Gets or sets the mapping function.
            /// </summary>
            /// <value>
            /// The map.
            /// </value>
            public Delegate Map { get; set; }
        }
    }
}
