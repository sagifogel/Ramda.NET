using System;
using Ramda.NET;
using System.Linq;
using System.Dynamic;
using System.Collections;
using static Ramda.NET.R;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core = Ramda.NET.IEnumerableExtensions;
using Reflection = Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    internal static partial class Currying
    {
        internal static dynamic Curry1(DynamicDelegate fn) {
            return new Curry1(fn);
        }

        internal static dynamic Curry1(Delegate fn) {
            return new Curry1(fn);
        }

        internal static dynamic Curry1(Func<object, object> fn) {
            return Curry1((Delegate)fn);
        }

        internal static dynamic Curry1<TArg1, TResult>(Func<TArg1, TResult> fn) {
            return Curry1((Delegate)fn);
        }

        internal static dynamic Curry2(DynamicDelegate fn) {
            return new Curry2(fn);
        }

        internal static dynamic Curry2(Delegate fn) {
            return new Curry2(fn);
        }

        internal static dynamic Curry2(Func<object, object, object> fn) {
            return Curry2((Delegate)fn);
        }

        internal static dynamic Curry2<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> fn) {
            return Curry2((Delegate)fn);
        }

        internal static dynamic Curry3(DynamicDelegate fn) {
            return new Curry3(fn);
        }

        internal static dynamic Curry3(Delegate fn) {
            return new Curry3(fn);
        }

        internal static dynamic Curry3(Func<object, object, object, object> fn) {
            return Curry3((Delegate)fn);
        }

        internal static dynamic Curry3<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, TResult> fn) {
            return Curry3((Delegate)fn);
        }

        internal static dynamic CurryParams(DynamicDelegate fn) {
            return new CurryParams(fn);
        }

        internal static dynamic CurryN = Curry2<int, dynamic, dynamic>((length, fn) => {
            if (length == 1) {
                return Curry1(fn);
            }

            return Arity(length, CurryNInternal(length, new object[0], fn));
        });

        internal readonly static dynamic Add = Curry2<object, object, dynamic>((arg1, arg2) => arg1.ToNumber() + arg2.ToNumber());

        internal readonly static dynamic Adjust = Curry3<DynamicDelegate, int, IList, IList>((fn, idx, list) => {
            var start = 0;
            var index = 0;
            Type elementType;
            object adjustedValue;
            IList concatedList = null;

            if (idx >= list.Count || idx < -list.Count) {
                return list;
            }

            start = idx < 0 ? list.Count : 0;
            index = start + idx;
            concatedList = list.Concat();
            elementType = concatedList.GetElementType();
            adjustedValue = ((dynamic)fn)(list[index]);

            if (!elementType.Equals(adjustedValue.GetType())) {
                adjustedValue = adjustedValue.Cast(elementType);
            }

            concatedList[index] = adjustedValue;

            return concatedList;
        });


        internal readonly static dynamic Always = Curry1<dynamic, DynamicDelegate>(value => Delegate(() => value));

        internal readonly static dynamic And = Curry2<bool, bool, bool>((a, b) => a && b);

        internal readonly static dynamic All = Curry2(Dispatchable2("All", XAll, new Func<dynamic, IList, bool>((fn, list) => AllOrAny(Delegate(fn), list, false))));

        internal readonly static dynamic Any = Curry2(Dispatchable2("Any", XAny, new Func<dynamic, IList, bool>((fn, list) => AllOrAny(Delegate(fn), list, true))));

        internal readonly static dynamic Aperture = Curry2(Dispatchable2("Aperture", XAperture, Delegate(new Func<int, IList, IList>(ApertureInternal))));

        internal readonly static dynamic Append = Curry2<object, IList, IList>((el, list) => list.Concat(list.CreateNewList(new[] { el })));

        internal readonly static dynamic Apply = Curry2<dynamic, object[], object>((fn, args) => Reflection.DynamicInvoke(fn, args));

        internal readonly static dynamic Assoc = Curry3<object, object, object, object>((prop, val, obj) => ShallowCloner.CloneAndAssignValue(prop, val, obj));

        internal readonly static dynamic AssocPath = Curry3<IList, object, object, object>((path, val, obj) => {
            switch (path.Count) {
                case 0:
                    return val;
                case 1:
                    return Assoc(path[0], val, obj);
                default:
                    return Assoc(path[0], AssocPath(path.Slice(1), val, Reflection.MemberOr(obj, path[0], () => new ExpandoObject())), obj);
            }
        });

        internal readonly static dynamic Bind = Curry2<Delegate, object, object>((fn, thisObj) => {
            return Arity(fn.Arity(), Delegate(thisObj, fn));
        });

        internal readonly static dynamic Clamp = Curry3<dynamic, dynamic, dynamic, dynamic>((min, max, value) => {
            if (min > max) {
                throw new ArgumentOutOfRangeException("min must not be greater than max in Clamp(min, max, value)");
            }

            return value < min ? min : value > max ? max : value;
        });

        internal readonly static dynamic Comparator = Curry1<DynamicDelegate, DynamicDelegate>(pred => {
            dynamic dynamicPred = pred;

            return Delegate((a, b) => dynamicPred(a, b) ? -1 : dynamicPred(b, a) ? 1 : 0);
        });

        internal readonly static dynamic Dec = Add(-1);

        internal readonly static dynamic DefaultTo = Curry2<object, object, object>((defaultValue, value) => {
            var type = value.GetType();

            if (type.IsClass) {
                return ReferenceEquals(value, Null) ? defaultValue : value;
            }

            return value == type.GetDefaultValue() ? defaultValue : value;
        });

        internal readonly static dynamic DifferenceWith = Curry3<DynamicDelegate, IList, IList, IList>((pred, first, second) => {
            var idx = 0;
            var firstLen = first.Count;
            var result = new ArrayList();
            dynamic dynamicPred = pred;
            Func<object, object, bool> containsPredicate = (a, b) => (bool)dynamicPred(a, b);

            while (idx < firstLen) {
                if (!ContainsWith(containsPredicate, first[idx], second) && !ContainsWith(containsPredicate, first[idx], result)) {
                    result.Add(first[idx]);
                }

                idx += 1;
            }

            if (first.IsArray()) {
                return result.ToArray<Array>();
            }

            return result.ToArray();
        });

        internal readonly static dynamic Dissoc = Curry2<string, object, object>((prop, obj) => ShallowCloner.CloneAndOmitValue(prop, obj));

        internal readonly static dynamic DissocPath = Curry2<IList, object, object>(InternalDissocPath);

        internal readonly static dynamic Divide = Curry2<dynamic, dynamic, dynamic>((a, b) => a / b);

        internal readonly static dynamic DropWhile = Curry2(Dispatchable2("DropWhile", XDropWhile, new Func<dynamic, IList, IList>((pred, list) => {
            var idx = 0;
            var len = list.Count;
            dynamic dynamicPred = Delegate(pred);

            while (idx < len && dynamicPred(list[idx])) {
                idx += 1;
            }

            return list.Slice(idx);
        })));

        internal readonly static dynamic Empty = Curry1<object, dynamic>(x => {
            if (x.IsNotNull()) {
                Type type = null;
                var @delegate = x.Member("Empty") as Delegate;

                if (@delegate != null) {
                    return @delegate.Invoke(new object[0]);
                }

                type = x.GetType();

                if (x.IsArray()) {
                    return ((Array)x).CreateNewArray(0);
                }

                if (x.IsList()) {
                    return ((IEnumerable)x).CreateNewList();
                }

                if (type.Equals(typeof(string))) {
                    return string.Empty;
                }

                if (x.IsEnumerable()) {
                    return new object[0];
                }

                if (type.IsClass && !type.TypeIsDelegate()) {
                    if (type.IsAnonymousType()) {
                        return new ExpandoObject();
                    }

                    return x.GetFactory().Invoke();
                }
            }

            return null;
        });

        internal readonly static dynamic Evolve = Curry2<object, object, object>(InternalEvolve);

        internal readonly static dynamic Find = Curry2(Dispatchable2("Find", XFind, new Func<dynamic, IList, object>((fn, list) => {
            var dynamicFn = Delegate(fn);

            return FindInternal(0, 1, idx => idx < list.Count, obj => Reflection.DynamicInvoke(dynamicFn, new[] { obj }), list);
        })));

        internal readonly static dynamic FindIndex = Curry2(Dispatchable2("FindIndex", XFindIndex, new Func<dynamic, IList, int>((fn, list) => {
            var dynamicFn = Delegate(fn);

            return FindIndexInternal(0, 1, idx => idx < list.Count, obj => dynamicFn(obj), list);
        })));

        internal readonly static dynamic FindLast = Curry2(Dispatchable2("FindLast", XFindLast, new Func<dynamic, IList, object>((fn, list) => {
            var dynamicFn = Delegate(fn);

            return FindInternal(list.Count - 1, -1, idx => idx >= 0, obj => dynamicFn(obj), list);
        })));

        internal readonly static dynamic FindLastIndex = Curry2(Dispatchable2("FindLastIndex", XFindLastIndex, new Func<dynamic, IList, int>((fn, list) => {
            var dynamicFn = Delegate(fn);

            return FindIndexInternal(list.Count - 1, -1, idx => idx >= 0, obj => dynamicFn(obj), list);
        })));

        internal readonly static dynamic ForEach = Curry2(CheckForMethod2("ForEach", Delegate(new Func<dynamic, IList, IList>((fn, list) => {
            foreach (var item in list) {
                Reflection.DynamicInvoke(fn, new[] { item });
            }

            return list;
        }))));

        internal readonly static dynamic FromPairs = Curry1<object[][], IDictionary<string, object>>(pairs => {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var pair in pairs) {
                var key = pair[0];

                if (key is string) {
                    result[key.ToString()] = pair[1];
                }
            }

            return result;
        });

        internal readonly static dynamic GroupWith = Curry2<Delegate, IList, IList>((fn, list) => {
            var idx = 0;
            var len = list.Count;
            var res = new ArrayList();
            dynamic dynamicFn = Delegate(fn);

            while (idx < len) {
                var nextidx = idx + 1;

                while (nextidx < len && dynamicFn(list[idx], list[nextidx])) {
                    nextidx += 1;
                }

                res.Add(Slice(list, idx, nextidx));
                idx = nextidx;
            }

            if (list.IsArray()) {
                return res.ToArray();
            }

            return res;
        });

        internal readonly static dynamic Gt = Curry2<dynamic, dynamic, bool>((a, b) => a > b);

        internal readonly static dynamic Gte = Curry2<dynamic, dynamic, bool>((a, b) => a >= b);

        internal readonly static dynamic Has = Curry2<string, object, bool>(HasInternal);

        internal readonly static dynamic Identical = Curry2<object, object, bool>((a, b) => a?.Equals(b) ?? b.IsNull());

        internal readonly static dynamic Identity = Curry1<object, object>(IdentityInternal);

        internal readonly static dynamic IfElse = Curry3<DynamicDelegate, DynamicDelegate, DynamicDelegate, DynamicDelegate>((condition, onTrue, onFalse) => {
            return CurryN(1, Curry1(arg1 => InternalIfElse(condition, onTrue, onFalse, arg1)));
        });

        internal readonly static dynamic Inc = Add(1);

        internal readonly static dynamic Insert = Curry3<int, object, IList, IList>((idx, elt, list) => {
            var result = list.CreateNewList();

            idx = idx < list.Count && idx >= 0 ? idx : list.Count;
            result.Insert(idx, elt);

            if (list.IsArray()) {
                return result.ToArray<Array>();
            }

            return result;
        });

        internal readonly static dynamic InsertAll = Curry3<int, IList, IList, IList>((idx, elts, list) => {
            idx = idx < list.Count && idx >= 0 ? idx : list.Count;

            return Slice(list, 0, idx).Concat(elts).Concat(Slice(list, idx));
        });

        internal readonly static dynamic Intersperse = Curry2(CheckForMethod2("Intersperse", new Func<object, IList, IList>((separator, list) => {
            var idx = 0;
            IList result = null;
            var length = list.Count;
            Type underlyingType = null;
            var listType = list.GetType();

            if (listType.IsArray) {
                var separatorType = underlyingType = separator.GetType();

                if (!separatorType.Equals(listType.GetElementType())) {
                    underlyingType = typeof(object);
                }
            }

            result = list.CreateNewList(type: underlyingType);

            while (idx < length) {
                result.Add(list[idx]);

                if (idx != length - 1) {
                    result.Add(separator);
                }

                idx += 1;
            }

            return result;
        })));

        internal readonly static dynamic IsArrayLike = Curry1<object, bool>(x => x.IsList());

        internal readonly static dynamic Is = Curry2<Type, object, bool>((type, val) => {
            if (val.IsNotNull()) {
                var valueType = val.GetType();

                return valueType.Equals(type) || valueType.IsInstanceOfType(type);
            }

            return false;
        });

        internal readonly static dynamic IsNil = Curry1<object, bool>(val => val.IsNull());

        internal readonly static dynamic Keys = Curry1<object, IEnumerable<string>>(val => val.ToMemberDictionary().Select(kv => kv.Key));

        internal readonly static dynamic Length = Curry1<IList, int>(list => list.Count);

        internal readonly static dynamic Lt = Curry2<dynamic, dynamic, bool>((a, b) => a < b);

        internal readonly static dynamic Lte = Curry2<dynamic, dynamic, bool>((a, b) => a <= b);

        internal readonly static dynamic MapAccum = Curry3<Delegate, object, IList, Tuple<object, IList>>((fn, acc, list) => MapAccumInternal(0, 1, from => from < list.Count, fn, acc, list));

        internal readonly static dynamic MapAccumRight = Curry3<Delegate, object, IList, Tuple<object, IList>>((fn, acc, list) => MapAccumInternal(list.Count - 1, -1, from => from >= 0, fn, acc, list));

        internal readonly static dynamic Match = Curry2<Regex, string, MatchCollection>((rx, str) => rx.Matches(str));

        internal readonly static dynamic MathMod = Curry2<dynamic, dynamic, dynamic>((m, p) => (m % p + p) % p);

        internal readonly static dynamic Max = Curry2<dynamic, dynamic, dynamic>((a, b) => b > a ? b : a);

        internal readonly static dynamic MaxBy = Curry3<Delegate, dynamic, dynamic, dynamic>((f, a, b) => f.DynamicInvoke(b) > f.DynamicInvoke(a) ? b : a);

        internal readonly static dynamic Merge = Curry2<object, object, object>((l, r) => Assign(l, r));

        internal readonly static dynamic MergeAll = Curry1<IList, object>(Assign);

        internal readonly static dynamic MergeWithKey = Curry3<Delegate, object, object, object>((fn, l, r) => {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var pair in l.ToMemberDictionary()) {
                if (HasInternal(pair.Key, l)) {
                    result[pair.Key] = HasInternal(pair.Key, r) ? fn.Invoke(pair, l.Member(pair.Key), r.Member(pair.Key)) : l.Member(pair.Key);
                }
            }

            foreach (var pair in r.ToMemberDictionary()) {
                if (HasInternal(pair.Key, r) && !HasInternal(pair.Key, result)) {
                    result[pair.Key] = r.Member(pair.Key);
                }
            }

            return result;
        });

        internal readonly static dynamic Min = Curry2<dynamic, dynamic, dynamic>((a, b) => b < a ? b : a);

        internal readonly static dynamic MinBy = Curry3<Delegate, dynamic, dynamic, dynamic>((f, a, b) => f.DynamicInvoke(b) < f.DynamicInvoke(a) ? b : a);

        internal readonly static dynamic Modulo = Curry2<dynamic, dynamic, dynamic>((a, b) => a % b);

        internal readonly static dynamic Multiply = Curry2<dynamic, dynamic, dynamic>((a, b) => a * b);

        internal readonly static dynamic NAry = Curry2<int, DynamicDelegate, DynamicDelegate>((length, fn) => {
            if (length <= 10) {
                return Delegate(arguments => {
                    object[] args;
                    dynamic dynamicFn = fn;

                    length = Math.Min(length, arguments.Length);
                    args = new object[length];
                    Array.Copy(arguments, (Array)args, length);

                    return Reflection.DynamicInvoke(dynamicFn, args);
                }, length);
            }
            else {
                throw new ArgumentOutOfRangeException("length", "First argument to NAry must be a non-negative integer no greater than ten");
            }
        });

        internal readonly static dynamic Negate = Curry1<dynamic, dynamic>(n => -n);

        internal readonly static dynamic None = Curry2(Dispatchable2("Any", XAny, new Func<object, object, dynamic>((a, b) => Any(a, b))));

        internal readonly static dynamic Not = Curry1<bool, bool>(a => !a);

        internal readonly static dynamic Nth = Curry2<int, dynamic, object>((offset, list) => {
            var count = list.GetType().Equals(typeof(string)) ? ((string)list).Length : ((IList)list).Count;
            var idx = offset < 0 ? count + offset : offset;

            return count > 0 && idx <= count ? list[idx] : null;
        });

        internal readonly static dynamic NthArg = Curry1<int, DynamicDelegate>(n => {
            var arity = n < 0 ? 1 : n + 1;

            return CurryN(arity, Curry1(arguments => {
                return Nth(n, arguments);
            }));
        });

        internal readonly static dynamic ObjOf = Curry2<string, object, object>((key, val) => {
            IDictionary<string, object> obj = new ExpandoObject();

            obj[key] = val;

            return obj;
        });

        internal readonly static dynamic Of = Curry1<object, dynamic>(OfInternal);

        internal readonly static dynamic Once = Curry1<DynamicDelegate, DynamicDelegate>(fn => {
            var called = false;
            object result = null;

            return Arity(fn.Arity(), Delegate((object[] arguments) => {
                dynamic dynamicFn = fn;

                if (called) {
                    return result;
                }


                called = true;
                result = dynamicFn(arguments);
                return result;
            }));
        });

        internal readonly static dynamic Or = Curry2<bool, bool, bool>((a, b) => a || b);

        internal readonly static dynamic Over = Curry3<Func<Func<object, Functor>, Func<object, Functor>>, Delegate, object, object>((lens, f, x) => {
            return lens(y => IdentityFunctor(f.Invoke(y)))(x).Value;
        });

        internal readonly static dynamic Pair = Curry2<object, object, object[]>((fst, snd) => new object[2] { fst, snd });

        internal readonly static dynamic Path = Curry2<IList<string>, object, object>((paths, obj) => {
            var idx = 0;
            var val = obj;

            while (idx < paths.Count) {
                if (val.IsNull()) {
                    return null;
                }

                val = val.Member(paths[idx]);
                idx += 1;
            }

            return val;
        });

        internal readonly static dynamic PathOr = Curry3<object, IList<string>, object, object>((d, p, obj) => DefaultTo(d, Path(p, obj)));

        internal readonly static dynamic PathSatisfies = Curry3<Delegate, IList<string>, object, bool>((pred, propPath, obj) => propPath.Count > 0 && (bool)pred.DynamicInvoke(Path(propPath, obj)));

        internal readonly static dynamic Pick = Curry2<IList<string>, object, IDictionary<string, object>>((names, obj) => PickIntrenal(names, obj));

        internal readonly static dynamic PickAll = Curry2<IList<string>, object, IDictionary<string, object>>((names, obj) => PickIntrenal(names, obj, true));

        internal readonly static dynamic PickBy = Curry2<Delegate, object, IDictionary<string, object>>((pred, obj) => {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var pair in obj.ToMemberDictionary()) {
                if ((bool)pred.DynamicInvoke(pair.Value, pair.Key, obj)) {
                    result[pair.Key] = pair.Value;
                }
            }

            return result;
        });

        internal readonly static dynamic Prepend = Curry2<object, IList, IList>((el, list) => new[] { el }.Concat(list));

        internal readonly static dynamic Prop = Curry2<dynamic, object, object>((p, obj) => Reflection.Member(obj, p));

        internal readonly static dynamic PropIs = Curry3<Type, dynamic, object, bool>((type, name, obj) => Is(type, Reflection.Member(obj, name)));

        internal readonly static dynamic PropOr = Curry3<object, dynamic, object, object>((val, p, obj) => obj.IsNotNull() ? Reflection.Member(obj, p) ?? val : val);

        internal readonly static dynamic PropSatisfies = Curry3<Delegate, dynamic, object, bool>((pred, name, obj) => (bool)pred.Invoke((object)Reflection.Member(obj, name)));

        internal readonly static dynamic Props = Curry2<IEnumerable<dynamic>, object, object>((ps, obj) => {
            var result = new List<object>();

            foreach (var prop in ps) {
                object member;

                if (Reflection.TryGetMember(prop, obj, out member)) {
                    result.Add(member);
                }
            }

            return result;
        });

        internal readonly static dynamic Range = Curry2<int, int, IList>((from, to) => {
            var start = 0;
            var count = 0;

            if (from < to) {
                start = from;
                count = to - from;
            }

            return Enumerable.Range(start, count).ToArray();
        });

        internal readonly static dynamic ReduceRight = Curry3<DynamicDelegate, object, IList, object>((fn, acc, list) => {
            var idx = list.Count - 1;
            dynamic dynamicFn = fn;

            while (idx >= 0) {
                acc = dynamicFn(acc, list[idx]);
                idx -= 1;
            }

            return acc;
        });

        internal readonly static dynamic Reduced = Curry1<object, IReduced>(ReducedInternal);

        internal readonly static dynamic Remove = Curry3<int, int, IList, IList>((start, count, list) => {
            return Slice(list, 0, Math.Min(start, list.Count)).Concat(Slice(list, Math.Min(list.Count, start + count)));
        });

        internal readonly static dynamic Replace = Curry3<Regex, string, string, string>((regex, replacement, str) => regex.Replace(str, replacement));

        internal readonly static dynamic Reverse = Curry1<IEnumerable, IEnumerable>(enumerable => {
            var list = enumerable.CreateNewList();
            var enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext()) {
                list.Insert(0, enumerator.Current);
            }

            return list.ToArray<Array>();
        });

        internal readonly static dynamic Scan = Curry3<Delegate, object, IList, IList>((fn, acc, list) => {
            var idx = 0;
            var len = list.Count;
            var result = new List<object>() { acc };

            while (idx < len) {
                acc = fn.DynamicInvoke(acc, list[idx]);
                result.Insert(idx + 1, acc);
                idx += 1;
            }

            return result;
        });

        internal readonly static dynamic Set = Curry3<Func<Func<object, Functor>>, object, object, object>((lens, v, x) => {
            return Over(lens, Always(v), x);
        });

        internal readonly static dynamic Slice = Curry3(CheckForMethod3("Slice", new Func<int, int, IList, IList>((fromIndex, toIndex, list) => list.Slice(fromIndex, toIndex))));

        internal readonly static dynamic Sort = Curry2<Delegate, IList, IList>((comparator, list) => {
            return SortInternal(list, comparator.ToComparer((x, y) => (int)comparator.DynamicInvoke(x, y)));
        });

        internal readonly static dynamic SortBy = Curry2<Delegate, IList, IList>((fn, list) => {
            return SortInternal(list, fn.ToComparer((x, y) => {
                var xx = (dynamic)fn.DynamicInvoke(x.ToArgumentsArray());
                var yy = (dynamic)fn.DynamicInvoke(y.ToArgumentsArray());

                return xx < yy ? -1 : xx > yy ? 1 : 0;
            }));
        });

        internal readonly static dynamic SplitEvery = Curry2<int, IList, IList>((n, list) => {
            if (n <= 0) {
                throw new ArgumentOutOfRangeException(nameof(n), "First argument to splitEvery must be a positive integer");
            }

            var idx = 0;
            var result = list.CreateNewList(type: list.GetType());

            while (idx < list.Count) {
                result.Add(Slice(idx, idx += n, list));
            }

            return result;
        });

        internal readonly static dynamic SplitWhen = Curry2<Delegate, IList, IList>((pred, list) => {
            var idx = 0;
            var len = list.Count;
            var prefix = list.CreateNewList();
            var result = list.CreateNewList(type: prefix.GetType());

            if (list.IsArray()) {
                list = list.CreateNewList(list);
            }

            while (idx < len && !(bool)pred.DynamicInvoke(list[idx])) {
                prefix.Add(list[idx]);
                idx += 1;
            }

            result.Add(prefix);
            result.Add(list.Slice(idx));

            return result;
        });

        internal readonly static dynamic Subtract = Curry2<double, double, double>((arg1, arg2) => arg1 - arg2);

        internal readonly static dynamic Tail = CheckForMethod1("Tail", Slice(1, int.MaxValue));

        internal readonly static dynamic Take = Curry2(Dispatchable2("Take", XTake, new Func<int, IList, IList>((n, xs) => {
            return Slice(0, n < 0 ? int.MaxValue : n, xs);
        })));

        internal readonly static dynamic TakeLastWhile = Curry2<Delegate, IList, IList>((fn, list) => {
            return TakeWhileInternal(list.Count - 1, -1, idx => idx >= 0, fn, list, idx => idx + 1, idx => int.MaxValue);
        });

        internal readonly static dynamic TakeWhile = Curry2(Dispatchable2("TakeWhile", XTakeWhile, new Func<Delegate, IList, IList>((fn, list) => {
            return TakeWhileInternal(0, 1, idx => idx < list.Count, fn, list, idx => 0, idx => idx);
        })));

        internal readonly static dynamic Tap = Curry2<Delegate, object, object>((fn, value) => {
            fn.DynamicInvoke(value);
            return value;
        });

        internal readonly static dynamic Times = Curry2<Delegate, int, IList>((fn, n) => {
            if (n < 0) {
                throw new ArgumentOutOfRangeException("n must be a non-negative number");
            }

            var idx = 0;
            var list = new object[n];

            while (idx < n) {
                list[idx] = fn.Invoke(idx);
                idx += 1;
            }

            return list.ToArray<Array>();
        });

        internal readonly static dynamic ToPairs = Curry1<object, object[]>((obj) => {
            return obj.ToMemberDictionary()
                      .Select(prop => new[] { prop.Key, prop.Value })
                      .ToArray();
        });

        internal readonly static dynamic Transpose = Curry1<IList, IList>((outerlist) => {
            var i = 0;
            var result = new List<IList>();

            while (i < outerlist.Count) {
                var j = 0;
                IList resultInnnerList = null;
                var innerlist = (IList)outerlist[i];

                while (j < innerlist.Count) {
                    if (result.Count > j) {
                        resultInnnerList = result[j];
                    }
                    else {
                        result.Add(resultInnnerList = innerlist.CreateNewList());
                    }

                    resultInnnerList.Add(innerlist[j]);
                    j += 1;
                }

                i += 1;
            }

            return outerlist.CreateNewArray(result.Select(list => list.ToArray<Array>()).ToArray());
        });

        internal readonly static dynamic Trim = Curry1<string, string>(str => str.Trim());

        internal readonly static dynamic TryCatch = Curry2<DynamicDelegate, DynamicDelegate, DynamicDelegate>((tryer, catcher) => {
            return Delegate(arguments => {
                try {
                    return ((dynamic)tryer)(arguments);
                }
                catch (Exception e) {
                    return ((dynamic)catcher)(new object[] { e }.Concat(arguments));
                }
            });
        });

        internal readonly static dynamic Type = Curry1<object, string>(obj => {
            if (obj.IsNotNull()) {
                var type = obj.GetType();

                return type.IsAnonymousType() ? "anonymous" : type.Name;
            }

            return "null";
        });

        internal readonly static dynamic Unapply = Curry1<DynamicDelegate, object>(fn => {
            return Delegate(arguments => ((dynamic)fn)(arguments.Slice()));
        });

        internal readonly static dynamic Unary = Curry1<DynamicDelegate, DynamicDelegate>(fn => NAry(1, fn));

        internal readonly static dynamic UncurryN = Curry2<int, DynamicDelegate, object>((depth, fn) => {
            return CurryN(depth, Delegate(arguments => {
                int endIdx;
                var idx = 0;
                object value = fn;
                var currentDepth = 1;

                while (currentDepth <= depth && value.IsFunction()) {
                    dynamic @delegate = value as DynamicDelegate;
                    var length = @delegate.Length;

                    endIdx = currentDepth == depth ? arguments.Length : idx + length;
                    value = @delegate(arguments.Slice(idx, endIdx));
                    currentDepth += 1;
                    idx = endIdx;
                }

                return value;
            }));
        });

        internal readonly static dynamic Unfold = Curry2<DynamicDelegate, int, IList>((fn, seed) => {
            IList list;
            dynamic dynamicDelegate = fn;
            var pair = dynamicDelegate(seed);
            IList result = new List<int>();

            while ((list = pair as IList).IsNotNull()) {
                result.Add(list[0]);
                pair = dynamicDelegate(list[1]);
            }

            return result.ToArray<Array>();
        });

        internal readonly static dynamic UniqWith = Curry2<DynamicDelegate, IList, IList>((pred, list) => {
            dynamic predicate = pred;
            var result = list.CreateNewList();
            var prediacte = new Func<object, object, bool>((a, b) => (bool)predicate(a, b));

            foreach (var item in list) {
                if (!ContainsWith(prediacte, item, result)) {
                    result.Add(item);
                }
            }

            if (list.IsArray()) {
                return result.ToArray<Array>();
            }

            return result.ToList<IList>();
        });

        internal readonly static dynamic Unless = Curry3<Delegate, Delegate, object, object>((pred, whenFalseFn, x) => {
            return (bool)pred.DynamicInvoke(x) ? x : whenFalseFn.DynamicInvoke(x);
        });

        internal readonly static dynamic Until = Curry3<Delegate, Delegate, object, object>((pred, fn, init) => {
            var val = init;

            while (!(bool)pred.Invoke(val)) {
                val = fn.Invoke(val);
            }

            return val;
        });

        internal readonly static dynamic Update = Curry3<int, object, IList, IList>((idx, x, list) => Adjust(Always(x), idx, list));

        internal readonly static dynamic UseWith = Curry2<DynamicDelegate, IList, object>((fn, transformers) => {
            return CurryN(transformers.Count, Delegate(arguments => {
                dynamic dynamicFn = fn;
                var args = transformers.Select((item, i) => {
                    dynamic transformer = Delegate(item);

                    return transformer(arguments[i]);
                });

                return dynamicFn(args.Concat((object[])arguments.Slice(transformers.Count)));
            }));
        });

        internal readonly static dynamic Values = Curry1<object, IList>(obj => {
            Type firstType = null;
            bool sameTypeForAll = true;
            var pairs = obj.ToMemberDictionary();
            IList vals = new List<object>();

            pairs.ForEach((pair, i) => {
                var value = pair.Value;
                var type = value.GetType();

                if (i == 0) {
                    firstType = type;
                }
                else {
                    sameTypeForAll &= type.Equals(firstType);
                }

                vals.Add(value);
            });

            if (sameTypeForAll) {
                vals = vals.CreateNewList(vals, firstType);
            }

            return vals.ToArray<Array>();
        });

        internal readonly static dynamic View = Curry2<Func<Func<object, Functor>, Func<object, Functor>>, object, object>((lens, x) => lens(Const)(x).Value);

        internal readonly static dynamic When = Curry3<Delegate, Delegate, object, object>((pred, whenTrueFn, x) => {
            return (bool)pred.Invoke(x) ? whenTrueFn.Invoke(x) : x;
        });

        internal readonly static dynamic Where = Curry2<IDictionary<string, object>, object, bool>((spec, testObj) => {
            foreach (var pair in spec.ToMemberDictionary()) {
                var testObjMember = testObj.Member(pair.Key);
                var @delegate = (Delegate)pair.Value;

                if (!(bool)@delegate.Invoke(testObjMember)) {
                    return false;
                }
            }

            return true;
        });

        internal readonly static dynamic XProd = Curry2<IList, IList, IList>((a, b) => {
            var idx = 0;
            var ilen = a.Count;
            var jlen = b.Count;
            var result = new List<IList[]>();

            while (idx < ilen) {
                result.Add(ZipInternal(a, b, jlen, (list, _) => list[idx]));
                idx += 1;
            }

            return result.SelectMany(list => list.AsEnumerable()).ToList();
        });

        internal readonly static dynamic Zip = Curry2<IList, IList, IList>((a, b) => {
            return ZipInternal(a, b, Math.Min(a.Count, b.Count));
        });

        internal readonly static dynamic ZipObj = Curry2<IList<string>, IList, IDictionary<string, object>>((keys, values) => {
            var idx = 0;
            var len = Math.Min(keys.Count, values.Count);
            IDictionary<string, object> result = new ExpandoObject();

            while (idx < len) {
                result[keys[idx]] = values[idx];
                idx += 1;
            }

            return result;
        });

        internal readonly static dynamic ZipWith = Curry3<Delegate, IList, IList, IList>((fn, a, b) => {
            var idx = 0;
            Type type = null;
            var allSameType = true;
            var len = Math.Min(a.Count, b.Count);
            var rv = new List<object>();

            while (idx < len) {
                var value = fn.Invoke(a[idx], b[idx]);
                var typeofValue = value.GetType();

                if (type.IsNotNull()) {
                    allSameType &= typeofValue.Equals(type.GetType());
                }
                else {
                    type = typeofValue;
                }

                rv.Add(value);
                idx += 1;
            }

            if (allSameType) {
                return rv.ToArray<Array>(type);
            }

            return rv.ToArray();
        });

        internal readonly static dynamic F = Always(false);

        internal readonly static dynamic T = Always(true);

        internal readonly static dynamic AddIndex = Curry1<DynamicDelegate, DynamicDelegate>(fn => {
            return CurryN(fn.Length, Delegate((object[] arguments) => {
                var idx = 0;
                var origFn = Delegate(arguments[0]);
                var args = (object[])arguments.Slice();
                var list = arguments[arguments.Length - 1];

                args[0] = Delegate((object[] innerArguments) => {
                    var result = Reflection.DynamicInvoke(origFn, (object[])innerArguments.Concat(new[] { idx, list }));

                    idx += 1;

                    return result;
                });

                return Reflection.DynamicInvoke(fn, args);
            }));
        });

        internal readonly static dynamic Binary = Curry1<DynamicDelegate, DynamicDelegate>(fn => NAry(2, fn));

        internal readonly static dynamic Clone = Curry1<object, object>(DeepCloner.Clone);

        internal readonly static dynamic Curry = Curry1<dynamic, DynamicDelegate>(fn => CurryN(Reflection.FunctionArity(fn), fn));

        internal readonly static dynamic Drop = Curry2(Dispatchable2("Drop", XDrop, new Func<int, IEnumerable, IEnumerable>((n, xs) => {
            string value = xs as string;

            if (value != null) {
                return value.Substring(Math.Min(n, value.Length));
            }

            return Slice(Math.Max(0, n), int.MaxValue, xs);
        })));

        internal readonly static dynamic DropLast = Curry2(Dispatchable2("DropLast", XDropLast, new Func<int, IEnumerable, IEnumerable>(DropLastInternal)));

        internal readonly static dynamic DropLastWhile = Curry2(Dispatchable2("DropLastWhile", XDropLastWhile, new Func<DynamicDelegate, IList, IEnumerable>(DropLastWhileInternal)));

        internal new readonly static dynamic Equals = Curry2<object, object, bool>((a, b) => EqualsInternal(a, b, new ArrayList(), new ArrayList()));

        internal readonly static dynamic Filter = Curry2(Dispatchable2("Filter", XFilter, new Func<DynamicDelegate, object, object>((pred, filterable) => {
            dynamic dynamicPred = pred;

            return !filterable.IsEnumerable() ? ReduceInternal(Delegate(new Func<IDictionary<string, object>, string, IDictionary<string, object>>((acc, key) => {
                var value = filterable.Member(key);

                if (Reflection.DynamicInvoke(dynamicPred, new object[] { value })) {
                    acc[key] = value;
                }

                return acc;
            })), new ExpandoObject(), filterable.Keys()) : FilterInternal(item => dynamicPred(item), (IList)filterable);
        })));

        internal readonly static dynamic Flatten = Curry1(MakeFlat(true));

        internal readonly static dynamic Flip = Curry1<dynamic, dynamic>(fn => {
            return Curry(Delegate((object[] arguments) => {
                var args = (object[])arguments.Slice();
                var dynamicFn = Delegate(fn);
                if (args.Length > 1) {
                    var a = args[0];
                    var b = args[1];

                    args[0] = b;
                    args[1] = a;
                }

                return Reflection.DynamicInvoke(dynamicFn, args);
            }, 2));
        });

        internal readonly static dynamic Head = Nth(0);

        internal readonly static dynamic Init = Curry1<IList, IList>(list => list.Slice(0, list.Count - 1));

        internal readonly static dynamic IntersectionWith = Curry3<Delegate, IList, IList, IList>((pred, list1, list2) => {
            IList lookupList;
            IList filteredList;
            var results = new ArrayList();
            Func<object, object, bool> containsPredicate = (a, b) => (bool)pred.Invoke(a, b);

            if (list1.Count > list2.Count) {
                lookupList = list1;
                filteredList = list2;
            }
            else {
                lookupList = list2;
                filteredList = list1;
            }

            foreach (var item in filteredList) {
                if (ContainsWith(containsPredicate, item, lookupList)) {
                    results.Add(item);
                }
            }

            return UniqWith(pred, results);
        });

        internal readonly static dynamic Into = Curry3<object, dynamic, object, object>((acc, xf, list) => {
            var transformer = acc as ITransformer;

            if (transformer.IsNotNull()) {
                return ReduceInternal(xf(transformer), transformer.Init(), list);
            }

            return ReduceInternal(Reflection.DynamicInvoke(xf, new[] { StepCat(acc) }), ShallowCloner.Clone(acc), list);
        });

        internal readonly static dynamic Invert = Curry1<object, IDictionary<string, object>>(obj => {
            var inverted = new Dictionary<string, ArrayList>();
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var prop in obj.Keys()) {
                var val = obj.Member(prop).ToString();
                var list = inverted.ContainsKey(val) ? inverted[val] : inverted[val] = new ArrayList();

                list.Add(prop);
            }

            inverted.Keys.ForEach(key => {
                result[key] = inverted[key].ToArray<Array>();
            });

            return result;
        });

        internal readonly static dynamic InvertObj = Curry1<object, IDictionary<string, object>>(obj => {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var prop in obj.Keys()) {
                var val = obj.Member(prop).ToString();

                result[val] = prop;
            }

            return result;
        });

        internal readonly static dynamic IsEmpty = Curry1<object, bool>(x => x.IsNotNull() && Equals(x, Empty(x)));

        internal readonly static dynamic Last = Nth(-1);

        internal readonly static dynamic LastIndexOf = Curry2<object, IList, int>((target, xs) => {
            if (!xs.IsArray() && xs.HasMemberWhere("LastIndexOf", m => m.IsFunction())) {
                return ((dynamic)xs).LastIndexOf(target);
            }
            else {
                var idx = xs.Count - 1;

                while (idx >= 0) {
                    if (Equals(xs[idx], target)) {
                        return idx;
                    }

                    idx -= 1;
                }

                return -1;
            }
        });

        internal readonly static dynamic Map = Curry2(Dispatchable2("Map", XMap, new Func<dynamic, object, object>((fn, functor) => {
            IList listFunctor = null;
            var functionFunctor = functor as DynamicDelegate;

            if (functionFunctor.IsNotNull()) {
                return CurryN(functionFunctor.Length, Delegate(((object[] arguments) => {
                    dynamic dynamicFunctor = functionFunctor;

                    return fn(Reflection.DynamicInvoke(functionFunctor, arguments));
                })));
            }

            listFunctor = functor as IList;

            if (listFunctor.IsNull()) {
                return ReduceInternal(Delegate(new Func<IDictionary<string, object>, string, IDictionary<string, object>>((acc, key) => {
                    acc[key] = fn.Invoke(functor.Member(key));
                    return acc;
                })), new ExpandoObject(), functor.Keys());
            }

            return MapInternal(fn, listFunctor);
        })));

        internal readonly static dynamic MapObjIndexed = Curry2<Delegate, object, object>((fn, obj) => {
            return ReduceInternal(Delegate(new Func<IDictionary<string, object>, string, object>((acc, key) => {
                acc[key] = fn.Invoke(obj.Member(key), key, obj);
                return acc;
            })), new ExpandoObject(), obj.Keys());
        });

        internal readonly static dynamic MergeWith = Curry3<Delegate, object, object, object>((fn, l, r) => {
            return MergeWithKey(new Func<object, object, object, object>((_, _l, _r) => fn.Invoke(_l, _r)), l, r);
        });

        internal readonly static dynamic Partial = CreatePartialApplicator(new Func<IList, IList, IList>(Core.Concat));

        internal readonly static dynamic PartialRight = CreatePartialApplicator(Flip(new Func<IList, IList, IList>(Core.Concat)));

        internal readonly static dynamic PathEq = Curry3<IList<string>, object, object, bool>((_path, val, obj) => Equals(Path(_path, obj), val));

        internal readonly static dynamic Pluck = Curry2<object, IList, IList>((p, list) => Map(Prop(p), list));

        internal readonly static dynamic Project = UseWith(Delegate(new Func<DynamicDelegate, IList, object>(MapInternal)), new object[] { PickAll, Identity });

        internal readonly static dynamic PropEq = Curry3<string, object, object, bool>((name, val, obj) => Equals(val, obj.Member(name)));

        internal readonly static dynamic Reduce = Curry3<object, object, object, object>(ReduceInternal);

        internal readonly static dynamic ReduceBy = CurryN(4, Dispatchable4("ReduceBy", XReduceBy, Delegate(new Func<dynamic, object, dynamic, IList, object>((valueFn, valueAcc, keyFn, list) => {
            return ReduceInternal(Delegate(new Func<IDictionary<string, object>, object, object>((acc, elt) => {
                var key = keyFn(elt).ToString();

                acc[key] = valueFn(acc.ContainsKey(key) ? acc[key] : valueAcc, elt);

                return acc;
            })), new ExpandoObject(), list);
        }))));

        internal readonly static dynamic ReduceWhile = CurryN(4, new Func<Delegate, Delegate, object, IList, object>((pred, fn, a, list) => {
            return ReduceInternal(new Func<object, object, object>((acc, x) => {
                return (bool)pred.Invoke(acc, x) ? fn.Invoke(acc, x) : ReducedInternal(acc);
            }), a, list);
        }));

        internal readonly static dynamic Reject = Curry2<DynamicDelegate, object, object>((pred, filterable) => Filter(ComplementInternal(pred), filterable));

        internal readonly static dynamic Repeat = Curry2<object, int, IList>((value, n) => Times(Always(value), n));

        internal readonly static dynamic Sum = Reduce(Add, 0);

        internal readonly static dynamic TakeLast = Curry2<int, IList, IList>((n, xs) => Drop(n >= 0 ? xs.Count - n : 0, xs));

        internal readonly static dynamic Transduce = CurryN(4, new Func<Delegate, object, object, object, object>((xf, fn, acc, list) => {
            return ReduceInternal(xf.Invoke(fn.IsFunction() ? new XWrap((DynamicDelegate)fn) : fn), acc, list);
        }));

        internal readonly static dynamic UnionWith = Curry3<Delegate, IList, IList, IList>((pred, list1, list2) => UniqWith(pred, list1.Concat(list2)));

        internal readonly static dynamic WhereEq = Curry2<object, object, bool>((spec, testObj) => Where(Map(Equals, spec), testObj));

        internal readonly static dynamic AllPass = Curry1<IList, DynamicDelegate>(preds => CurryN(Reduce(Max, 0, Pluck("Length", preds)), AnyOrAllPass(preds, false)));

        internal readonly static dynamic AnyPass = Curry1<IList, DynamicDelegate>(preds => CurryN(Reduce(Max, 0, Pluck("Length", preds)), AnyOrAllPass(preds, true)));

        internal readonly static dynamic Ap = Curry2<object, object, object>((applicative, fn) => {
            dynamic applicativeFn = null;
            var ap = applicative.Member("Ap");

            if (ap.IsNotNull()) {
                applicativeFn = Delegate(ap);

                return applicativeFn(fn);
            }

            if (applicative.IsFunction()) {
                dynamic @delegete = Delegate(fn);

                applicativeFn = Delegate(applicative);

                return Delegate(new Func<object, object>(x => {
                    var result = Delegate(applicativeFn(x));

                    return result(@delegete(x));
                }));
            }

            return ReduceInternal(Delegate(new Func<IList, dynamic, IList>((acc, f) => {
                if (fn.IsFunction()) {
                    fn = Delegate(fn);
                }

                return acc.Concat((IList)Map(Delegate(f), fn));
            })), new object[0], applicative);
        });

        internal readonly static dynamic ApplySpec = Curry1<object, object>(ApplySpecInternal);

        internal readonly static dynamic Call = CurryParams(Delegate(arguments => {
            dynamic fn = arguments[0];

            return Reflection.DynamicInvoke(Delegate(fn), (object[])arguments[1]);
        }));

        internal readonly static dynamic Chain = Curry2(Dispatchable2("Chain", XChain, new Func<DynamicDelegate, object, object>((fn, monad) => {
            if (monad.IsFunction()) {
                dynamic monadFn = monad;

                return Delegate((object[] arguments) => {
                    dynamic dynamicFn = fn;
                    var dynamicResult = Reflection.DynamicInvoke(dynamicFn, arguments);
                    var resultFn = Delegate(Reflection.DynamicInvoke(monadFn, new[] { dynamicResult }));

                    return Reflection.DynamicInvoke(resultFn, arguments);
                });
            }

            return MakeFlat(false)(Map(fn, monad));
        })));

        internal readonly static dynamic Cond = Curry1<IList, object>(pairs => {
            var dynamicPairs = pairs.Cast<IList>().Select(pair => new[] { Delegate(pair[0]), Delegate(pair[1]) }).ToList();
            int arity = Reduce(Max, 0, Map(Delegate(pair => ((DynamicDelegate)pair[0]).Length), dynamicPairs));

            return Arity(arity, Delegate((object[] arguments) => {
                foreach (IList pair in dynamicPairs) {
                    dynamic @delegate = pair[0];

                    if (Reflection.DynamicInvoke(@delegate, arguments)) {
                        return Reflection.DynamicInvoke(pair[1], arguments);
                    }
                }

                return null;
            }));
        });

        internal readonly static dynamic ConstructN = Curry2<int, dynamic, object>((n, Fn) => {
            if (n > 10) {
                throw new ArgumentOutOfRangeException("Constructor with greater than ten arguments");
            }

            if (n == 0) {
                return Delegate(() => Reflection.DynamicInvoke(Fn));
            }

            return CurryN(n, Delegate(arguments => {
                return Reflection.DynamicInvoke(Fn, (object[])arguments.Slice(0, n));
            }));
        });

        internal readonly static dynamic Converge = Curry2<dynamic, IList, object>((after, fns) => {
            var dynamicFns = fns.Select(Delegate).ToList();

            return CurryN(Reduce(Max, 0, Pluck("Length", fns)), Delegate(arguments => {
                return Reflection.DynamicInvoke(after, MapInternal(Delegate(fn => Reflection.DynamicInvoke(fn, arguments)), dynamicFns));
            }));
        });

        internal readonly static dynamic CountBy = ReduceBy(Delegate(new Func<int, object, int>((acc, elem) => acc + 1)), 0);

        internal readonly static dynamic DropRepeatsWith = Curry2(Dispatchable2("DropRepeatsWith", XDropRepeatsWith, new Func<DynamicDelegate, IList, IList>((pred, list) => {
            var idx = 1;
            var len = list.Count;
            var result = new List<object>();

            if (len != 0) {
                result.Add(list[0]);

                while (idx < len) {
                    if (!(bool)Reflection.DynamicInvoke(pred, new[] { (object)Last(result), list[idx] })) {
                        result.Add(list[idx]);
                    }

                    idx += 1;
                }
            }

            return result.ToArray<Array>();
        })));

        internal readonly static dynamic EqBy = Curry3<DynamicDelegate, object, object, bool>((f, x, y) => R.Equals(((dynamic)f)(x), ((dynamic)f)(y)));

        internal readonly static dynamic EqProps = Curry3<string, object, object, bool>((prop, obj1, obj2) => R.Equals(obj1.Member(prop), obj2.Member(prop)));

        internal readonly static dynamic GroupBy = Curry2(CheckForMethod2("GroupBy", ReduceBy(new Func<object, object, IList>((acc, item) => {
            var result = acc as IList;

            if (result.IsNull()) {
                result = new List<object>();
            }

            result.Add(item);

            return result;
        }), R.Null)));

        internal readonly static dynamic IndexBy = ReduceBy(new Func<object, object, object>((acc, elem) => elem), R.Null);

        internal readonly static dynamic IndexOf = Curry2<object, object, int>((target, xs) => {
            if (xs.IsList()) {
                return ((IList)xs).IndexOf(target);
            }

            if (xs.HasMemberWhere("IndexOf", t => t.IsFunction())) {
                return ((dynamic)xs).IndexOf(target);
            }

            return -1;
        });

        internal readonly static dynamic Juxt = Curry1<IList, DynamicDelegate>(fns => Converge(ArrayOf, fns));

        internal readonly static dynamic Lens = Curry2<Delegate, Delegate, Delegate>((getter, setter) => {
            return new Func<Func<object, Functor>, Func<object, Functor>>(toFunctorFn => {
                return new Func<object, Functor>(target => {
                    return Map(new Func<object, object>(focus => setter.Invoke(focus, target)), toFunctorFn(getter.Invoke(target)));
                });
            });
        });

        internal readonly static dynamic LensIndex = Curry1<int, Delegate>(n => Lens(Nth(n), Update(n)));

        internal readonly static dynamic LensPath = Curry1<IList<string>, Delegate>(p => Lens(Path(p), AssocPath(p)));

        internal readonly static dynamic LensProp = Curry1<string, Delegate>(k => Lens(Prop(k), Assoc(k)));

        internal readonly static dynamic LiftN = Curry2<int, DynamicDelegate, DynamicDelegate>((arity, fn) => {
            return CurryN(arity, Delegate(arguments => ReduceInternal(Ap, Map(CurryN(arity, fn), arguments[0]), arguments.Slice(1))));
        });

        internal readonly static dynamic Mean = Curry1<IList<double>, double>(list => Sum(list) / list.Count);

        internal readonly static dynamic Median = Curry1<IList, double>(list => {
            var len = list.Count;

            if (len == 0) {
                return 0;
            }

            var width = 2 - len % 2;
            var idx = (len - width) / 2;

            return Mean(list.Slice().Sort<double>((a, b) => {
                return a < b ? -1 : a > b ? 1 : 0;
            }).Slice(idx, idx + width));
        });

        internal readonly static dynamic Partition = Juxt(new object[] { Filter, Reject });

        internal readonly static dynamic Pipe = PipeFactory(PipeInternal, "Pipe");

        internal readonly static dynamic PipeP = PipeFactory(PipePInternal, "Pipe");

        internal readonly static dynamic Product = Reduce(Multiply, 1);

        internal readonly static dynamic Sequence = Curry2<DynamicDelegate, IList, object>((of, traversable) => {
            if (!traversable.IsArray() && traversable.HasMemberWhere("Sequence", m => m.IsFunction())) {
                return ((dynamic)traversable).Sequence(of);
            }

            return ReduceRight(Delegate((acc, x) => Ap(Map(Prepend, x), acc)), Of(new object[0]), traversable);
        });

        internal readonly static dynamic Traverse = Curry3<DynamicDelegate, DynamicDelegate, object, IList>((of, f, traversable) => Sequence(Of, Map(f, traversable)));

        internal readonly static dynamic Unnest = Chain(Delegate(IdentityInternal));

        internal readonly static dynamic Compose = ComposeFactory(Pipe, "Compose");

        internal readonly static dynamic ComposeK = Delegate((object[] arguments) => {
            arguments = arguments.Select(arg => {
                if (arg.IsFunction()) {
                    return Delegate(arg);
                }

                return arg;
            }).ToArray();

            return Compose(Prepend(Identity, Map(Chain, arguments)));
        });

        internal readonly static dynamic ComposeP = ComposeFactory(PipeP, "ComposeP");

        internal readonly static dynamic Construct = Curry1<DynamicDelegate, object>(Fn => ConstructN(Fn.Length, Fn));

        internal readonly static dynamic Contains = Curry2<object, object, bool>(ContainsInternal);

        internal readonly static dynamic Difference = Curry2<IList, IList, IList>((first, second) => {
            var result = new ArrayList();

            foreach (var item in first) {
                if (!second.Contains(item) && !result.Contains(item)) {
                    result.Add(item);
                }
            }

            return result.ToArray<Array>();
        });

        internal readonly static dynamic DropRepeats = Curry1<object, dynamic>(Dispatchable1("DropRepeats", XDropRepeatsWith(Equals), DropRepeatsWith(Equals)));

        internal readonly static dynamic Lift = Curry1<DynamicDelegate, DynamicDelegate>(fn => LiftN(fn.Arity(), fn));

        internal readonly static dynamic Omit = Curry2<IList<string>, object, IDictionary<string, object>>((names, obj) => {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var pair in obj.ToMemberDictionary()) {
                if (!names.Contains(pair.Key)) {
                    result[pair.Key] = pair.Value;
                }
            }

            return result;
        });

        internal readonly static dynamic PipeK = Delegate((object[] arguments) => Reflection.DynamicInvoke(ComposeK, Reverse(arguments)));

        internal new readonly static dynamic ToString = Curry1<object, string>(ToStringInternal);

        internal readonly static dynamic Without = Curry2<IList, IList, IList>((xs, list) => Reject(Flip(new Func<object, object, bool>(ContainsInternal))(xs), list));

        internal static dynamic Both = Curry2<object, object, object>((f, g) => BothOrEither(Delegate(f), Delegate(g), new Func<Func<bool>, Func<bool>, bool>((a, b) => a() && b()), And));

        internal readonly static dynamic Complement = Lift(Not);

        internal readonly static dynamic Concat = Curry2<object, object, IEnumerable>((a, b) => {
            Delegate concat = null;
            IList firstList = null;
            string firstString = null;

            if (a.IsNull()) {
                throw new ArgumentNullException(nameof(a));
            }

            if (b.IsNull()) {
                throw new ArgumentNullException(nameof(b));
            }

            firstString = a as string;

            if (firstString.IsNotNull()) {
                var secondString = b as string;

                if (secondString.IsNotNull()) {
                    return string.Concat(firstString, secondString);
                }

                throw new ArgumentException($"{b.GetType().Name} is not a string");
            }

            firstList = a as IList;

            if (firstList.IsNotNull()) {
                var secondList = b as IList;

                if (secondList.IsNotNull()) {
                    return firstList.Concat(secondList);
                }

                throw new ArgumentException($"{b.GetType().Name} is not a list");
            }

            concat = a.Member("Concat") as Delegate;

            if (concat.IsNotNull()) {
                return (IEnumerable)concat.DynamicInvoke(b);
            }

            throw new ArgumentException($"{a.GetType().Name} is not a list or string or does not have a method named \"Concat\"");
        });

        internal static dynamic Either = Curry2<object, object, object>((f, g) => BothOrEither(Delegate(f), Delegate(g), new Func<Func<bool>, Func<bool>, bool>((a, b) => a() || b()), Or));

        internal static dynamic Invoker = Curry2<int, string, object>((arity, method) => {
            return CurryN(arity + 1, Delegate((object[] arguments) => {
                var target = arguments[arity];

                if (target.IsNotNull()) {
                    var methodMember = target.Member(method, arity);
                    var @delegate = methodMember as Delegate;

                    if (methodMember.IsNotNull()) {
                        return @delegate.DynamicInvoke((object[])arguments.Slice(0, arity));
                    }
                }

                throw new MissingMethodException($"{nameof(target)} does not have a method named '{method}'");
            }));
        });

        internal static dynamic Join = Curry2<string, IList, string>((separator, xs) => {
            return string.Join(separator, xs.Select<string>(item => item.ToString()));
        });

        internal static dynamic Memoize = Curry1<DynamicDelegate, DynamicDelegate>(fn => {
            dynamic dynamicFn = fn;
            var cache = new Dictionary<string, object>();
            var a = "A".Split();

            return Arity(fn.Length, Delegate((object[] arguments) => {
                var key = (string)ToString(arguments);

                return cache.GetOrAdd<string, object>(key, () => {
                    return Reflection.DynamicInvoke(dynamicFn, arguments);
                });
            }));

        });

        internal static dynamic Split = Invoker(1, "Split");

        internal static dynamic SymmetricDifference = Curry2<IList, IList, IList>((list1, list2) => {
            return Concat(Difference(list1, list2), Difference(list2, list1));
        });

        internal static dynamic SymmetricDifferenceWith = Curry3<DynamicDelegate, IList, IList, IList>((pred, list1, list2) => {
            return Concat(DifferenceWith(pred, list1, list2), DifferenceWith(pred, list2, list1));
        });

        internal static dynamic Test = Curry2<Regex, string, bool>((pattern, str) => pattern.IsMatch(str));

        internal static dynamic ToLower = Invoker(0, "ToLower");

        internal static dynamic ToUpper = Invoker(0, "ToUpper");

        internal static dynamic UniqBy = Curry2<DynamicDelegate, IList, IList>((fn, list) => {
            dynamic dynamicFn = fn;
            var result = new ArrayList();
            var set = new HashSet<object>();

            foreach (var item in list) {
                var appliedItem = dynamicFn(item);

                if (set.Add(appliedItem)) {
                    result.Add(item);
                }
            }

            return result.ToArray<Array>();
        });

        internal static dynamic Uniq = UniqBy(Identity);

        internal static dynamic Intersection = Curry2<IList, IList, IList>((list1, list2) => {
            dynamic flipped;
            IList lookupList;
            IList filteredList;

            if (list1.Count > list2.Count) {
                lookupList = list1;
                filteredList = list2;
            }
            else {
                lookupList = list2;
                filteredList = list1;
            }

            flipped = Flip(new Func<object, object, bool>(ContainsInternal))(lookupList);

            return Uniq(FilterInternal(a => flipped(a), filteredList));
        });

        internal static dynamic Union = Curry2(Compose(Uniq, new Func<IList, IList, IList>(Core.ConcatInternal)));
    }
}
