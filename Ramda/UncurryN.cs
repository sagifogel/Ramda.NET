﻿// <autogenerated>
//   This file was generated by T4 code generator UncurryN.tt.
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
		public static dynamic UncurryN(int length, Delegate fn) {
			return Currying.UncurryN(length, Delegate(fn));
		}

		public static dynamic UncurryN(RamdaPlaceholder length, Delegate fn) {
			return Currying.UncurryN(length, Delegate(fn));
		}

		public static dynamic UncurryN(int length, RamdaPlaceholder fn = null) {
			return Currying.UncurryN(length, fn);
		}

		public static dynamic UncurryN(RamdaPlaceholder length, dynamic fn) {
			return Currying.UncurryN(length, Delegate(fn));
		}

		public static dynamic UncurryN(int length, dynamic fn) {
			return Currying.UncurryN(length, Delegate(fn));
		}

		public static dynamic UncurryN(RamdaPlaceholder length = null, RamdaPlaceholder fn = null) {
			return Currying.UncurryN(length, fn);
		}
	}
}