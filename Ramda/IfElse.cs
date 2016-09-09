﻿// <autogenerated>
//   This file was generated by T4 code generator IfElse.tt.
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
		public static dynamic IfElse(Delegate condition, Delegate onTrue, Delegate onFalse) {
			return Currying.IfElse(condition, onTrue, onFalse);
		}
		
		public static dynamic IfElse(RamdaPlaceholder condition, Delegate onTrue, Delegate onFalse) {
			return Currying.IfElse(condition, onTrue, onFalse); 
		}
		
		public static dynamic IfElse(Delegate condition, RamdaPlaceholder onTrue, Delegate onFalse) {
			return Currying.IfElse(condition, onTrue, onFalse); 
		}
		
		public static dynamic IfElse(Delegate condition, Delegate onTrue, RamdaPlaceholder onFalse = null) {
			return Currying.IfElse(condition, onTrue, onFalse); 
		}
		
		
		public static dynamic IfElse(Delegate condition, RamdaPlaceholder onTrue = null, RamdaPlaceholder onFalse = null) {
			return Currying.IfElse(condition, onTrue, onFalse);
		}
		
		public static dynamic IfElse(RamdaPlaceholder condition = null, RamdaPlaceholder onTrue = null, RamdaPlaceholder onFalse = null) {
			return Currying.IfElse(condition, onTrue, onFalse);
		}
	}
}