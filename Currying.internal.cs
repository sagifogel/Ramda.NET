using System;
using System.Linq;
using Sys = System;
using System.Dynamic;
using System.Reflection;
using System.Collections;
using static Ramda.NET.R;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using static Ramda.NET.ReflectionExtensions;
using System.Text;

namespace Ramda.NET
{
    internal static partial class Currying
    {
        internal static DynamicDelegate Delegate(Func<object[], object> fn, int? length = null) {
            return new DelegateDecorator(fn, length);
        }

        internal static DynamicDelegate Delegate(object context, Delegate fn, int? length = null) {
            var @delegate = fn.Method.CreateDelegate(context);

            return Delegate((object[] arguments) => @delegate.DynamicInvoke(arguments), length);
        }

        internal static DynamicDelegate Delegate(Func<object> fn) {
            return new DelegateDecorator(fn);
        }

        internal static DynamicDelegate Delegate(Func<object, object> fn) {
            return new DelegateDecorator(fn);
        }

        internal static DynamicDelegate Delegate(Func<object, object, object> fn) {
            return new DelegateDecorator(fn);
        }

        internal static DynamicDelegate Delegate(Func<object, object, object, object> fn) {
            return new DelegateDecorator(fn);
        }

        internal static DynamicDelegate Delegate(dynamic fn) {
            Type type = fn.GetType();

            if (type.IsDelegate()) {
                return new DelegateDecorator((Delegate)fn);
            }

            return fn;
        }

        private static dynamic ComplementInternal(dynamic fn) {
            return Delegate((object[] arguments) => !DynamicInvoke(fn, arguments));
        }

        private static bool ContainsWith(Func<object, object, bool> predicate, object x, IList list) {
            foreach (var item in list) {
                if (predicate(x, item)) {
                    return true;
                }
            }

            return false;
        }

        private static IList FilterInternal(Func<object, bool> fn, IList list) {
            var result = new ArrayList();

            foreach (var item in list) {
                if (fn(item)) {
                    result.Add(item);
                }
            }

            return result.ToArray<Array>();
        }

        internal static IReduced ForceReduced(object x) => new ReducedImpl(x);

        private static bool HasInternal(string prop, object obj) {
            if (obj.IsDictionary()) {
                var dictionary = obj as IDictionary;
                IDictionary<string, object> expandoDictionary;

                if (dictionary.IsNotNull()) {
                    return dictionary.Contains(prop);
                }

                expandoDictionary = obj as IDictionary<string, object>;

                if (expandoDictionary.IsNotNull()) {
                    return expandoDictionary.ContainsKey(prop);
                }
            }

            return obj.TryGetMemberInfo(prop).IsNotNull();
        }

        internal static TValue IdentityInternal<TValue>(TValue x) => x;

        private static object[] MapInternal(dynamic fn, IList functor) {
            var idx = 0;
            var len = functor.Count;
            var result = new object[len];

            while (idx < len) {
                result[idx] = fn(functor[idx]);
                idx += 1;
            }

            return result;
        }

        private static Array OfInternal(object x) {
            var type = x.GetType();
            var list = type.CreateNewList<IList>();

            list.Insert(0, x);

            return list.ToArray<Array>();
        }

        private static DynamicDelegate PipeInternal(dynamic f, dynamic g) {
            return Delegate((object[] arguments) => g((object)DynamicInvoke(f, arguments)));
        }

        private static DynamicDelegate PipePInternal(dynamic f, dynamic g) {
            return Delegate((object[] arguments) => {
                return new Task<object>(() => DynamicInvoke(f, arguments)).Then(result => g(result));
            });
        }

        private static DynamicDelegate PipeFactory(Func<dynamic, dynamic, DynamicDelegate> pipe, string name) {
            return Delegate((object[] arguments) => {
                if (arguments.Length == 0) {
                    throw new ArgumentNullException($"{name} requires at least one argument");
                }

                var delegates = arguments.Select(arg => Delegate(arg)).ToArray();

                return Arity(delegates[0].Length, (DynamicDelegate)Reduce(Delegate(pipe), delegates[0], Tail(delegates)));
            });
        }

