﻿// <autogenerated>
//   This file was generated by T4 code generator Has.tt.
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
		public static dynamic Has<TTarget>(string prop, TTarget obj) {
			return Currying.Has(prop, obj);
		}
		
		public static dynamic Has<TTarget>(RamdaPlaceholder prop, TTarget obj) {
			return Currying.Has(prop, obj); 
		}
		
		public static dynamic Has<TTarget>(string prop, RamdaPlaceholder obj = null) {
			return Currying.Has(prop, obj); 
		}
		
		public static dynamic Has<TTarget>(RamdaPlaceholder prop = null, RamdaPlaceholder obj = null) {
			return Currying.Has(prop, obj);
		}
	}
}