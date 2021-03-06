﻿// <autogenerated>
//   This file was generated by T4 code generator LiftN.tt.
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
		/// "lifts" a function to be the specified arity, so that it may "map over" thatmany lists, Functions or other objects that satisfy the [FantasyLand Apply spec](https://github.com/fantasyland/fantasy-land#apply).
		/// <para />
		/// sig: Number -> (*... -> *) -> ([*]... -> [*])
		/// </summary>
		/// <param name="fn">The function to lift into higher context</param>
		/// <returns>The lifted function.</returns>
		/// <see cref="R.Lift"/>
		/// <see cref="R.Ap"/>
		public static dynamic LiftN(int n, Delegate fn) {
			return Currying.LiftN(n, Delegate(fn));
		}

		/// <summary>
		/// "lifts" a function to be the specified arity, so that it may "map over" thatmany lists, Functions or other objects that satisfy the [FantasyLand Apply spec](https://github.com/fantasyland/fantasy-land#apply).
		/// <para />
		/// sig: Number -> (*... -> *) -> ([*]... -> [*])
		/// </summary>
		/// <param name="fn">The function to lift into higher context</param>
		/// <returns>The lifted function.</returns>
		/// <see cref="R.Lift"/>
		/// <see cref="R.Ap"/>
		public static dynamic LiftN(RamdaPlaceholder n, Delegate fn) {
			return Currying.LiftN(n, Delegate(fn));
		}

		/// <summary>
		/// "lifts" a function to be the specified arity, so that it may "map over" thatmany lists, Functions or other objects that satisfy the [FantasyLand Apply spec](https://github.com/fantasyland/fantasy-land#apply).
		/// <para />
		/// sig: Number -> (*... -> *) -> ([*]... -> [*])
		/// </summary>
		/// <param name="fn">The function to lift into higher context</param>
		/// <returns>The lifted function.</returns>
		/// <see cref="R.Lift"/>
		/// <see cref="R.Ap"/>
		public static dynamic LiftN(int n, RamdaPlaceholder fn = null) {
			return Currying.LiftN(n, fn);
		}

		/// <summary>
		/// "lifts" a function to be the specified arity, so that it may "map over" thatmany lists, Functions or other objects that satisfy the [FantasyLand Apply spec](https://github.com/fantasyland/fantasy-land#apply).
		/// <para />
		/// sig: Number -> (*... -> *) -> ([*]... -> [*])
		/// </summary>
		/// <param name="fn">The function to lift into higher context</param>
		/// <returns>The lifted function.</returns>
		/// <see cref="R.Lift"/>
		/// <see cref="R.Ap"/>
		public static dynamic LiftN(RamdaPlaceholder n, dynamic fn) {
			return Currying.LiftN(n, Delegate(fn));
		}

		/// <summary>
		/// "lifts" a function to be the specified arity, so that it may "map over" thatmany lists, Functions or other objects that satisfy the [FantasyLand Apply spec](https://github.com/fantasyland/fantasy-land#apply).
		/// <para />
		/// sig: Number -> (*... -> *) -> ([*]... -> [*])
		/// </summary>
		/// <param name="fn">The function to lift into higher context</param>
		/// <returns>The lifted function.</returns>
		/// <see cref="R.Lift"/>
		/// <see cref="R.Ap"/>
		public static dynamic LiftN(int n, dynamic fn) {
			return Currying.LiftN(n, Delegate(fn));
		}

		/// <summary>
		/// "lifts" a function to be the specified arity, so that it may "map over" thatmany lists, Functions or other objects that satisfy the [FantasyLand Apply spec](https://github.com/fantasyland/fantasy-land#apply).
		/// <para />
		/// sig: Number -> (*... -> *) -> ([*]... -> [*])
		/// </summary>
		/// <param name="fn">The function to lift into higher context</param>
		/// <returns>The lifted function.</returns>
		/// <see cref="R.Lift"/>
		/// <see cref="R.Ap"/>
		public static dynamic LiftN(RamdaPlaceholder n = null, RamdaPlaceholder fn = null) {
			return Currying.LiftN(n, fn);
		}
	}
}