        internal static IReduced ReducedInternal(object x) {
            var reduced = x as IReduced;

            return reduced.IsNotNull() && reduced.IsReduced ? reduced : new ReducedImpl(x);
        }

        private static IList ApertureInternal(int length, IList list) {
            var idx = 0;
            var limit = list.Count - (length - 1);
            IList acc = null;

            limit = limit >= 0 ? limit : 0;
            acc = list.IsArray() ? (IList)new object[limit] : new List<object>(limit);

            while (idx < limit) {
                acc[idx] = list.Slice(idx, idx + length);
                idx += 1;
            }

            return acc;
        }

        private static object Assign(params object[] objectN) {
            return ObjectAssigner.Assign(new ExpandoObject(), objectN);
        }

        private static object Assign(IList list) {
            return ObjectAssigner.Assign(new ExpandoObject(), list.Cast<object>().ToArray());
        }

        private static Curry1 CheckForMethod1(string methodName, dynamic fn) {
            return Curry1<IList, IList>(list => (IList)CheckForMethodN(methodName, fn, list));
        }

        private static Func<object, object, object> CheckForMethod2(string methodName, dynamic fn) {
            return new Func<object, object, object>((arg1, arg2) => CheckForMethodN(methodName, fn, arg1, arg2));
        }

        private static Func<TArg1, TArg2, TArg3, TResult> CheckForMethod3<TArg1, TArg2, TArg3, TResult>(string methodName, Func<TArg1, TArg2, TArg3, TResult> fn) {
            return (arg1, arg2, arg3) => (TResult)CheckForMethodN(methodName, Delegate(fn), arg1, arg2, arg3);
        }

        private static object CheckForMethodN(string methodName, dynamic fn, params object[] arguments) {
            object obj;
            object member;
            var invokeFn = false;
            var length = arguments.Length;

            if (length == 0) {
                return fn(new object[0]);
            }

            obj = arguments[length - 1];
            member = obj.TryGetMemberInfo(methodName);
            invokeFn = member.IsNotNull() && !member.IsFunction();

            if (invokeFn || obj.IsList()) {
                return DynamicInvoke(fn, arguments);
            }

            return ((dynamic)member).DynamicInvoke(obj, arguments.Slice(0, length - 1));
        }

        private static Func<object, object> Dispatchable1(string methodName, dynamic xf, dynamic fn) {
            return new Func<object, object>(arg => {
                return Dispatchable(methodName, xf, fn, Arguments(arg));
            });
        }

        private static DynamicDelegate Dispatchable2(string methodName, dynamic xf, Delegate fn) {
            return Dispatchable2(methodName, xf, new DelegateDecorator(fn));
        }

        private static DynamicDelegate Dispatchable2(string methodName, dynamic xf, DynamicDelegate fn) {
            return Delegate(new Func<object, object, object>((arg1, arg2) => Dispatchable(methodName, xf, fn, Arguments(arg1, arg2))));
        }

        private static DynamicDelegate Dispatchable4(string methodName, dynamic xf, dynamic fn) {
            return Delegate(new Func<dynamic, dynamic, dynamic, dynamic, dynamic>((arg1, arg2, arg3, arg4) => {
                return Dispatchable(methodName, xf, fn, Arguments(arg1, arg2, arg3, arg4));
            }));
        }

        private static object Dispatchable(string methodName, dynamic xf, dynamic fn, object[] arguments) {
            object obj;

            if (arguments.Length == 0) {
                return fn.Invoke(new object[0]);
            }

            obj = arguments[arguments.Length - 1];

