﻿// <autogenerated>
//   This file was generated by T4 code generator MaxBy.tt.
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
		public static dynamic MaxBy(Func<dynamic, dynamic> f, dynamic a, dynamic b) {
			return Currying.MaxBy(f, a, b);
		}
		
		public static dynamic MaxBy(RamdaPlaceholder f, dynamic a, dynamic b) {
			return Currying.MaxBy(f, a, b); 
		}
		
		public static dynamic MaxBy(Func<dynamic, dynamic> f, RamdaPlaceholder a, dynamic b) {
			return Currying.MaxBy(f, a, b); 
		}
		
		public static dynamic MaxBy(Func<dynamic, dynamic> f, dynamic a, RamdaPlaceholder b = null) {
			return Currying.MaxBy(f, a, b); 
		}
		
		
		public static dynamic MaxBy(Func<dynamic, dynamic> f, RamdaPlaceholder a = null, RamdaPlaceholder b = null) {
			return Currying.MaxBy(f, a, b);
		}
		
		public static dynamic MaxBy(RamdaPlaceholder f = null, RamdaPlaceholder a = null, RamdaPlaceholder b = null) {
			return Currying.MaxBy(f, a, b);
		}
	}
}