﻿// <autogenerated>
//   This file was generated by T4 code generator Unapply.tt.
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
		public static dynamic Unapply(Delegate fn) {
			return Currying.Unapply(fn);
		}

		public static dynamic Unapply(RamdaPlaceholder fn = null) {
			return Currying.Unapply(fn);
		}

		public static dynamic Unapply(dynamic fn) {
			return Currying.Unapply(fn);
		}
	}
}