            if (!obj.IsList()) {
                var args = (object[])arguments.Slice(0, arguments.Length - 1);
                var member = obj.Member(methodName) as Delegate;

                if (member.IsNotNull()) {
                    return member.DynamicInvoke(args);
                }

                if (obj is ITransformer) {
                    var transformer = DynamicInvoke(xf, args);

                    return transformer(obj);
                }
            }

            return DynamicInvoke(fn, arguments);
        }

        private static IList DropLastWhileInternal(dynamic pred, IList list) {
            var idx = list.Count - 1;

            while (idx >= 0 && (bool)pred.Invoke(list[idx])) {
                idx -= 1;
            }

            return list.Slice(0, idx + 1);
        }

        private readonly static dynamic XAll = Curry2(new Func<dynamic, ITransformer, ITransformer>((f, xf) => new XAll((o => f(o)), xf)));

        private readonly static dynamic XAny = Curry2(new Func<dynamic, ITransformer, ITransformer>((f, xf) => new XAny((o => f(o)), xf)));

        private readonly static dynamic XAperture = Curry2(new Func<int, ITransformer, ITransformer>((n, xf) => new XAperture(n, xf)));

        private readonly static dynamic XDrop = Curry2(new Func<int, ITransformer, ITransformer>((n, xf) => new XDrop(n, xf)));

        private readonly static dynamic XDropLast = Curry2(new Func<int, ITransformer, ITransformer>((n, xf) => new XDropLast(n, xf)));

        private readonly static dynamic XDropRepeatsWith = Curry2(new Func<dynamic, ITransformer, ITransformer>((pred, xf) => new XDropRepeatsWith(pred, xf)));

        private readonly static dynamic XDropWhile = Curry2(new Func<dynamic, ITransformer, ITransformer>((f, xf) => new XDropWhile(f, xf)));

        private readonly static dynamic XFilter = Curry2(new Func<dynamic, ITransformer, ITransformer>((f, xf) => new XFilter(f, xf)));

        private readonly static dynamic XFind = Curry2(new Func<dynamic, ITransformer, ITransformer>((f, xf) => new XFind(f, xf)));

        private readonly static dynamic XFindIndex = Curry2(new Func<dynamic, ITransformer, ITransformer>((f, xf) => new XFindIndex(f, xf)));

        private readonly static dynamic XFindLast = Curry2(new Func<dynamic, ITransformer, ITransformer>((f, xf) => new XFindLast(f, xf)));

        private readonly static dynamic XFindLastIndex = Curry2(new Func<dynamic, ITransformer, ITransformer>((f, xf) => new XFindLastIndex(f, xf)));

        private readonly static dynamic XMap = Curry2(new Func<dynamic, ITransformer, ITransformer>((f, xf) => new XMap(f, xf)));

        private readonly static dynamic XReduceBy = CurryNInternal(4, new object[0], Delegate(new Func<dynamic, object, dynamic, ITransformer, ITransformer>((valueFn, valueAcc, keyFn, xf) => {
            return new XReduceBy(valueFn, valueAcc, keyFn, xf);
        })));

        private readonly static dynamic XTake = Curry2(new Func<int, ITransformer, ITransformer>((n, xf) => new XTake(n, xf)));

        private readonly static dynamic XTakeWhile = Curry2(new Func<DynamicDelegate, ITransformer, ITransformer>((f, xf) => new XTakeWhile(f, xf)));

        private readonly static dynamic XDropLastWhile = Curry2(new Func<DynamicDelegate, ITransformer, ITransformer>((f, xf) => new XDropLstWhile(f, xf)));

        private readonly static dynamic XChain = Curry2(new Func<dynamic, ITransformer, object>((f, xf) => Map(f, new XFlatCat(xf))));

        internal class ComparerFactory : IComparer
        {
            private Func<object, object, int> comparator;

            internal ComparerFactory(Func<object, object, int> comparator) {
                this.comparator = comparator;
            }

            public int Compare(object x, object y) {
                return comparator(x, y);
            }
        }

