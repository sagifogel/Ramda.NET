﻿// <autogenerated>
//   This file was generated by T4 code generator InsertAll.tt.
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
		public static dynamic InsertAll<TValue>(int index, IList<TValue> elts, IList<TValue> list) {
			return Currying.InsertAll(index, elts, list);
		}
		
		public static dynamic InsertAll<TValue>(RamdaPlaceholder index, IList<TValue> elts, IList<TValue> list) {
			return Currying.InsertAll(index, elts, list); 
		}
		
		public static dynamic InsertAll<TValue>(int index, RamdaPlaceholder elts, IList<TValue> list) {
			return Currying.InsertAll(index, elts, list); 
		}
		
		public static dynamic InsertAll<TValue>(int index, IList<TValue> elts, RamdaPlaceholder list = null) {
			return Currying.InsertAll(index, elts, list); 
		}
		
		
		public static dynamic InsertAll<TValue>(int index, RamdaPlaceholder elts = null, RamdaPlaceholder list = null) {
			return Currying.InsertAll(index, elts, list);
		}
		
		public static dynamic InsertAll<TValue>(RamdaPlaceholder index = null, RamdaPlaceholder elts = null, RamdaPlaceholder list = null) {
			return Currying.InsertAll(index, elts, list);
		}
	}
}