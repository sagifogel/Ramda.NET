﻿	// <autogenerated>
//   This file was generated by T4 code generator Adjust.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

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
		/// Applies a function to the value at the given index of an array, returning anew copy of the array with the element at the given index replaced with theresult of the function application.
		/// <para />
		/// sig: (a -> a) -> Number -> [a] -> [a]
		/// </summary>
		/// <param name="fn">The function to apply.</param>
		/// <param name="idx">The index.</param>
		/// <param name="list">An array-like object whose value       at the supplied index will be replaced.</param>
		/// <returns>A copy of the supplied array-like object with the element at index `idx` replaced with the value returned by applying `fn` to the existing element.</returns>
		/// <see cref="R.Update"/>
		public static dynamic Adjust<TSource>(Func<TSource, TSource> fn, int idx, IList<TSource> list) {
			return Currying.Adjust(Delegate(fn), idx, list);
		}

		/// <summary>
		/// Applies a function to the value at the given index of an array, returning anew copy of the array with the element at the given index replaced with theresult of the function application.
		/// <para />
		/// sig: (a -> a) -> Number -> [a] -> [a]
		/// </summary>
		/// <param name="fn">The function to apply.</param>
		/// <param name="idx">The index.</param>
		/// <param name="list">An array-like object whose value       at the supplied index will be replaced.</param>
		/// <returns>A copy of the supplied array-like object with the element at index `idx` replaced with the value returned by applying `fn` to the existing element.</returns>
		/// <see cref="R.Update"/>
		public static dynamic Adjust<TSource>(RamdaPlaceholder fn, int idx, IList<TSource> list) {
			return Currying.Adjust(fn, idx, list);
		}

		/// <summary>
		/// Applies a function to the value at the given index of an array, returning anew copy of the array with the element at the given index replaced with theresult of the function application.
		/// <para />
		/// sig: (a -> a) -> Number -> [a] -> [a]
		/// </summary>
		/// <param name="fn">The function to apply.</param>
		/// <param name="idx">The index.</param>
		/// <param name="list">An array-like object whose value       at the supplied index will be replaced.</param>
		/// <returns>A copy of the supplied array-like object with the element at index `idx` replaced with the value returned by applying `fn` to the existing element.</returns>
		/// <see cref="R.Update"/>
		public static dynamic Adjust<TSource>(Func<TSource, TSource> fn, RamdaPlaceholder idx, IList<TSource> list) {
			return Currying.Adjust(Delegate(fn), idx, list);
		}

		/// <summary>
		/// Applies a function to the value at the given index of an array, returning anew copy of the array with the element at the given index replaced with theresult of the function application.
		/// <para />
		/// sig: (a -> a) -> Number -> [a] -> [a]
		/// </summary>
		/// <param name="fn">The function to apply.</param>
		/// <param name="idx">The index.</param>
		/// <param name="list">An array-like object whose value       at the supplied index will be replaced.</param>
		/// <returns>A copy of the supplied array-like object with the element at index `idx` replaced with the value returned by applying `fn` to the existing element.</returns>
		/// <see cref="R.Update"/>
		public static dynamic Adjust<TSource>(Func<TSource, TSource> fn, int idx, RamdaPlaceholder list = null) {
			return Currying.Adjust(Delegate(fn), idx, list);
		}

		/// <summary>
		/// Applies a function to the value at the given index of an array, returning anew copy of the array with the element at the given index replaced with theresult of the function application.
		/// <para />
		/// sig: (a -> a) -> Number -> [a] -> [a]
		/// </summary>
		/// <param name="fn">The function to apply.</param>
		/// <param name="idx">The index.</param>
		/// <param name="list">An array-like object whose value       at the supplied index will be replaced.</param>
		/// <returns>A copy of the supplied array-like object with the element at index `idx` replaced with the value returned by applying `fn` to the existing element.</returns>
		/// <see cref="R.Update"/>
		public static dynamic Adjust<TSource>(Func<TSource, TSource> fn, RamdaPlaceholder idx = null, RamdaPlaceholder list = null) {
			return Currying.Adjust(Delegate(fn), idx, list);
		}

		/// <summary>
		/// Applies a function to the value at the given index of an array, returning anew copy of the array with the element at the given index replaced with theresult of the function application.
		/// <para />
		/// sig: (a -> a) -> Number -> [a] -> [a]
		/// </summary>
		/// <param name="fn">The function to apply.</param>
		/// <param name="idx">The index.</param>
		/// <param name="list">An array-like object whose value       at the supplied index will be replaced.</param>
		/// <returns>A copy of the supplied array-like object with the element at index `idx` replaced with the value returned by applying `fn` to the existing element.</returns>
		/// <see cref="R.Update"/>
		public static dynamic Adjust<TSource>(dynamic fn, RamdaPlaceholder idx, IList<TSource> list) {
			return Currying.Adjust(Delegate(fn), idx, list);
		}

		/// <summary>
		/// Applies a function to the value at the given index of an array, returning anew copy of the array with the element at the given index replaced with theresult of the function application.
		/// <para />
		/// sig: (a -> a) -> Number -> [a] -> [a]
		/// </summary>
		/// <param name="fn">The function to apply.</param>
		/// <param name="idx">The index.</param>
		/// <param name="list">An array-like object whose value       at the supplied index will be replaced.</param>
		/// <returns>A copy of the supplied array-like object with the element at index `idx` replaced with the value returned by applying `fn` to the existing element.</returns>
		/// <see cref="R.Update"/>
		public static dynamic Adjust(dynamic fn, int idx, RamdaPlaceholder list = null) {
			return Currying.Adjust(Delegate(fn), idx, list);
		}

		/// <summary>
		/// Applies a function to the value at the given index of an array, returning anew copy of the array with the element at the given index replaced with theresult of the function application.
		/// <para />
		/// sig: (a -> a) -> Number -> [a] -> [a]
		/// </summary>
		/// <param name="fn">The function to apply.</param>
		/// <param name="idx">The index.</param>
		/// <param name="list">An array-like object whose value       at the supplied index will be replaced.</param>
		/// <returns>A copy of the supplied array-like object with the element at index `idx` replaced with the value returned by applying `fn` to the existing element.</returns>
		/// <see cref="R.Update"/>
		public static dynamic Adjust(dynamic fn, RamdaPlaceholder idx = null, RamdaPlaceholder list = null) {
			return Currying.Adjust(Delegate(fn), idx, list);
		}

		/// <summary>
		/// Applies a function to the value at the given index of an array, returning anew copy of the array with the element at the given index replaced with theresult of the function application.
		/// <para />
		/// sig: (a -> a) -> Number -> [a] -> [a]
		/// </summary>
		/// <param name="fn">The function to apply.</param>
		/// <param name="idx">The index.</param>
		/// <param name="list">An array-like object whose value       at the supplied index will be replaced.</param>
		/// <returns>A copy of the supplied array-like object with the element at index `idx` replaced with the value returned by applying `fn` to the existing element.</returns>
		/// <see cref="R.Update"/>
		public static dynamic Adjust<TSource>(dynamic fn, int idx, IList<TSource> list) {
			return Currying.Adjust(Delegate(fn), idx, list);
		}

		/// <summary>
		/// Applies a function to the value at the given index of an array, returning anew copy of the array with the element at the given index replaced with theresult of the function application.
		/// <para />
		/// sig: (a -> a) -> Number -> [a] -> [a]
		/// </summary>
		/// <param name="fn">The function to apply.</param>
		/// <param name="idx">The index.</param>
		/// <param name="list">An array-like object whose value       at the supplied index will be replaced.</param>
		/// <returns>A copy of the supplied array-like object with the element at index `idx` replaced with the value returned by applying `fn` to the existing element.</returns>
		/// <see cref="R.Update"/>
		public static dynamic Adjust(RamdaPlaceholder fn = null, RamdaPlaceholder idx = null, RamdaPlaceholder list = null) {
			return Currying.Adjust(fn, idx, list);
		}
	}
}