﻿// <autogenerated>
//   This file was generated by T4 code generator PickBy.tt.
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
		public static dynamic PickBy<TTarget>(Func<object, string, TTarget, bool> pred, TTarget obj) {
			return Currying.PickBy(pred, obj);
		}
		
		public static dynamic PickBy<TTarget>(RamdaPlaceholder pred, TTarget obj) {
			return Currying.PickBy(pred, obj); 
		}
		
		public static dynamic PickBy<TTarget>(Func<object, string, TTarget, bool> pred, RamdaPlaceholder obj = null) {
			return Currying.PickBy(pred, obj); 
		}
		
		public static dynamic PickBy<TTarget>(RamdaPlaceholder pred = null, RamdaPlaceholder obj = null) {
			return Currying.PickBy(pred, obj);
		}
	}
}