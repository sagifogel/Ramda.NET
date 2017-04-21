﻿// <autogenerated>
//   This file was generated by T4 code generator Either.func.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

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
		public static dynamic Either<TSource>(Func<TSource, bool> f, Func<TSource, bool> g) {
			return Currying.Either(Delegate(f), Delegate(g));
		}

		public static dynamic Either<TSource>(RamdaPlaceholder f, Func<TSource, bool> g) {
			return Currying.Either(f, Delegate(g));
		}

		public static dynamic Either<TSource>(Func<TSource, bool> f, RamdaPlaceholder g = null) {
			return Currying.Either(Delegate(f), g);
		}

		public static dynamic Either(RamdaPlaceholder f, dynamic g) {
			return Currying.Either(f, Delegate(g));
		}

		public static dynamic Either(dynamic f, RamdaPlaceholder g = null) {
			return Currying.Either(Delegate(f), g);
		}

		public static dynamic Either<TSource>(dynamic f, Func<TSource, bool> g) {
			return Currying.Either(Delegate(f), Delegate(g));
		}

		public static dynamic Either<TSource>(Func<TSource, bool> f, dynamic g) {
			return Currying.Either(Delegate(f), Delegate(g));
		}

		public static dynamic Either(RamdaPlaceholder f = null, RamdaPlaceholder g = null) {
			return Currying.Either(f, g);
		}
	}
}