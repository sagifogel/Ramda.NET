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
        /// Represents a null value. A Replacement for the null keyword.
        /// </summary>
        public readonly static Nothing @null = new Nothing();

        /// <summary>
        ///  A special placeholder value used to specify "gaps" within curried functions,
        ///  allowing partial application of any combination of arguments, regardless of
        ///  their positions.
        /// </summary>
        public readonly static RamdaPlaceholder __ = new RamdaPlaceholder();

        /// <summary>
		/// Makes a descending comparator function out of a function that returns a valuethat can be compared with `<` and `>`.
		/// <para />
		/// sig: Ord b => (a -> b) -> a -> a -> Number
		/// </summary>
		/// <param name="fn">A function of arity one that returns a value that can be compared</param>
		/// <param name="a">The first item to be compared.</param>
		/// <param name="b">The second item to be compared.</param>
		/// <returns>`-1` if fn(a) > fn(b), `1` if fn(b) > fn(a), otherwise `0`</returns>
        public static dynamic Descend<TSource>(IList<dynamic> functions, IList<TSource> list) {
            return Currying.Descend(functions, list);
        }

        /// <summary>
		/// Makes a descending comparator function out of a function that returns a valuethat can be compared with `<` and `>`.
		/// <para />
		/// sig: Ord b => (a -> b) -> a -> a -> Number
		/// </summary>
		/// <param name="fn">A function of arity one that returns a value that can be compared</param>
		/// <param name="a">The first item to be compared.</param>
		/// <param name="b">The second item to be compared.</param>
		/// <returns>`-1` if fn(a) > fn(b), `1` if fn(b) > fn(a), otherwise `0`</returns>
        public static dynamic Descend<TSource>(IList<dynamic> functions, RamdaPlaceholder list = null) {
            return Currying.Descend(functions, list);
        }

        /// <summary>
		/// Returns the number of elements in the array by returning `list.length`.
		/// <para />
		/// sig: [a] -> Number
		/// </summary>
		/// <param name="list">The array to inspect.</param>
		/// <returns>The length of the array.</returns>
        public static dynamic Length<TValue>(IList list) {
            return Currying.Length(list);
        }

        /// <summary>
		/// Returns the number of elements in the array by returning `list.length`.
		/// <para />
		/// sig: [a] -> Number
		/// </summary>
		/// <param name="list">The array to inspect.</param>
		/// <returns>The length of the array.</returns>
        public static dynamic Length(string list) {
            return Currying.Length(list);
        }
        /// <summary>
        /// Returns the number of elements in the array by returning `list.length`.
        /// <para />
        /// sig: [a] -> Number
        /// </summary>
        /// <param name="list">The array to inspect.</param>
        /// <returns>The length of the array.</returns>
        public static dynamic Length(Delegate list) {
            return Currying.Length(Delegate(list));
        }

        /// <summary>
		/// Returns the number of elements in the array by returning `list.length`.
		/// <para />
		/// sig: [a] -> Number
		/// </summary>
		/// <param name="list">The array to inspect.</param>
		/// <returns>The length of the array.</returns>
        public static dynamic Length(object list) {
            return Currying.Length(list);
        }

        /// <summary>
		/// Returns a lens for the given getter and setter functions. The getter "gets"the value of the focus; the setter "sets" the value of the focus. The settershould not mutate the data structure.
		/// <para />
		/// sig: (s -> a) -> ((a, s) -> s) -> Lens s a
		/// </summary>
		/// <param name="getter">first</param>
		/// <param name="setter">second</param>
		/// <returns>Lens</returns>
		/// <see cref="R.View"/>
		/// <see cref="R.Set"/>
		/// <see cref="R.Over"/>
		/// <see cref="R.LensIndex"/>
		/// <see cref="R.LensProp"/>
        public static dynamic Lens(dynamic getter, dynamic setter) {
            return Currying.Lens(Delegate(getter), Delegate(setter));
        }

        /// <summary>
        /// Creates a new object by recursively evolving a shallow copy of `object`,according to the `transformation` functions. All non-primitive propertiesare copied by reference.A `transformation` function will not be invoked if its corresponding keydoes not exist in the evolved object.
        /// <para />
        /// sig: {k: (v -> v)} -> {k: v} -> {k: v}
        /// </summary>
        /// <param name="transformations">The object specifying transformation functions to apply       to the object.</param>
        /// <param name="object">The object to be transformed.</param>
        /// <returns>The transformed object.</returns>
        public static dynamic Evolve<TTarget>(object transformations, TTarget obj) {
            return Currying.Evolve(transformations, obj);
        }

        /// <summary>
		/// Creates a new object by recursively evolving a shallow copy of `object`,according to the `transformation` functions. All non-primitive propertiesare copied by reference.A `transformation` function will not be invoked if its corresponding keydoes not exist in the evolved object.
		/// <para />
		/// sig: {k: (v -> v)} -> {k: v} -> {k: v}
		/// </summary>
		/// <param name="transformations">The object specifying transformation functions to apply       to the object.</param>
		/// <param name="object">The object to be transformed.</param>
		/// <returns>The transformed object.</returns>
        public static dynamic Evolve(object transformations, RamdaPlaceholder obj = null) {
            return Currying.Evolve(transformations, obj);
        }

        /// <summary>
		/// Creates a new object from a list key-value pairs. If a key appears inmultiple pairs, the rightmost pair is included in the object.
		/// <para />
		/// sig: [[k,v]] -> {k: v}
		/// </summary>
		/// <param name="pairs">An array of two-element arrays that will be the keys and values of the output object.</param>
		/// <returns>The object made by pairing up `keys` and `values`.</returns>
		/// <see cref="R.ToPairs"/>
		/// <see cref="R.Pair"/>
        public static dynamic FormPairs<TValue>(IEnumerable<KeyValuePair<string, TValue>> pairs) {
            return Currying.FromPairs(pairs.Select(pair => new object[] { pair.Key, pair.Value }).ToArray());
        }

        /// <summary>
		/// Returns a function that when supplied an object returns the indicatedproperty of that object, if it exists.
		/// <para />
		/// sig: s -> {s: a} -> a | Undefined
		/// </summary>
		/// <param name="p">The property name</param>
		/// <param name="obj">The object to query</param>
		/// <returns>The value at `obj.p`.</returns>
		/// <see cref="R.Path"/>
        public static dynamic Prop<TTarget>(int p, TTarget obj) where TTarget : IList {
            return Currying.Prop(p, obj);
        }

        /// <summary>
		/// Returns a function that when supplied an object returns the indicatedproperty of that object, if it exists.
		/// <para />
		/// sig: s -> {s: a} -> a | Undefined
		/// </summary>
		/// <param name="p">The property name</param>
		/// <param name="obj">The object to query</param>
		/// <returns>The value at `obj.p`.</returns>
		/// <see cref="R.Path"/>
        public static dynamic Prop<TTarget>(int p, RamdaPlaceholder obj = null) where TTarget : IList {
            return Currying.Prop(p, obj);
        }

        /// <summary>
		/// Returns `true` if the specified object property is of the given type;`false` otherwise.
		/// <para />
		/// sig: Type -> String -> Object -> Boolean
		/// </summary>
		/// <param name="type">first</param>
		/// <param name="name">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.Is"/>
		/// <see cref="R.PropSatisfies"/>
        public static dynamic PropIs<TTarget>(Type type, int p, TTarget obj) where TTarget : IList {
            return Currying.PropIs(type, p, obj);
        }

        /// <summary>
		/// Returns `true` if the specified object property is of the given type;`false` otherwise.
		/// <para />
		/// sig: Type -> String -> Object -> Boolean
		/// </summary>
		/// <param name="type">first</param>
		/// <param name="name">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.Is"/>
		/// <see cref="R.PropSatisfies"/>
        public static dynamic PropIs<TTarget>(RamdaPlaceholder type, int p, TTarget obj) where TTarget : IList {
            return Currying.PropIs(type, p, obj);
        }

        /// <summary>
		/// Returns `true` if the specified object property is of the given type;`false` otherwise.
		/// <para />
		/// sig: Type -> String -> Object -> Boolean
		/// </summary>
		/// <param name="type">first</param>
		/// <param name="name">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.Is"/>
		/// <see cref="R.PropSatisfies"/>
        public static dynamic PropIs<TTarget>(Type type, int name, RamdaPlaceholder obj = null) where TTarget : IList {
            return Currying.PropIs(type, name, obj);
        }

        /// <summary>
		/// If the given, non-null object has an own property with the specified name,returns the value of that property. Otherwise returns the provided defaultvalue.
		/// <para />
		/// sig: a -> String -> Object -> a
		/// </summary>
		/// <param name="val">The default value.</param>
		/// <param name="p">The name of the property to return.</param>
		/// <param name="obj">The object to query.</param>
		/// <returns>The value of given property of the supplied object or the default value.</returns>
        public static dynamic PropOr<TValue, TTarget>(TValue val, IList<int> p, TTarget obj) where TTarget : IList {
            return Currying.PropOr(val, p, obj);
        }

        /// <summary>
		/// If the given, non-null object has an own property with the specified name,returns the value of that property. Otherwise returns the provided defaultvalue.
		/// <para />
		/// sig: a -> String -> Object -> a
		/// </summary>
		/// <param name="val">The default value.</param>
		/// <param name="p">The name of the property to return.</param>
		/// <param name="obj">The object to query.</param>
		/// <returns>The value of given property of the supplied object or the default value.</returns>
        public static dynamic PropOr<TValue, TTarget>(RamdaPlaceholder val, IList<int> p, TTarget obj) where TTarget : IList {
            return Currying.PropOr(val, p, obj);
        }

        /// <summary>
		/// If the given, non-null object has an own property with the specified name,returns the value of that property. Otherwise returns the provided defaultvalue.
		/// <para />
		/// sig: a -> String -> Object -> a
		/// </summary>
		/// <param name="val">The default value.</param>
		/// <param name="p">The name of the property to return.</param>
		/// <param name="obj">The object to query.</param>
		/// <returns>The value of given property of the supplied object or the default value.</returns>
        public static dynamic PropOr<TValue, TTarget>(TValue val, IList<int> p, RamdaPlaceholder obj = null) where TTarget : IList {
            return Currying.PropOr(val, p, obj);
        }

        /// <summary>
		/// Returns `true` if the specified object property satisfies the givenpredicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> String -> {String: a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="name">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropEq"/>
		/// <see cref="R.PropIs"/>
        public static dynamic PropSatisfies<TTarget>(dynamic pred, int p, TTarget obj) where TTarget : IList {
            return Currying.PropSatisfies(pred, p, obj);
        }

        /// <summary>
		/// Returns `true` if the specified object property satisfies the givenpredicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> String -> {String: a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="name">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropEq"/>
		/// <see cref="R.PropIs"/>
        public static dynamic PropSatisfies<TArg, TTarget>(Func<TArg, bool> pred, int p, TTarget obj) where TTarget : IList {
            return Currying.PropSatisfies(pred, p, obj);
        }

        /// <summary>
		/// Returns `true` if the specified object property satisfies the givenpredicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> String -> {String: a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="name">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropEq"/>
		/// <see cref="R.PropIs"/>
        public static dynamic PropSatisfies<TArg, TTarget>(RamdaPlaceholder pred, int p, TTarget obj) {
            return Currying.PropSatisfies(pred, p, obj);
        }

        /// <summary>
		/// Returns `true` if the specified object property satisfies the givenpredicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> String -> {String: a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="name">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropEq"/>
		/// <see cref="R.PropIs"/>
        public static dynamic PropSatisfies(dynamic pred, int p, RamdaPlaceholder obj = null) {
            return Currying.PropSatisfies(pred, p, obj);
        }

        /// <summary>
		/// Returns `true` if the specified object property satisfies the givenpredicate; `false` otherwise.
		/// <para />
		/// sig: (a -> Boolean) -> String -> {String: a} -> Boolean
		/// </summary>
		/// <param name="pred">first</param>
		/// <param name="name">second</param>
		/// <param name="obj">third</param>
		/// <returns>Boolean</returns>
		/// <see cref="R.PropEq"/>
		/// <see cref="R.PropIs"/>
        public static dynamic PropSatisfies<TArg, TTarget>(Func<TArg, bool> pred, int p, RamdaPlaceholder obj = null) where TTarget : IList {
            return Currying.PropSatisfies(Delegate(pred), p, obj);
        }

        /// <summary>
        /// A function that always returns `true`. Any passed in parameters are ignored.
        /// <para />
        /// sig: * -> Boolean
        /// </summary>
        /// <returns>Function</returns>
        public static dynamic F = Delegate(() => Currying.F());

        /// <summary>
		/// A function that always returns `false`. Any passed in parameters are ignored.
		/// <para />
		/// sig: * -> Boolean
		/// </summary>
		/// <returns>Function</returns>
        public static dynamic T = Delegate(() => Currying.T());

        /// <summary>
		/// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type. The arity of the functionreturned is specified to allow using variadic constructor functions.
		/// <para />
		/// sig: Number -> (* -> {*}) -> (* -> {*})
		/// </summary>
		/// <param name="n">The arity of the constructor function.</param>
		/// <param name="Fn">The constructor function to wrap.</param>
		/// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic ConstructN<TTarget>(int n) {
            return Currying.ConstructN(n, typeof(TTarget));
        }

        /// <summary>
		/// Wraps a constructor function inside a curried function that can be calledwith the same arguments and returns the same type. The arity of the functionreturned is specified to allow using variadic constructor functions.
		/// <para />
		/// sig: Number -> (* -> {*}) -> (* -> {*})
		/// </summary>
		/// <param name="n">The arity of the constructor function.</param>
		/// <param name="Fn">The constructor function to wrap.</param>
		/// <returns>A wrapped, curried constructor function.</returns>
        public static dynamic ConstructN<TTarget>(RamdaPlaceholder n = null) {
            return Currying.ConstructN(n, typeof(TTarget));
        }

        public static dynamic Compose(params dynamic[] functions) {
            return Currying.Compose(functions);
        }

        public static dynamic ComposeP(params dynamic[] functions) {
            return Currying.ComposeP(functions);
        }

        public static dynamic ComposeK(params dynamic[] functions) {
            return Currying.ComposeK(functions);
        }

        public static dynamic Both(dynamic f, dynamic g) {
            return Currying.Both(f, g);
        }

        public static dynamic Either(dynamic f, dynamic g) {
            return Currying.Either(f, g);
        }

        public static dynamic Flatten(IDictionary list) {
            return Currying.Flatten(list);
        }

        public static dynamic Flatten(ExpandoObject list) {
            return Currying.Flatten(list);
        }

        public static dynamic ForEach<TSource>(Action<TSource> fn, object list) {
            return Currying.ForEach(Delegate(fn), list);
        }

        public static dynamic ForEach(RamdaPlaceholder fn, object list) {
            return Currying.ForEach(Delegate(fn), list);
        }

        public static dynamic GroupWith(dynamic fn, string list) {
            return Currying.GroupWith(fn, list);
        }

        public static dynamic Insert<TSource, TTarget>(int index, TTarget elt, IList<TSource> list) {
            return Currying.Insert(index, elt, list);
        }

        public static dynamic Intersperse<TResult, TSeperator>(TSeperator separator, IDispersible<TSeperator, TResult> list) {
            return Currying.Intersperse(separator, list);
        }

        public static dynamic Intersperse<TResult, TSeperator>(RamdaPlaceholder separator, IDispersible<TSeperator, TResult> list) {
            return Currying.Intersperse(separator, list);
        }

        public static dynamic Over<TTarget>(dynamic lens, dynamic v, TTarget x) {
            return Currying.Over(Delegate(lens), Delegate(v), x);
        }

        public static dynamic Prepend(object el, IList list) {
            return Currying.Prepend(el, list);
        }

        public static dynamic Replace(string pattern, string replacement, string str) {
            return Currying.Replace(new Regex(pattern), replacement, str);
        }
        
        public static dynamic Replace(string pattern, RamdaPlaceholder replacement, string str) {
            return Currying.Replace(new Regex(pattern), replacement, str);
        }

        public static dynamic Replace(string pattern, string replacement, RamdaPlaceholder str = null) {
            return Currying.Replace(new Regex(pattern), replacement, str);
        }

        public static dynamic Replace(string pattern, RamdaPlaceholder replacement = null, RamdaPlaceholder str = null) {
            return Currying.Replace(new Regex(pattern), replacement, str);
        }

        public static dynamic Slice(int fromIndex, int toIndex, string list) {
			return Currying.Slice(fromIndex, toIndex, list);
		}

		public static dynamic Slice(RamdaPlaceholder fromIndex, int toIndex, string list) {
			return Currying.Slice(fromIndex, toIndex, list);
		}

		public static dynamic Slice(int fromIndex, RamdaPlaceholder toIndex, string list) {
			return Currying.Slice(fromIndex, toIndex, list);
		}

        public static dynamic SortWith<TSource>(IList<dynamic> functions, IList<TSource> list) {
            return Currying.SortWith(functions, list);
        }

        public static dynamic SortWith<TSource>(IList<dynamic> functions, RamdaPlaceholder list = null) {
            return Currying.SortWith(functions, list);
        }

        public static dynamic Match(string rx, string str) {
            return Currying.Match(new Regex(rx), str);
        }

        public static dynamic Match(string rx, RamdaPlaceholder str) {
            return Currying.Match(new Regex(rx), str);
        }

        public static dynamic Test(string pattern, string str) {
            return Currying.Test(new Regex(pattern), str);
        }

        public static dynamic Test(string pattern, RamdaPlaceholder str) {
            return Currying.Test(new Regex(pattern), str);
        }

        public static dynamic Transduce<TSource, TAccumulator>(dynamic xf, dynamic fn, TAccumulator acc, IList<TSource> list) {
            return Currying.Transduce(xf, fn, acc, list);
        }

        public static dynamic Until<TArg>(dynamic pred, dynamic fn, TArg init) {
            return Currying.Until(Delegate(pred), Delegate(fn), init);
        }
    }
}