﻿// <autogenerated>
//   This file was generated by T4 code generator Construct.3.tt.
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
		/// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type.
		/// <para />
		/// sig: (* -> {*}) -> (* -> {*})
		/// </summary>
		/// <param name="fn">The constructor function to wrap.</param>
		/// <returns>A wrapped, curried constructor function.</returns>
		public static dynamic Construct<TArg1, TArg2, TArg3, TTarget>(Func<TArg1, TArg2, TArg3, TTarget> fn) {
			return Currying.Construct(fn);
		}
	}
}