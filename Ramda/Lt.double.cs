﻿// <autogenerated>
//   This file was generated by T4 code generator Lt.double.tt.
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
		public static dynamic Lt(double a, double b) {
			return Currying.Lt(a, b);
		}
		
		public static dynamic Lt(RamdaPlaceholder a, double b) {
			return Currying.Lt(a, b); 
		}
		
		public static dynamic Lt(double a, RamdaPlaceholder b = null) {
			return Currying.Lt(a, b); 
		}
		
		public static dynamic Lt(RamdaPlaceholder a = null, RamdaPlaceholder b = null) {
			return Currying.Lt(a, b);
		}
	}
}