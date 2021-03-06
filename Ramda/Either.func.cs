﻿// <autogenerated>
//   This file was generated by T4 code generator Either.func.tt.
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
		/// A function wrapping calls to the two functions in an `||` operation,returning the result of the first function if it is truth-y and the resultof the second function otherwise. Note that this is short-circuited,meaning that the second function will not be invoked if the first returns atruth-y value.In addition to functions, `R.either` also accepts any fantasy-land compatibleapplicative functor.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> Boolean) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">a predicate</param>
		/// <param name="g">another predicate</param>
		/// <returns>a function that applies its arguments to `f` and `g` and `||`s their outputs together.</returns>
		/// <see cref="R.Or"/>
		public static dynamic Either<TSource>(Func<TSource, bool> f, Func<TSource, bool> g) {
			return Currying.Either(Delegate(f), Delegate(g));
		}

		/// <summary>
		/// A function wrapping calls to the two functions in an `||` operation,returning the result of the first function if it is truth-y and the resultof the second function otherwise. Note that this is short-circuited,meaning that the second function will not be invoked if the first returns atruth-y value.In addition to functions, `R.either` also accepts any fantasy-land compatibleapplicative functor.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> Boolean) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">a predicate</param>
		/// <param name="g">another predicate</param>
		/// <returns>a function that applies its arguments to `f` and `g` and `||`s their outputs together.</returns>
		/// <see cref="R.Or"/>
		public static dynamic Either<TSource>(RamdaPlaceholder f, Func<TSource, bool> g) {
			return Currying.Either(f, Delegate(g));
		}

		/// <summary>
		/// A function wrapping calls to the two functions in an `||` operation,returning the result of the first function if it is truth-y and the resultof the second function otherwise. Note that this is short-circuited,meaning that the second function will not be invoked if the first returns atruth-y value.In addition to functions, `R.either` also accepts any fantasy-land compatibleapplicative functor.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> Boolean) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">a predicate</param>
		/// <param name="g">another predicate</param>
		/// <returns>a function that applies its arguments to `f` and `g` and `||`s their outputs together.</returns>
		/// <see cref="R.Or"/>
		public static dynamic Either<TSource>(Func<TSource, bool> f, RamdaPlaceholder g = null) {
			return Currying.Either(Delegate(f), g);
		}

		/// <summary>
		/// A function wrapping calls to the two functions in an `||` operation,returning the result of the first function if it is truth-y and the resultof the second function otherwise. Note that this is short-circuited,meaning that the second function will not be invoked if the first returns atruth-y value.In addition to functions, `R.either` also accepts any fantasy-land compatibleapplicative functor.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> Boolean) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">a predicate</param>
		/// <param name="g">another predicate</param>
		/// <returns>a function that applies its arguments to `f` and `g` and `||`s their outputs together.</returns>
		/// <see cref="R.Or"/>
		public static dynamic Either(RamdaPlaceholder f, dynamic g) {
			return Currying.Either(f, Delegate(g));
		}

		/// <summary>
		/// A function wrapping calls to the two functions in an `||` operation,returning the result of the first function if it is truth-y and the resultof the second function otherwise. Note that this is short-circuited,meaning that the second function will not be invoked if the first returns atruth-y value.In addition to functions, `R.either` also accepts any fantasy-land compatibleapplicative functor.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> Boolean) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">a predicate</param>
		/// <param name="g">another predicate</param>
		/// <returns>a function that applies its arguments to `f` and `g` and `||`s their outputs together.</returns>
		/// <see cref="R.Or"/>
		public static dynamic Either(dynamic f, RamdaPlaceholder g = null) {
			return Currying.Either(Delegate(f), g);
		}

		/// <summary>
		/// A function wrapping calls to the two functions in an `||` operation,returning the result of the first function if it is truth-y and the resultof the second function otherwise. Note that this is short-circuited,meaning that the second function will not be invoked if the first returns atruth-y value.In addition to functions, `R.either` also accepts any fantasy-land compatibleapplicative functor.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> Boolean) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">a predicate</param>
		/// <param name="g">another predicate</param>
		/// <returns>a function that applies its arguments to `f` and `g` and `||`s their outputs together.</returns>
		/// <see cref="R.Or"/>
		public static dynamic Either<TSource>(dynamic f, Func<TSource, bool> g) {
			return Currying.Either(Delegate(f), Delegate(g));
		}

		/// <summary>
		/// A function wrapping calls to the two functions in an `||` operation,returning the result of the first function if it is truth-y and the resultof the second function otherwise. Note that this is short-circuited,meaning that the second function will not be invoked if the first returns atruth-y value.In addition to functions, `R.either` also accepts any fantasy-land compatibleapplicative functor.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> Boolean) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">a predicate</param>
		/// <param name="g">another predicate</param>
		/// <returns>a function that applies its arguments to `f` and `g` and `||`s their outputs together.</returns>
		/// <see cref="R.Or"/>
		public static dynamic Either<TSource>(Func<TSource, bool> f, dynamic g) {
			return Currying.Either(Delegate(f), Delegate(g));
		}

		/// <summary>
		/// A function wrapping calls to the two functions in an `||` operation,returning the result of the first function if it is truth-y and the resultof the second function otherwise. Note that this is short-circuited,meaning that the second function will not be invoked if the first returns atruth-y value.In addition to functions, `R.either` also accepts any fantasy-land compatibleapplicative functor.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> Boolean) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">a predicate</param>
		/// <param name="g">another predicate</param>
		/// <returns>a function that applies its arguments to `f` and `g` and `||`s their outputs together.</returns>
		/// <see cref="R.Or"/>
		public static dynamic Either(RamdaPlaceholder f = null, RamdaPlaceholder g = null) {
			return Currying.Either(f, g);
		}
	}
}