        private static Tuple<object, IList> MapAccumInternal(int from, int indexerAcc, Func<int, bool> loopPredicate, dynamic fn, object acc, IList list) {
            var tuple = R.Tuple.Create(acc, null);
            IList result = new object[list.Count];

            while (loopPredicate(from)) {
                tuple = (R.Tuple)fn.DynamicInvoke(tuple.Item1, list[from]);
                result[from] = tuple.Item1;
                from += indexerAcc;
            }

            return Sys.Tuple.Create(tuple.Item1, result);
        }

        private static object FindInternal(int from, int indexerAcc, Func<int, bool> loopPredicate, Func<object, bool> fn, IList list) {
            var idx = from;

            while (loopPredicate(idx)) {
                if (fn(list[idx])) {
                    return list[idx];
                }

                idx += indexerAcc;
            }

            return null;
        }

        private static int FindIndexInternal(int from, int indexerAcc, Func<int, bool> loopPredicate, Func<object, bool> fn, IList list) {
            var idx = from;

            while (loopPredicate(idx)) {
                if (fn(list[idx])) {
                    return idx;
                }

                idx += indexerAcc;
            }

            return -1;
        }

        private static object InternalIfElse(dynamic condition, dynamic onTrue, dynamic onFalse, params object[] arguments) {
            return condition(arguments) ? onTrue(arguments) : onFalse(arguments);
        }

        private static object InternalEvolve(IDictionary<string, object> transformations, object target) {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var keyValue in target.ToMemberDictionary()) {
                object transformation;
                var key = keyValue.Key;
                var value = keyValue.Value;

                if (transformations.TryGetValue(key, out transformation)) {
                    if (transformation.IsFunction()) {
                        result[key] = ((dynamic)transformation).Invoke(value);
                        continue;
                    }
                    else if (value is object) {
                        if (!transformation.IsDictionary()) {
                            transformation = transformation.ToMemberDictionary();
                        }

                        result[key] = InternalEvolve((IDictionary<string, object>)transformation, value);
                        continue;
                    }
                }

                result[key] = value;
            }

            return result;
        }

        private static object InternalDissocPath(IList path, object obj) {
            switch (path.Count) {
                case 0:
                    return obj;
                case 1:
                    return Dissoc(path[0], obj);
                default:
                    var head = (string)path[0];
                    var tail = Slice(path, 1);
                    var headValue = obj.Member(head);

                    return headValue.IsNull() ? obj : Assoc(head, InternalDissocPath(tail, headValue), obj);
            }
        }

        private static bool AllOrAny(dynamic fn, IList list, bool returnValue) {
            foreach (var item in list) {
                if (fn(item) == returnValue) {
                    return returnValue;
                }
            }

            return !returnValue;
        }

        private static IDictionary<string, object> PickIntrenal(IList<string> names, object obj, bool setIfNull = false) {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var name in names) {
                object member;

                if (!TryGetMember(name, obj, out member) && !setIfNull) {
                    continue;
                }

                result[name] = member;
            }

