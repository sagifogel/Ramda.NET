﻿// <autogenerated>
//   This file was generated by T4 code generator Complement.tt.
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
		/// Takes a function `f` and returns a function `g` such that if called with the same argumentswhen `f` returns a "truthy" value, `g` returns `false` and when `f` returns a "falsy" value `g` returns `true`.`R.complement` may be applied to any functor
		/// <para />
		/// sig: (*... -> *) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">first</param>
		/// <returns>Function</returns>
		/// <see cref="R.Not"/>
		public static dynamic Complement<TSource>(Func<TSource, bool> f) {
			return Currying.Complement(Delegate(f));
		}

		/// <summary>
		/// Takes a function `f` and returns a function `g` such that if called with the same argumentswhen `f` returns a "truthy" value, `g` returns `false` and when `f` returns a "falsy" value `g` returns `true`.`R.complement` may be applied to any functor
		/// <para />
		/// sig: (*... -> *) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">first</param>
		/// <returns>Function</returns>
		/// <see cref="R.Not"/>
		public static dynamic Complement(RamdaPlaceholder f = null) {
			return Currying.Complement(f);
		}

		/// <summary>
		/// Takes a function `f` and returns a function `g` such that if called with the same argumentswhen `f` returns a "truthy" value, `g` returns `false` and when `f` returns a "falsy" value `g` returns `true`.`R.complement` may be applied to any functor
		/// <para />
		/// sig: (*... -> *) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">first</param>
		/// <returns>Function</returns>
		/// <see cref="R.Not"/>
		public static dynamic Complement(dynamic f) {
			return Currying.Complement(Delegate(f));
		}
	}
}