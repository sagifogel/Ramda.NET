﻿// <autogenerated>
//   This file was generated by T4 code generator Ap.fn.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Dynamic;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ramda.NET
{
	public static partial class R
	{	
		public static dynamic Ap<TSource>(Func<TSource, TSource> fns, Func<TSource, TSource> vs) {
			return Currying.Ap(fns, vs);
		}

		public static dynamic Ap<TSource>(RamdaPlaceholder fns, Func<TSource, TSource> vs) {
			return Currying.Ap(fns, vs);
		}

		public static dynamic Ap<TSource>(Func<TSource, TSource> fns, RamdaPlaceholder vs = null) {
			return Currying.Ap(fns, vs);
		}
	}
}