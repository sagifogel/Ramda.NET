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
        //public static dynamic PropSatisfies<TTarget>(dynamic pred, int p, TTarget obj) where TTarget : IList {
        //    return Currying.PropSatisfies(pred, p, obj);
        //}

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
        //public static dynamic PropSatisfies<TArg, TTarget>(Func<TArg, bool> pred, int p, TTarget obj) where TTarget : IList {
        //    return Currying.PropSatisfies(pred, p, obj);
        //}

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
        //public static dynamic PropSatisfies<TArg, TTarget>(RamdaPlaceholder pred, int p, TTarget obj) {
        //    return Currying.PropSatisfies(pred, p, obj);
        //}

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
        //public static dynamic PropSatisfies(dynamic pred, int p, RamdaPlaceholder obj = null) {
        //    return Currying.PropSatisfies(pred, p, obj);
        //}

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
        //public static dynamic PropSatisfies<TArg, TTarget>(Func<TArg, bool> pred, int p, RamdaPlaceholder obj = null) where TTarget : IList {
        //    return Currying.PropSatisfies(Delegate(pred), p, obj);
        //}

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

        /// <summary>
		/// A function which calls the two provided functions and returns the `&&`of the results.It returns the result of the first function if it is false-y and the resultof the second function otherwise. Note that this is short-circuited,meaning that the second function will not be invoked if the first returns afalse-y value.In addition to functions, `R.both` also accepts any fantasy-land compatibleapplicative functor.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> Boolean) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">A predicate</param>
		/// <param name="g">Another predicate</param>
		/// <returns>a function that applies its arguments to `f` and `g` and `&&`s their outputs together.</returns>
		/// <see cref="R.And"/>
        public static dynamic Both(dynamic f, dynamic g) {
            return Currying.Both(f, g);
        }

        /// <summary>
		/// A function wrapping calls to the two functions in an `||` operation,returning the result of the first function if it is truth-y and the resultof the second function otherwise. Note that this is short-circuited,meaning that the second function will not be invoked if the first returns atruth-y value.In addition to functions, `R.either` also accepts any fantasy-land compatibleapplicative functor.
		/// <para />
		/// sig: (*... -> Boolean) -> (*... -> Boolean) -> (*... -> Boolean)
		/// </summary>
		/// <param name="f">a predicate</param>
		/// <param name="g">another predicate</param>
		/// <returns>a function that applies its arguments to `f` and `g` and `||`s their outputs together.</returns>
		/// <see cref="R.Or"/>
        public static dynamic Either(dynamic f, dynamic g) {
            return Currying.Either(f, g);
        }

        /// <summary>
		/// Returns a new list by pulling every item out of it (and all its sub-arrays)and putting them in a new array, depth-first.
		/// <para />
		/// sig: [a] -> [b]
		/// </summary>
		/// <param name="list">The array to consider.</param>
		/// <returns>The flattened list.</returns>
		/// <see cref="R.Unnest"/>
        public static dynamic Flatten(IDictionary list) {
            return Currying.Flatten(list);
        }

        /// <summary>
		/// Returns a new list by pulling every item out of it (and all its sub-arrays)and putting them in a new array, depth-first.
		/// <para />
		/// sig: [a] -> [b]
		/// </summary>
		/// <param name="list">The array to consider.</param>
		/// <returns>The flattened list.</returns>
		/// <see cref="R.Unnest"/>
        public static dynamic Flatten(ExpandoObject list) {
            return Currying.Flatten(list);
        }

        /// <summary>
		/// Takes a list and returns a list of lists where each sublist's elements areall "equal" according to the provided equality function.
		/// <para />
		/// sig: ((a, a) → Boolean) → [a] → [[a]]
		/// </summary>
		/// <param name="fn">Function for determining whether two given (adjacent) elements should be in the same group</param>
		/// <param name="list">The array to group. Also accepts a string, which will be treated as a list of characters.</param>
		/// <returns>A list that contains sublists of equal elements, whose concatenations are equal to the original list.</returns>
        public static dynamic GroupWith(dynamic fn, string list) {
            return Currying.GroupWith(fn, list);
        }

        /// <summary>
		/// Inserts the supplied element into the list, at index `index`. _Note thatthis is not destructive_: it returns a copy of the list with the changes.<small>No lists have been harmed in the application of this function.</small>
		/// <para />
		/// sig: Number -> a -> [a] -> [a]
		/// </summary>
		/// <param name="index">The position to insert the element</param>
		/// <param name="elt">The element to insert into the Array</param>
		/// <param name="list">The list to insert into</param>
		/// <returns>A new Array with `elt` inserted at `index`.</returns>
        public static dynamic Insert<TSource, TTarget>(int index, TTarget elt, IList<TSource> list) {
            return Currying.Insert(index, elt, list);
        }

        /// <summary>
		/// Returns the result of "setting" the portion of the given data structurefocused by the given lens to the result of applying the given function tothe focused value.
		/// <para />
		/// sig: Lens s a -> (a -> a) -> s -> s
		/// </summary>
		/// <param name="lens">first</param>
		/// <param name="v">second</param>
		/// <param name="x">third</param>
		/// <returns>*</returns>
		/// <see cref="R.Prop"/>
		/// <see cref="R.LensIndex"/>
		/// <see cref="R.LensProp"/>
        public static dynamic Over<TTarget>(dynamic lens, dynamic v, TTarget x) {
            return Currying.Over(Delegate(lens), Delegate(v), x);
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