            return result;
        }

        private static IList SortInternal(IList list, IComparer comparer) {
            if (list.IsArray()) {
                Array.Sort((Array)list, comparer);

                return list;
            }

            var array = list.ToArray<Array>();
            Array.Sort(array, comparer);

            return array.CreateNewList(array);
        }

        private static IList TakeWhileInternal(int from, int indexerAcc, Func<int, bool> loopPredicate, dynamic fn, IList list, Func<int, int> sliceFrom, Func<int, int> sliceTo) {
            var idx = from;

            while (loopPredicate(idx) && (bool)fn.DynamicInvoke(list[idx])) {
                idx += indexerAcc;
            }

            return list.Slice(sliceFrom(idx), sliceTo(idx));
        }

        private static IList[] ZipInternal(IList a, IList b, int len, Func<IList, int, object> valAResolver = null) {
            var idx = 0;
            var rv = new List<IList>();

            while (idx < len) {
                IList pair;
                var valB = b[idx];
                var valA = valAResolver?.Invoke(a, idx) ?? a[idx];
                var typeA = valA.GetType();

                if (typeA.Equals(valB.GetType())) {
                    pair = typeA.CreateNewList<IList>();
                    pair.Add(valA);
                    pair.Add(valB);
                    pair = pair.ToArray<Array>(typeA);
                }
                else {
                    pair = new object[] { valA, valB };
                }

                rv.Add(pair);
                idx += 1;
            }

            return rv.ToArray();
        }

        internal static bool IsPlaceholder(object param) {
            return param != null && R.__.Equals(param);
        }

        private static DynamicDelegate CurryNInternal(int length, object[] received, DynamicDelegate fn) {
            return new CurryN(fn, received, length);
        }

        private static DynamicDelegate CurryNInternal(int length, object[] received, Delegate fn) {
            return CurryNInternal(length, received, new DelegateDecorator(fn));
        }

        private static Functor IdentityFunctor(object x) {
            return new Functor {
                Value = x,
                Map = new Func<dynamic, Functor>(f => {
                    return IdentityFunctor(f.DynamicInvoke(x));
                })
            };
        }

        private static Functor Const(object x) {
            var functor = new Functor { Value = x };

            functor.Map = new Func<dynamic, Functor>(f => {
                return functor;
            });

            return functor;
        }

        private static dynamic CreatePartialApplicator(dynamic concat) {
            return Curry2(new Func<DynamicDelegate, IList, dynamic>((fn, args) => {
                return Arity(Math.Max(0, fn.Arity() - args.Count), Delegate((object[] arguments) => {
                    dynamic dynamicFn = fn;
                    var concatedArgs = (IList)concat.Invoke(args, arguments);

                    return dynamicFn(concatedArgs.ToArray<object[]>(typeof(object)));
                }));
            }));
        }

        private static IList DropLastInternal(int n, IList xs) {
            return Take(n < xs.Count ? xs.Count - n : 0, xs);
        }

        private static bool EqualsInternal(object a, object b) {
            Type typeA;
            bool bothEnumerables;
            Type typeB = b.GetType();
            var typeofString = typeof(string);

            if (Identical(a, b)) {
                return true;
            }

            if (a.IsNull() || b.IsNull()) {
                return false;
            }

            typeA = a.GetType();
            bothEnumerables = a.IsEnumerable() && !typeA.Equals(typeofString) && b.IsEnumerable() && !typeA.Equals(typeofString);

            if (!typeA.Equals(typeB) && !bothEnumerables) {
                var bothAnonymous = a.IsAnonymousType() && b.IsAnonymousType();

                if (!bothAnonymous) {
                    return false;
                }
            }

            if (bothEnumerables) {
                return ((IEnumerable)a).SequenceEqual((IEnumerable)b, EqualsInternal);
            }

            if (typeA.Equals(typeB) && (typeA.IsPrimitive || typeA.Equals(typeof(string)) || typeA.Equals(typeof(DateTime)) || typeA.Equals(typeof(decimal)) || typeA.Equals(typeof(Guid)))) {
                return false;
            }

            return MemberwiseComparer.Compare(a, b);
        }

        private static Func<IList, IList> MakeFlat(bool recursive) {
            return list => Flatt(list, recursive);
        }

        private static IList Flatt(IList list, bool recursive) {
            var result = new ArrayList();

            foreach (var item in list) {
                if (IsArrayLike(item)) {
                    var itemAsList = (IList)item;
                    var value = recursive ? Flatt(itemAsList, recursive) : itemAsList;
                    var list2 = itemAsList.CreateNewList(type: typeof(object));

                    foreach (var item2 in value) {
                        result.Add(item2);
                    }
                }
                else {
                    result.Add(item);
                }
            }

            return result.ToArray<IList>();
        }

        internal static object ReduceInternal(object fn, object acc, object list) {
            IReducible reducible = null;
            var transformer = fn as ITransformer;

            if (fn.IsFunction()) {
                transformer = new XWrap((dynamic)fn);
            }

            if (list.IsEnumerable()) {
                return IterableReduce(transformer, acc, (IEnumerable)list);
            }

            reducible = list as IReducible;

            if (reducible.IsNotNull()) {
                return MethodReduce(transformer, acc, reducible);
            }

            throw new ArgumentException("Reduce: list must be array or iterable");
        }

        private static object IterableReduce(ITransformer xf, object acc, IEnumerable list) {
            object result;
            IReduced reduced;

            foreach (var item in list) {
                acc = xf.Step(acc, item);
                reduced = acc as IReduced;

                if (reduced.IsNotNull()) {
                    acc = reduced.Value;
                    break;
                }
            }

            result = xf.Result(acc);

            if (result.IsList()) {
                return new ArrayList((ICollection)result).ToArray<Array>();
            }

            return result;
        }

        private static object MethodReduce(ITransformer xf, object acc, IReducible obj) {
            return xf.Result((object)obj.Reduce(new Func<object, object, object>(xf.Step), acc));
        }

        private static ITransformer StepCat(object obj) {
            Type objType;
            var transformer = obj as ITransformer;

            if (transformer.IsNotNull()) {
                return transformer;
            }

            if (IsArrayLike(obj)) {
                return new StepCatArray();
            }

            objType = obj.GetType();

            if (objType.Equals(typeof(string))) {
                return new StepCatString();
            }

            if (typeof(object).IsAssignableFrom(objType)) {
                return new StepCatObject();
            }

            throw new ArgumentException($"Cannot create transformer for {obj.GetType().Name}");
        }

        private static DynamicDelegate AnyOrAllPass(IList preds, bool comparend) {
            return Delegate((object[] arguments) => {
                foreach (dynamic pred in preds) {
                    var dynamicPred = Delegate(pred);

                    if (DynamicInvoke(dynamicPred, arguments) == comparend) {
                        return comparend;
                    }
                }

                return !comparend;
            });
        }

        private static object ApplySpecInternal(object spec) {
            spec = Map(new Func<object, object>(v => v.IsFunction() ? v : ApplySpecInternal(v)), spec);

            return CurryN(Reduce(Max, 0, Pluck("Length", Values(spec))), Delegate((object[] arguments) => {
                return Map(new Func<object, object>(f => Apply(f, arguments)), spec);
            }));
        }

        private static object ArrayOf = new DelegateDecorator(new Func<object[], object[]>(arguments => arguments));

        private static bool ContainsInternal(object item, object list) {
            if (list.IsList()) {
                return ((IList)list).Contains(item);
            }

            var contains = list.GetMemberWhen<MethodInfo>("Contains", m => m.MemberType == MemberTypes.Method);

            if (contains.IsNotNull()) {
                return (bool)contains.Invoke(list, new[] { item });
            }

            return false;
        }

        private static string ToStringInternal(object x) {
            if (x.IsList()) {
                var list = (IList)x;
                var stringBuilder = new StringBuilder("[");

                list.ForEach(item => stringBuilder.Append(ToStringInternal(item)));

                return stringBuilder.Append("]").ToString();
            }

            return x.ToString();
        }

        private static DynamicDelegate ComposeFactory(dynamic pipe, string name) {
            return Delegate((object[] arguments) => {
                if (arguments.IsNull() || arguments.Length == 0) {
                    throw new ArgumentNullException($"{name} requires at least one argument");
                }

                return DynamicInvoke(pipe, Reverse(arguments));
            });
        }

        private static object BothOrEither(DynamicDelegate f, DynamicDelegate g, Func<Func<bool>, Func<bool>, bool> operand, dynamic liftBy) {
            if (f.IsNotNull()) {
                return Delegate((object[] arguments) => operand(() => DynamicInvoke((dynamic)f, arguments), () => DynamicInvoke((dynamic)g, arguments)));
            }

            return Lift(liftBy)(f, g);
        }
    }
}
