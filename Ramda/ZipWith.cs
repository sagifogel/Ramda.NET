﻿// <autogenerated>
//   This file was generated by T4 code generator ZipWith.tt.
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
		public static dynamic ZipWith<TSource1, TSource2, TReturn>(Func<TSource1, TSource2, TReturn> fn, IList<TSource1> list1, IList<TSource2> list2) {
			return Currying.ZipWith(Delegate(fn), list1, list2);
		}

		public static dynamic ZipWith<TSource1, TSource2>(RamdaPlaceholder fn, IList<TSource1> list1, IList<TSource2> list2) {
			return Currying.ZipWith(fn, list1, list2);
		}

		public static dynamic ZipWith<TSource1, TSource2, TReturn>(Func<TSource1, TSource2, TReturn> fn, RamdaPlaceholder list1, IList<TSource2> list2) {
			return Currying.ZipWith(Delegate(fn), list1, list2);
		}

		public static dynamic ZipWith<TSource1, TSource2, TReturn>(Func<TSource1, TSource2, TReturn> fn, IList<TSource1> list1, RamdaPlaceholder list2 = null) {
			return Currying.ZipWith(Delegate(fn), list1, list2);
		}

		public static dynamic ZipWith<TSource1, TSource2, TReturn>(Func<TSource1, TSource2, TReturn> fn, RamdaPlaceholder list1 = null, RamdaPlaceholder list2 = null) {
			return Currying.ZipWith(Delegate(fn), list1, list2);
		}

		public static dynamic ZipWith<TSource2>(dynamic fn, RamdaPlaceholder list1, IList<TSource2> list2) {
			return Currying.ZipWith(Delegate(fn), list1, list2);
		}

		public static dynamic ZipWith<TSource1>(dynamic fn, IList<TSource1> list1, RamdaPlaceholder list2 = null) {
			return Currying.ZipWith(Delegate(fn), list1, list2);
		}

		public static dynamic ZipWith(dynamic fn, RamdaPlaceholder list1 = null, RamdaPlaceholder list2 = null) {
			return Currying.ZipWith(Delegate(fn), list1, list2);
		}

		public static dynamic ZipWith<TSource1, TSource2>(dynamic fn, IList<TSource1> list1, IList<TSource2> list2) {
			return Currying.ZipWith(Delegate(fn), list1, list2);
		}

		public static dynamic ZipWith(RamdaPlaceholder fn = null, RamdaPlaceholder list1 = null, RamdaPlaceholder list2 = null) {
			return Currying.ZipWith(fn, list1, list2);
		}
	}
}