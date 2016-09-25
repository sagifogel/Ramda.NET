﻿// <autogenerated>
//   This file was generated by T4 code generator Scan.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ramda.NET
{
	public static partial class R
	{	
		public static dynamic Scan<TAccmulator, TSource>(dynamic fn, TAccmulator acc, IEnumerable<TSource> list) {
			return Currying.Scan(fn, acc, list);
		}
			
		public static dynamic Scan<TSource, TAccmulator, TResult>(Func<TAccmulator, TSource, TResult> fn, TAccmulator acc, IEnumerable<TSource> list) {
			return Currying.Scan(fn, acc, list);
		}
		
		public static dynamic Scan(dynamic fn, RamdaPlaceholder acc = null, RamdaPlaceholder list = null) {
			return Currying.Scan(fn, acc, list);
		}
		
		public static dynamic Scan<TSource, TAccmulator, TResult>(RamdaPlaceholder fn, TAccmulator acc, IEnumerable<TSource> list) {
			return Currying.Scan(fn, acc, list); 
		}

		public static dynamic Scan<TSource>(dynamic fn, RamdaPlaceholder acc, IEnumerable<TSource> list) {
			return Currying.Scan(fn, acc, list);
		}
				
		
		public static dynamic Scan<TSource, TAccmulator, TResult>(Func<TAccmulator, TSource, TResult> fn, RamdaPlaceholder acc, IEnumerable<TSource> list) {
			return Currying.Scan(fn, acc, list); 
		}

		public static dynamic Scan<TAccmulator>(dynamic fn, TAccmulator acc, RamdaPlaceholder list = null) {
			return Currying.Scan(fn, acc, list);
		}
				
		
		public static dynamic Scan<TSource, TAccmulator, TResult>(Func<TAccmulator, TSource, TResult> fn, TAccmulator acc, RamdaPlaceholder list = null) {
			return Currying.Scan(fn, acc, list); 
		}
		
		
		public static dynamic Scan<TSource, TAccmulator, TResult>(Func<TAccmulator, TSource, TResult> fn, RamdaPlaceholder acc = null, RamdaPlaceholder list = null) {
			return Currying.Scan(fn, acc, list);
		}
		
		public static dynamic Scan<TSource, TAccmulator, TResult>(RamdaPlaceholder fn = null, RamdaPlaceholder acc = null, RamdaPlaceholder list = null) {
			return Currying.Scan(fn, acc, list);
		}
	}
}