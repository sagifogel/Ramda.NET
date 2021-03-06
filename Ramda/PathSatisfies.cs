﻿// <autogenerated>
//   This file was generated by T4 code generator PathSatisfies.tt.
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
		/// Returns `true` if the specified object property at given path satisfies thegiven predicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> [Idx] -> {a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="propPath">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropSatisfies"/>
		/// <see cref="R.Path"/>
		public static dynamic PathSatisfies<TArg, TTarget>(Func<TArg, bool> pred, IList propPath, TTarget obj) {
			return Currying.PathSatisfies(Delegate(pred), propPath, obj);
		}

		/// <summary>
		/// Returns `true` if the specified object property at given path satisfies thegiven predicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> [Idx] -> {a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="propPath">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropSatisfies"/>
		/// <see cref="R.Path"/>
		public static dynamic PathSatisfies<TTarget>(RamdaPlaceholder pred, IList propPath, TTarget obj) {
			return Currying.PathSatisfies(pred, propPath, obj);
		}

		/// <summary>
		/// Returns `true` if the specified object property at given path satisfies thegiven predicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> [Idx] -> {a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="propPath">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropSatisfies"/>
		/// <see cref="R.Path"/>
		public static dynamic PathSatisfies<TArg, TTarget>(Func<TArg, bool> pred, RamdaPlaceholder propPath, TTarget obj) {
			return Currying.PathSatisfies(Delegate(pred), propPath, obj);
		}

		/// <summary>
		/// Returns `true` if the specified object property at given path satisfies thegiven predicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> [Idx] -> {a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="propPath">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropSatisfies"/>
		/// <see cref="R.Path"/>
		public static dynamic PathSatisfies<TArg>(Func<TArg, bool> pred, IList propPath, RamdaPlaceholder obj = null) {
			return Currying.PathSatisfies(Delegate(pred), propPath, obj);
		}

		/// <summary>
		/// Returns `true` if the specified object property at given path satisfies thegiven predicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> [Idx] -> {a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="propPath">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropSatisfies"/>
		/// <see cref="R.Path"/>
		public static dynamic PathSatisfies<TArg>(Func<TArg, bool> pred, RamdaPlaceholder propPath = null, RamdaPlaceholder obj = null) {
			return Currying.PathSatisfies(Delegate(pred), propPath, obj);
		}

		/// <summary>
		/// Returns `true` if the specified object property at given path satisfies thegiven predicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> [Idx] -> {a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="propPath">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropSatisfies"/>
		/// <see cref="R.Path"/>
		public static dynamic PathSatisfies<TTarget>(dynamic pred, RamdaPlaceholder propPath, TTarget obj) {
			return Currying.PathSatisfies(Delegate(pred), propPath, obj);
		}

		/// <summary>
		/// Returns `true` if the specified object property at given path satisfies thegiven predicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> [Idx] -> {a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="propPath">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropSatisfies"/>
		/// <see cref="R.Path"/>
		public static dynamic PathSatisfies(dynamic pred, IList propPath, RamdaPlaceholder obj = null) {
			return Currying.PathSatisfies(Delegate(pred), propPath, obj);
		}

		/// <summary>
		/// Returns `true` if the specified object property at given path satisfies thegiven predicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> [Idx] -> {a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="propPath">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropSatisfies"/>
		/// <see cref="R.Path"/>
		public static dynamic PathSatisfies(dynamic pred, RamdaPlaceholder propPath = null, RamdaPlaceholder obj = null) {
			return Currying.PathSatisfies(Delegate(pred), propPath, obj);
		}

		/// <summary>
		/// Returns `true` if the specified object property at given path satisfies thegiven predicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> [Idx] -> {a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="propPath">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropSatisfies"/>
		/// <see cref="R.Path"/>
		public static dynamic PathSatisfies<TTarget>(dynamic pred, IList propPath, TTarget obj) {
			return Currying.PathSatisfies(Delegate(pred), propPath, obj);
		}

		/// <summary>
		/// Returns `true` if the specified object property at given path satisfies thegiven predicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> [Idx] -> {a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="propPath">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropSatisfies"/>
		/// <see cref="R.Path"/>
		public static dynamic PathSatisfies(RamdaPlaceholder pred = null, RamdaPlaceholder propPath = null, RamdaPlaceholder obj = null) {
			return Currying.PathSatisfies(pred, propPath, obj);
		}
	}
}