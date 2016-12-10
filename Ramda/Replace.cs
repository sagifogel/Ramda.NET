﻿// <autogenerated>
//   This file was generated by T4 code generator Replace.tt.
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
		public static dynamic Replace(Regex pattern, string replacement, string str) {
			return Currying.Replace(pattern, replacement, str);
		}

		public static dynamic Replace(RamdaPlaceholder pattern, string replacement, string str) {
			return Currying.Replace(pattern, replacement, str);
		}

		public static dynamic Replace(Regex pattern, RamdaPlaceholder replacement, string str) {
			return Currying.Replace(pattern, replacement, str);
		}

		public static dynamic Replace(Regex pattern, string replacement, RamdaPlaceholder str = null) {
			return Currying.Replace(pattern, replacement, str);
		}

		public static dynamic Replace(Regex pattern, RamdaPlaceholder replacement = null, RamdaPlaceholder str = null) {
			return Currying.Replace(pattern, replacement, str);
		}

		public static dynamic Replace(RamdaPlaceholder pattern = null, RamdaPlaceholder replacement = null, RamdaPlaceholder str = null) {
			return Currying.Replace(pattern, replacement, str);
		}
	}
}