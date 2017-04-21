﻿// <autogenerated>
//   This file was generated by T4 code generator Into.IList.tt.
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
		public static dynamic Into<TSource, TAccumulator>(IList<TAccumulator> acc, Func<ITransformer, ITransformer> xf, IList<TSource> list) {
			return Currying.Into(acc, Delegate(xf), list);
		}

		public static dynamic Into<TSource>(RamdaPlaceholder acc, Func<ITransformer, ITransformer> xf, IList<TSource> list) {
			return Currying.Into(acc, Delegate(xf), list);
		}

		public static dynamic Into<TSource, TAccumulator>(IList<TAccumulator> acc, RamdaPlaceholder xf, IList<TSource> list) {
			return Currying.Into(acc, xf, list);
		}

		public static dynamic Into<TAccumulator>(IList<TAccumulator> acc, Func<ITransformer, ITransformer> xf, RamdaPlaceholder list = null) {
			return Currying.Into(acc, Delegate(xf), list);
		}

		public static dynamic Into<TAccumulator>(IList<TAccumulator> acc, RamdaPlaceholder xf = null, RamdaPlaceholder list = null) {
			return Currying.Into(acc, xf, list);
		}

		public static dynamic Into<TSource>(RamdaPlaceholder acc, dynamic xf, IList<TSource> list) {
			return Currying.Into(acc, Delegate(xf), list);
		}

		public static dynamic Into<TAccumulator>(IList<TAccumulator> acc, dynamic xf, RamdaPlaceholder list = null) {
			return Currying.Into(acc, Delegate(xf), list);
		}

		public static dynamic Into<TSource, TAccumulator>(IList<TAccumulator> acc, dynamic xf, IList<TSource> list) {
			return Currying.Into(acc, Delegate(xf), list);
		}

		public static dynamic Into(RamdaPlaceholder acc = null, RamdaPlaceholder xf = null, RamdaPlaceholder list = null) {
			return Currying.Into(acc, xf, list);
		}
	}
}