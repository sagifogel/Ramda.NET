using System;
using System.Linq;
using Sys = System;
using System.Dynamic;
using System.Collections;
using static Ramda.NET.R;
using static Ramda.NET.Lambda;
using System.Collections.Generic;
using Object = Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    internal static partial class Currying
    {
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

        private static Tuple<object, IList> MapAccumInternal(int from, int indexerAcc, Func<int, bool> loopPredicate, Delegate fn, object acc, IList list) {
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

        private static object InternalIfElse(Delegate condition, Delegate onTrue, Delegate onFalse, params object[] arguments) {
            return (bool)((LambdaN)condition).Invoke(arguments) ? ((LambdaN)onTrue).Invoke(arguments) : ((LambdaN)onFalse).Invoke(arguments);
        }

        private static object InternalEvolve(IDictionary<string, object> transformations, object target) {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var keyValue in target.ToMemberDictionary()) {
                object transformation;
                var key = keyValue.Key;
                var value = keyValue.Value;

                if (transformations.TryGetValue(key, out transformation)) {
                    if (transformation.IsDelegate()) {
                        result[key] = ((Delegate)transformation).DynamicInvoke(value);
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

        private static bool AllOrAny(Delegate fn, IList list, bool returnValue) {
            foreach (var item in list) {
                if ((bool)fn.DynamicInvoke(item) == returnValue) {
                    return returnValue;
                }
            }

            return !returnValue;
        }

        internal static IDictionary<string, object> PickIntrenal(IList<string> names, object obj, bool setIfNull = false) {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var name in names) {
                object member;

                if (!Object.TryGetMember(name, obj, out member) && !setIfNull) {
                    continue;
                }

                result[name] = member;
            }

            return result;
        }

        private static object ReduceInternal(int from, int indexerAcc, Func<int, bool> loopPredicate, Delegate fn, object acc, IList list) {
            var idx = from;

            while (loopPredicate(idx)) {
                acc = fn.DynamicInvoke(acc, list[idx]);
                idx += indexerAcc;
            }

            return acc;
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

        private static IList TakeWhileInternal(int from, int indexerAcc, Func<int, bool> loopPredicate, Delegate fn, IList list, Func<int, int> sliceFrom, Func<int, int> sliceTo) {
            var idx = from;

            while (loopPredicate(idx) && (bool)fn.DynamicInvoke(list[idx])) {
                idx += indexerAcc;
            }

            return Core.Slice(list, sliceFrom(idx), sliceTo(idx));
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

        internal static object Member(object target, dynamic member) {
            if (member.GetType().Equals(typeof(int)) && target.IsArray()) {
                return ((Array)target).Member((int)member);
            }

            return target.Member((string)member);
        }

        private static bool IsPlaceholder(object param) {
            return param != null && R.__.Equals(param);
        }

        private static LambdaN InternalCurryN(int length, object[] received, Delegate fn) {
            return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var argsIdx = 0;
                var left = length;
                var combinedIdx = 0;
                var combined = new List<object>();
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                var argumentsLength = arguments?.Length ?? 0;

                while (combinedIdx < received.Length || argsIdx < argumentsLength) {
                    object result = null;

                    if (combinedIdx < received.Length && (!IsPlaceholder(received[combinedIdx]) || argsIdx >= argumentsLength)) {
                        result = received[combinedIdx];
                    }
                    else {
                        result = arguments[argsIdx];
                        argsIdx += 1;
                    }

                    combined.Insert(combinedIdx, result);

                    if (!IsPlaceholder(result)) {
                        left -= 1;
                    }

                    combinedIdx += 1;
                }

                return left <= 0 ? fn.Invoke(combined.ToArray()) : Arity(left, InternalCurryN(length, combined.ToArray(), fn));
            });
        }

        private static IdentityObj IdentityInternal(object x) {
            return new IdentityObj() {
                Value = x,
                Map = new Func<Delegate, IdentityObj>(f => {
                    return IdentityInternal(f.DynamicInvoke(x));
                })
            };
        }

        private static IdentityObj Const(object x) {
            var identity = new IdentityObj() { Value = x };

            identity.Map = new Func<Delegate, IdentityObj>(f => {
                return identity;
            });

            return identity;
        }

        internal static Delegate CreatePartialApplicator(Func<IList, IList, IList> concat) {
            return Curry2<Delegate, object[], Delegate>((fn, args) => {
                return Arity(Math.Max(0, fn.Arity() - args.Length), new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                    var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                    return fn.Invoke(concat(args, arguments));
                }));
            });
        }

        internal static IList DropLastInternal(int n, IList xs) {
            return Take(n < xs.Count ? xs.Count - n : 0, xs);
        }

        internal static IList DropLastWhileInternal(Delegate pred, IList list) {
            var idx = list.Count - 1;

            while (idx >= 0 && (bool)pred.Invoke(list[idx])) {
                idx -= 1;
            }

            return Core.Slice(list, 0, idx + 1);
        }

        internal static bool EqualsInternal(object a, object b) {
            bool bothEnumerables;
            Type typeA;
            Type typeB = b.GetType();

            if (Identical(a, b)) {
                return true;
            }

            if (a.IsNull() || b.IsNull()) {
                return false;
            }

            typeA = a.GetType();
            typeB = b.GetType();
            bothEnumerables = a.IsEnumerable() && b.IsEnumerable();

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

        internal static Func<IList, IList> MakeFlat(bool recursive) {
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

        internal static object Reduce(object fn, object acc, object list) {
            ITransformer transformer = null;

            if (fn.IsFunction()) {
                transformer = new XWrap((Delegate)fn);
            }

            if (list.IsEnumerable()) {
                return IterableReduce(transformer, acc, (IEnumerable)list);
            }

            if (list.WhereMember("Reduce", t => t.IsFunction())) {
                return MethodReduce(transformer, acc, list);
            }

            throw new ArgumentException("Reduce: list must be array or iterable");
        }

        private static object IterableReduce(ITransformer xf, object acc, IEnumerable list) {
            IReduced reduced;

            foreach (var item in list) {
                acc = xf.Step(acc, item);
                reduced = acc as IReduced;

                if (reduced.IsNotNull()) {
                    acc = reduced.Value;
                    break;
                }
            }

            return xf.Result(acc);
        }

        private static object MethodReduce(ITransformer xf, object acc, dynamic obj) {
            return xf.Result(obj.Reduce(new Func<object, object, object>(xf.Step), acc));
        }

        internal static ITransformer StepCat(object obj) {
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
                return transformer;
            }

            if (objType.Equals(typeof(object))) {
                return transformer;
            }

            throw new ArgumentException($"Cannot create transformer for {obj.GetType().Name}");
        }
    }
}
