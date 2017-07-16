using System;
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
        /// Returns a single item by iterating through the list, successively callingthe iterator function and passing it an accumulator value and the currentvalue from the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It may use`R.reduced` to shortcut the iteration.The arguments' order of `reduceRight`'s iterator function is *(value, acc)*.Note: `R.reduce` does not skip deleted or unassigned indices (sparsearrays), unlike the native `Array.prototype.reduce` method. For more detailson this behavior, see:https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/reduce#DescriptionDispatches to the `reduce` method of the third argument, if present.
        /// <para />
        /// sig: ((a, b) -> a) -> a -> [b] -> a
        /// </summary>
        /// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array.</param>
        /// <param name="acc">The accumulator value.</param>
        /// <param name="list">The list to iterate over.</param>
        /// <returns>The final, accumulated value.</returns>
        /// <see cref="R.Reduced"/>
        /// <see cref="R.AddIndex"/>
        /// <see cref="R.Reduce"/>
        public static dynamic Reduce<TSource, TAccmulator, TReturn>(Func<TSource, TAccmulator, TReturn> fn, TAccmulator acc, IReducible list) {
			return Currying.Reduce(Delegate(fn), acc, list);
		}

        /// <summary>
		/// Returns a single item by iterating through the list, successively callingthe iterator function and passing it an accumulator value and the currentvalue from the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It may use`R.reduced` to shortcut the iteration.The arguments' order of `reduceRight`'s iterator function is *(value, acc)*.Note: `R.reduce` does not skip deleted or unassigned indices (sparsearrays), unlike the native `Array.prototype.reduce` method. For more detailson this behavior, see:https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/reduce#DescriptionDispatches to the `reduce` method of the third argument, if present.
		/// <para />
		/// sig: ((a, b) -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array.</param>
		/// <param name="acc">The accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.AddIndex"/>
		/// <see cref="R.Reduce"/>
		public static dynamic Reduce<TAccmulator>(RamdaPlaceholder fn, TAccmulator acc, IReducible list) {
			return Currying.Reduce(fn, acc, list);
		}

        /// <summary>
        /// Returns a single item by iterating through the list, successively callingthe iterator function and passing it an accumulator value and the currentvalue from the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It may use`R.reduced` to shortcut the iteration.The arguments' order of `reduceRight`'s iterator function is *(value, acc)*.Note: `R.reduce` does not skip deleted or unassigned indices (sparsearrays), unlike the native `Array.prototype.reduce` method. For more detailson this behavior, see:https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/reduce#DescriptionDispatches to the `reduce` method of the third argument, if present.
        /// <para />
        /// sig: ((a, b) -> a) -> a -> [b] -> a
        /// </summary>
        /// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array.</param>
        /// <param name="acc">The accumulator value.</param>
        /// <param name="list">The list to iterate over.</param>
        /// <returns>The final, accumulated value.</returns>
        /// <see cref="R.Reduced"/>
        /// <see cref="R.AddIndex"/>
        /// <see cref="R.Reduce"/>
        public static dynamic Reduce<TSource, TAccmulator, TReturn>(Func<TSource, TAccmulator, TReturn> fn, RamdaPlaceholder acc, IReducible list) {
			return Currying.Reduce(Delegate(fn), acc, list);
		}

        /// <summary>
		/// Returns a single item by iterating through the list, successively callingthe iterator function and passing it an accumulator value and the currentvalue from the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It may use`R.reduced` to shortcut the iteration.The arguments' order of `reduceRight`'s iterator function is *(value, acc)*.Note: `R.reduce` does not skip deleted or unassigned indices (sparsearrays), unlike the native `Array.prototype.reduce` method. For more detailson this behavior, see:https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/reduce#DescriptionDispatches to the `reduce` method of the third argument, if present.
		/// <para />
		/// sig: ((a, b) -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array.</param>
		/// <param name="acc">The accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.AddIndex"/>
		/// <see cref="R.Reduce"/>
		public static dynamic Reduce(dynamic fn, RamdaPlaceholder acc, IReducible list) {
			return Currying.Reduce(Delegate(fn), acc, list);
		}

        /// <summary>
		/// Returns a single item by iterating through the list, successively callingthe iterator function and passing it an accumulator value and the currentvalue from the array, and then passing the result to the next call.The iterator function receives two values: *(acc, value)*. It may use`R.reduced` to shortcut the iteration.The arguments' order of `reduceRight`'s iterator function is *(value, acc)*.Note: `R.reduce` does not skip deleted or unassigned indices (sparsearrays), unlike the native `Array.prototype.reduce` method. For more detailson this behavior, see:https://developer.mozilla.org/en-US/docs/Web/JavaScript/Reference/Global_Objects/Array/reduce#DescriptionDispatches to the `reduce` method of the third argument, if present.
		/// <para />
		/// sig: ((a, b) -> a) -> a -> [b] -> a
		/// </summary>
		/// <param name="fn">The iterator function. Receives two values, the accumulator and the       current element from the array.</param>
		/// <param name="acc">The accumulator value.</param>
		/// <param name="list">The list to iterate over.</param>
		/// <returns>The final, accumulated value.</returns>
		/// <see cref="R.Reduced"/>
		/// <see cref="R.AddIndex"/>
		/// <see cref="R.Reduce"/>
		public static dynamic Reduce<TAccmulator>(dynamic fn, TAccmulator acc, IReducible list) {
			return Currying.Reduce(Delegate(fn), acc, list);
		}
	}
}