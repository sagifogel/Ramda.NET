﻿// <autogenerated>
//   This file was generated by T4 code generator Divide.double.tt.
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
		public static dynamic Divide(double a, double b) {
			return Currying.Divide(a, b);
		}
		
		public static dynamic Divide(RamdaPlaceholder a, double b) {
			return Currying.Divide(a, b); 
		}
		
		public static dynamic Divide(double a, RamdaPlaceholder b = null) {
			return Currying.Divide(a, b); 
		}
		
		public static dynamic Divide(RamdaPlaceholder a = null, RamdaPlaceholder b = null) {
			return Currying.Divide(a, b);
		}
	}
}