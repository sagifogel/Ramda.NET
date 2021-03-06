﻿// <autogenerated>
//   This file was generated by T4 code generator Lift.tt.
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
		/// "lifts" a function of arity <![CDATA[>]]> 1 so that it may "map over" a list, Function or otherobject that satisfies the [FantasyLand Apply spec](https://github.com/fantasyland/fantasy-land#apply).
		/// <para />
		/// sig: (*... -> *) -> ([*]... -> [*])
		/// </summary>
		/// <param name="fn">The function to lift into higher context</param>
		/// <returns>The lifted function.</returns>
		/// <see cref="R.LiftN"/>
		public static dynamic Lift(Delegate fn) {
			return Currying.Lift(Delegate(fn));
		}

		/// <summary>
		/// "lifts" a function of arity <![CDATA[>]]> 1 so that it may "map over" a list, Function or otherobject that satisfies the [FantasyLand Apply spec](https://github.com/fantasyland/fantasy-land#apply).
		/// <para />
		/// sig: (*... -> *) -> ([*]... -> [*])
		/// </summary>
		/// <param name="fn">The function to lift into higher context</param>
		/// <returns>The lifted function.</returns>
		/// <see cref="R.LiftN"/>
		public static dynamic Lift(RamdaPlaceholder fn = null) {
			return Currying.Lift(fn);
		}

		/// <summary>
		/// "lifts" a function of arity <![CDATA[>]]> 1 so that it may "map over" a list, Function or otherobject that satisfies the [FantasyLand Apply spec](https://github.com/fantasyland/fantasy-land#apply).
		/// <para />
		/// sig: (*... -> *) -> ([*]... -> [*])
		/// </summary>
		/// <param name="fn">The function to lift into higher context</param>
		/// <returns>The lifted function.</returns>
		/// <see cref="R.LiftN"/>
		public static dynamic Lift(dynamic fn) {
			return Currying.Lift(Delegate(fn));
		}
	}
}