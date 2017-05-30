using System;
using System.Linq;
using System.Collections;
using static Ramda.NET.Currying;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace Ramda.NET
{
    public static partial class R
    {
        public readonly static Nothing @null = new Nothing();
        public readonly static RamdaPlaceholder __ = new RamdaPlaceholder();

        public static dynamic Length<TValue>(IList list) {
            return Currying.Length(list);
        }

        public static dynamic Length(string list) {
            return Currying.Length(list);
        }

        public static dynamic Length(Delegate list) {
            return Currying.Length(Delegate(list));
        }

        public static dynamic Length(object list) {
            return Currying.Length(list);
        }

        public static dynamic Lens(dynamic getter, dynamic setter) {
            return Currying.Lens(Delegate(getter), Delegate(setter));
        }

        public static dynamic CurryN(int length, Delegate fn) {
            return Currying.CurryN(length, Delegate(fn));
        }

        public static dynamic CurryN(int length, RamdaPlaceholder fn = null) {
            return Currying.CurryN(length, fn);
        }

        public static dynamic Nth<TValue>(int offset, IList<TValue> list) {
            return Currying.Nth(offset, list);
        }

        public static dynamic Evolve<TTarget>(object transformations, TTarget obj) {
            return Currying.Evolve(transformations, obj);
        }

        public static dynamic Evolve(object transformations, RamdaPlaceholder obj = null) {
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
            return Currying.PropSatisfies(Delegate(pred), p, obj);
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
            return Currying.SplitWhen(Delegate(pred), list.ToCharArray());
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

        public static dynamic F = Delegate(() => Currying.F());

        public static dynamic T = Delegate(() => Currying.T());

        public static dynamic Filter(dynamic pred, object filterable) {
            return Currying.Filter(Delegate(pred), filterable);
        }

        public static dynamic Head(string list) {
            return Currying.Head(list);
        }

        public static dynamic Last(string list) {
            return Currying.Last(list);
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
            return Currying.Converge(Delegate(after), functions);
        }

        public static dynamic Converge(RamdaPlaceholder after, IList<dynamic> functions) {
            return Currying.Converge(after, functions);
        }

        public static dynamic Juxt(IList<dynamic> fns) {
            return Currying.Juxt(fns);
        }

        public static dynamic UseWith<TSource>(Delegate fn, IList<dynamic> transformers) {
            return Currying.UseWith(Delegate(fn), transformers);
        }

        public static dynamic UseWith<TSource>(RamdaPlaceholder fn, IList<dynamic> transformers) {
            return Currying.UseWith(fn, transformers);
        }

        public static dynamic Pipe(params dynamic[] functions) {
            return Currying.Pipe(functions);
        }

        public static dynamic PipeK(params dynamic[] functions) {
            return Currying.PipeK(functions);
        }

        public static dynamic PipeP(params Func<dynamic, Task<dynamic>>[] functions) {
            return Currying.PipeP(functions);
        }

        public static dynamic PipeP(params Func<dynamic, dynamic, Task<dynamic>>[] functions) {
            return Currying.PipeP(functions);
        }

        public static dynamic PipeP(params Func<dynamic, dynamic, dynamic, Task<dynamic>>[] functions) {
            return Currying.PipeP(functions);
        }

        public static dynamic PipeP(params Func<dynamic, dynamic, dynamic, dynamic, Task<dynamic>>[] functions) {
            return Currying.PipeP(functions);
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

        public static dynamic Transduce<TSource, TAccumulator>(dynamic xf, dynamic fn, TAccumulator acc, IList<TSource> list) {
            return Currying.Transduce(Delegate(xf), Delegate(fn), acc, list);
        }

        public static dynamic Prepend(object el, IList list) {
            return Currying.Prepend(el, list);
        }
    }
}