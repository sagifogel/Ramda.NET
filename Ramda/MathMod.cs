﻿// <autogenerated>
//   This file was generated by T4 code generator MathMod.tt.
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
		public static dynamic MathMod(int m, int p) {
			return Currying.MathMod(m, p);
		}
		
		public static dynamic MathMod(RamdaPlaceholder m, int p) {
			return Currying.MathMod(m, p); 
		}
		
		public static dynamic MathMod(int m, RamdaPlaceholder p = null) {
			return Currying.MathMod(m, p); 
		}
		
		public static dynamic MathMod(RamdaPlaceholder m = null, RamdaPlaceholder p = null) {
			return Currying.MathMod(m, p);
		}
	}
}