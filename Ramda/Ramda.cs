using System;
using System.Linq;
using System.Collections;
using static Ramda.NET.Lambda;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class R
    {
        public readonly static RamdaPlaceholder __ = new RamdaPlaceholder();

        public static dynamic Length<TValue>(IList list) {
            return Currying.Length(list);
        }

        public static dynamic CurryN(int length, Delegate fn) {
            return Currying.CurryN(length, fn);
        }

        public static dynamic Nth<TValue>(int offset, IList<TValue> list) {
            return Currying.Nth(offset, list);
        }

        public static dynamic Evolve<TTarget>(object transformations, TTarget obj) {
            return Currying.Evolve(transformations, obj);
        }

        public static dynamic Nth<TValue>(RamdaPlaceholder offset, IList<TValue> list) {
            return Currying.Nth(offset, list);
        }

        public static dynamic NthArg<TValue>(int offset, RamdaPlaceholder list = null) {
            return Currying.NthArg(offset, list);
        }

        public static dynamic FormPairs<TValue>(IEnumerable<KeyValuePair<string, TValue>> pairs) {
            return Currying.FromPairs(pairs.Select(pair => new object[] { pair.Key, pair.Value }).ToArray());
        }

        public static dynamic NthArg<TValue>(RamdaPlaceholder offset = null, RamdaPlaceholder list = null) {
            return Currying.NthArg(offset, list);
        }

        public static dynamic Prop<TTarget>(int p, TTarget obj) where TTarget : IList {
            return Currying.Prop(p, obj);
        }

        public static dynamic Prop<TTarget>(int p, RamdaPlaceholder obj = null) where TTarget : IList {
            return Currying.Prop(p, obj);
        }

        public static dynamic PropIs<TTarget>(Type type, int p, TTarget obj) where TTarget : IList {
            return Currying.PropIs(type, p, obj);
        }

        public static dynamic PropIs<TTarget>(RamdaPlaceholder type, int p, TTarget obj) where TTarget : IList {
            return Currying.PropIs(type, p, obj);
        }

        public static dynamic PropIs<TTarget>(Type type, int name, RamdaPlaceholder obj = null) where TTarget : IList {
            return Currying.PropIs(type, name, obj);
        }

        public static dynamic PropOr<TValue, TTarget>(TValue val, IList<int> p, TTarget obj) where TTarget : IList {
            return Currying.PropOr(val, p, obj);
        }

        public static dynamic PropOr<TValue, TTarget>(RamdaPlaceholder val, IList<int> p, TTarget obj) where TTarget : IList {
            return Currying.PropOr(val, p, obj);
        }

        public static dynamic PropOr<TValue, TTarget>(TValue val, IList<int> p, RamdaPlaceholder obj = null) where TTarget : IList {
            return Currying.PropOr(val, p, obj);
        }

        public static dynamic PropSatisfies<TTarget>(dynamic pred, int p, TTarget obj) where TTarget : IList {
            return Currying.PropSatisfies(pred, p, obj);
        }

        public static dynamic PropSatisfies<TArg, TTarget>(Func<TArg, bool> pred, int p, TTarget obj) where TTarget : IList {
            return Currying.PropSatisfies(pred, p, obj);
        }

        public static dynamic PropSatisfies<TArg, TTarget>(RamdaPlaceholder pred, int p, TTarget obj) {
            return Currying.PropSatisfies(pred, p, obj);
        }

        public static dynamic PropSatisfies(dynamic pred, int p, RamdaPlaceholder obj = null) {
            return Currying.PropSatisfies(pred, p, obj);
        }

        public static dynamic PropSatisfies<TArg, TTarget>(Func<TArg, bool> pred, int p, RamdaPlaceholder obj = null) where TTarget : IList {
            return Currying.PropSatisfies(pred, p, obj);
        }

        public static dynamic SplitEvery<TValue>(int n, string list) {
            return Currying.SplitEvery(n, list.ToCharArray());
        }

        public static dynamic SplitEvery<TValue>(RamdaPlaceholder n, string list) {
            return Currying.SplitEvery(n, list.ToCharArray());
        }

        public static dynamic SplitWhen<TValue>(dynamic pred, string list) {
            return Currying.SplitWhen(pred, list);
        }

        public static dynamic SplitWhen(Func<char, bool> pred, string list) {
            return Currying.SplitWhen(pred, list.ToCharArray());
        }

        public static dynamic Tail(string list) {
            return Currying.Tail(list.ToCharArray());
        }

        public static dynamic Take(int n, string list) {
            return Currying.Take(n, list.ToCharArray());
        }

        public static dynamic Take(RamdaPlaceholder n, string list) {
            return Currying.Take(n, list.ToCharArray());
        }

        public static dynamic F = new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
            return Currying.F();
        });

        public static dynamic T = new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
            return Currying.T();
        });
    }
}