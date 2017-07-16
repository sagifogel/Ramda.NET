using System;
using System.Dynamic;
using System.Collections;
using static Ramda.NET.Currying;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ramda.NET
{
    public static partial class R
    {
        /// <summary>
        /// Creates a function that will process either the `onTrue` or the `onFalse`function depending upon the result of the `condition` predicate.
        /// <para />
        /// sig: (*... -> Boolean) -> (*... -> *) -> (*... -> *) -> (*... -> *)
        /// </summary>
        /// <param name="condition">A predicate function</param>
        /// <param name="onTrue">A function to invoke when the `condition` evaluates to a truthy value.</param>
        /// <param name="onFalse">A function to invoke when the `condition` evaluates to a falsy value.</param>
        /// <returns>A new unary function that will process either the `onTrue` or the `onFalse`     function depending upon the result of the `condition` predicate.</returns>
        /// <see cref="R.Unless"/>
        /// <see cref="R.When"/>
        public static dynamic IfElse(dynamic condition, dynamic onTrue, Delegate onFalse) {
            return Currying.IfElse(Delegate(condition), Delegate(onTrue), Delegate(onFalse));
        }

        /// <summary>
		/// Creates a function that will process either the `onTrue` or the `onFalse`function depending upon the result of the `condition` predicate.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> *) -> (*... -> *) -> (*... -> *)
		/// </summary>
		/// <param name="condition">A predicate function</param>
		/// <param name="onTrue">A function to invoke when the `condition` evaluates to a truthy value.</param>
		/// <param name="onFalse">A function to invoke when the `condition` evaluates to a falsy value.</param>
		/// <returns>A new unary function that will process either the `onTrue` or the `onFalse`     function depending upon the result of the `condition` predicate.</returns>
		/// <see cref="R.Unless"/>
		/// <see cref="R.When"/>
        public static dynamic IfElse(dynamic condition, Delegate onTrue, dynamic onFalse) {
            return Currying.IfElse(Delegate(condition), Delegate(onTrue), Delegate(onFalse));
        }

        /// <summary>
		/// Creates a function that will process either the `onTrue` or the `onFalse`function depending upon the result of the `condition` predicate.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> *) -> (*... -> *) -> (*... -> *)
		/// </summary>
		/// <param name="condition">A predicate function</param>
		/// <param name="onTrue">A function to invoke when the `condition` evaluates to a truthy value.</param>
		/// <param name="onFalse">A function to invoke when the `condition` evaluates to a falsy value.</param>
		/// <returns>A new unary function that will process either the `onTrue` or the `onFalse`     function depending upon the result of the `condition` predicate.</returns>
		/// <see cref="R.Unless"/>
		/// <see cref="R.When"/>
        public static dynamic IfElse(Delegate condition, dynamic onTrue, dynamic onFalse) {
            return Currying.IfElse(Delegate(condition), Delegate(onTrue), Delegate(onFalse));
        }

        /// <summary>
		/// Creates a function that will process either the `onTrue` or the `onFalse`function depending upon the result of the `condition` predicate.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> *) -> (*... -> *) -> (*... -> *)
		/// </summary>
		/// <param name="condition">A predicate function</param>
		/// <param name="onTrue">A function to invoke when the `condition` evaluates to a truthy value.</param>
		/// <param name="onFalse">A function to invoke when the `condition` evaluates to a falsy value.</param>
		/// <returns>A new unary function that will process either the `onTrue` or the `onFalse`     function depending upon the result of the `condition` predicate.</returns>
		/// <see cref="R.Unless"/>
		/// <see cref="R.When"/>
        public static dynamic IfElse(dynamic condition, dynamic onTrue, dynamic onFalse) {
            return Currying.IfElse(Delegate(condition), Delegate(onTrue), Delegate(onFalse));
        }
    }
}