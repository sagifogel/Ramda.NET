﻿// <autogenerated>
//   This file was generated by T4 code generator MapAccumRight.tt.
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
		public static dynamic MapAccumRight(Func<dynamic, dynamic, Tuple<dynamic, dynamic>> fn, dynamic acc, IList<dynamic> list) {
			return Currying.MapAccumRight(fn, acc, list);
		}
		
		public static dynamic MapAccumRight(RamdaPlaceholder fn, dynamic acc, IList<dynamic> list) {
			return Currying.MapAccumRight(fn, acc, list); 
		}
		
		public static dynamic MapAccumRight(Func<dynamic, dynamic, Tuple<dynamic, dynamic>> fn, RamdaPlaceholder acc, IList<dynamic> list) {
			return Currying.MapAccumRight(fn, acc, list); 
		}
		
		public static dynamic MapAccumRight(Func<dynamic, dynamic, Tuple<dynamic, dynamic>> fn, dynamic acc, RamdaPlaceholder list = null) {
			return Currying.MapAccumRight(fn, acc, list); 
		}
		
		
		public static dynamic MapAccumRight(Func<dynamic, dynamic, Tuple<dynamic, dynamic>> fn, RamdaPlaceholder acc = null, RamdaPlaceholder list = null) {
			return Currying.MapAccumRight(fn, acc, list);
		}
		
		public static dynamic MapAccumRight(RamdaPlaceholder fn = null, RamdaPlaceholder acc = null, RamdaPlaceholder list = null) {
			return Currying.MapAccumRight(fn, acc, list);
		}
	}
}