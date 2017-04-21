﻿// <autogenerated>
//   This file was generated by T4 code generator Find.tt.
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
		public static dynamic Find<TSource>(Func<TSource, bool> fn, IList<TSource> list) {
			return Currying.Find(Delegate(fn), list);
		}

		public static dynamic Find<TSource>(RamdaPlaceholder fn, IList<TSource> list) {
			return Currying.Find(fn, list);
		}

		public static dynamic Find<TSource>(Func<TSource, bool> fn, RamdaPlaceholder list = null) {
			return Currying.Find(Delegate(fn), list);
		}

		public static dynamic Find(dynamic fn, RamdaPlaceholder list = null) {
			return Currying.Find(Delegate(fn), list);
		}

		public static dynamic Find<TSource>(dynamic fn, IList<TSource> list) {
			return Currying.Find(Delegate(fn), list);
		}

		public static dynamic Find(RamdaPlaceholder fn = null, RamdaPlaceholder list = null) {
			return Currying.Find(fn, list);
		}
	}
}