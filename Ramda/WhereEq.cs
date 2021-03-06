﻿// <autogenerated>
//   This file was generated by T4 code generator WhereEq.tt.
//   Any changes made to this file manually will be lost next time the file is regenerated.
// </autogenerated>

using System;
using System.Linq;
using System.Dynamic;
using System.Collections;
using System.Threading.Tasks;
using static Ramda.NET.Currying;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Ramda.NET
{
	public static partial class R
	{	
		/// <summary>
		/// Takes a spec object and a test object; returns true if the test satisfiesthe spec, false otherwise. An object satisfies the spec if, for each of thespec's own properties, accessing that property of the object gives the samevalue (in `R.equals` terms) as accessing that property of the spec.`whereEq` is a specialization of [`where`](#where).
		/// <para />
		/// sig: {String: *} -> {String: *} -> Boolean
		/// </summary>
		/// <param name="spec">first</param>
		/// <param name="testObj">second</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.Where"/>
		public static dynamic WhereEq<TSpec, TTArget>(TSpec spec, TTArget testObj) {
			return Currying.WhereEq(spec, testObj);
		}

		/// <summary>
		/// Takes a spec object and a test object; returns true if the test satisfiesthe spec, false otherwise. An object satisfies the spec if, for each of thespec's own properties, accessing that property of the object gives the samevalue (in `R.equals` terms) as accessing that property of the spec.`whereEq` is a specialization of [`where`](#where).
		/// <para />
		/// sig: {String: *} -> {String: *} -> Boolean
		/// </summary>
		/// <param name="spec">first</param>
		/// <param name="testObj">second</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.Where"/>
		public static dynamic WhereEq<TTArget>(RamdaPlaceholder spec, TTArget testObj) {
			return Currying.WhereEq(spec, testObj);
		}

		/// <summary>
		/// Takes a spec object and a test object; returns true if the test satisfiesthe spec, false otherwise. An object satisfies the spec if, for each of thespec's own properties, accessing that property of the object gives the samevalue (in `R.equals` terms) as accessing that property of the spec.`whereEq` is a specialization of [`where`](#where).
		/// <para />
		/// sig: {String: *} -> {String: *} -> Boolean
		/// </summary>
		/// <param name="spec">first</param>
		/// <param name="testObj">second</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.Where"/>
		public static dynamic WhereEq<TSpec>(TSpec spec, RamdaPlaceholder testObj = null) {
			return Currying.WhereEq(spec, testObj);
		}

		/// <summary>
		/// Takes a spec object and a test object; returns true if the test satisfiesthe spec, false otherwise. An object satisfies the spec if, for each of thespec's own properties, accessing that property of the object gives the samevalue (in `R.equals` terms) as accessing that property of the spec.`whereEq` is a specialization of [`where`](#where).
		/// <para />
		/// sig: {String: *} -> {String: *} -> Boolean
		/// </summary>
		/// <param name="spec">first</param>
		/// <param name="testObj">second</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.Where"/>
		public static dynamic WhereEq(RamdaPlaceholder spec = null, RamdaPlaceholder testObj = null) {
			return Currying.WhereEq(spec, testObj);
		}
	}
}