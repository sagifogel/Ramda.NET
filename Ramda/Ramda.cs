using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class R
    {
        public readonly static Nothing Null = new Nothing();
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

        public static dynamic F = Currying.Delegate(() => Currying.F());

        public static dynamic T = Currying.Delegate(() => Currying.T());

        public static dynamic Filter<TSource>(Func<TSource, bool> pred, object filterable) {
            return Currying.Filter(pred, filterable);
        }

        public static dynamic Filter<TSource>(dynamic pred, object filterable) {
            return Currying.Filter(pred, filterable);
        }

        public static dynamic Head(string list) {
            return Currying.Head(list.ToCharArray());
        }

        public static dynamic Init(string list) {
            return Currying.Init(list.ToCharArray());
        }

        public static dynamic Last(string list) {
            return Currying.Last(list.ToCharArray());
        }

        public static dynamic TakeLast(int n, string list) {
            return Currying.TakeLast(n, list.ToCharArray());
        }

        public static dynamic TakeLast(RamdaPlaceholder n, string list) {
            return Currying.TakeLast(list.ToCharArray());
        }

        public static dynamic AllPass(IList<dynamic> preds) {
            return Currying.AllPass(preds);
        }

        public static dynamic AnyPass(IList<dynamic> preds) {
            return Currying.AnyPass(preds);
        }

        public static dynamic Ap(dynamic fns, dynamic vs) {
            return Currying.Ap(fns, vs);
        }

        public static dynamic Ap(dynamic fns, RamdaPlaceholder vs = null) {
            return Currying.Ap(fns, vs);
        }

        public static dynamic Ap(RamdaPlaceholder fns, dynamic vs) {
            return Currying.Ap(fns, vs);
        }

        public static dynamic Ap<TSource>(dynamic fns, dynamic vs) {
            return Currying.Ap(fns, vs);
        }

        public static dynamic Cond(IList<dynamic> pairs) {
            return Currying.Cond(pairs);
        }

        public static dynamic ConstructN<TTarget>(int n) {
            return Currying.ConstructN(n, typeof(TTarget));
        }

        public static dynamic ConstructN<TTarget>(RamdaPlaceholder n = null) {
            return Currying.ConstructN(n, typeof(TTarget));
        }

        public static dynamic Converge(Delegate after, IList<dynamic> functions) {
            return Currying.Converge(after, functions);
        }

        public static dynamic Converge(RamdaPlaceholder after, IList<dynamic> functions) {
            return Currying.Converge(after, functions);
        }

        public static dynamic Juxt(IList<dynamic> fns) {
            return Currying.Juxt(fns);
        }

        public static dynamic UseWith<TSource>(Delegate fn, IList<dynamic> transformers) {
            return Currying.UseWith(fn, transformers);
        }

        public static dynamic UseWith<TSource>(RamdaPlaceholder fn, IList<dynamic> transformers) {
            return Currying.UseWith(fn, transformers);
        }

        public static dynamic Pipe(IList<dynamic> functions) {
            return Currying.Pipe(functions);
        }

        public static dynamic Pipe(params dynamic[] functions) {
            return Currying.Pipe(functions);
        }

        public static dynamic PipeP(IList<dynamic> functions) {
            return Currying.PipeP(functions);
        }

        public static dynamic PipeP(params dynamic[] functions) {
            return Currying.PipeP(functions);
        }
    }
}