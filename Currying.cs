using System;
using Ramda.NET;
using System.Linq;
using Sys = System;
using System.Dynamic;
using System.Collections;
using static Ramda.NET.R;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Core = Ramda.NET.IEnumerableExtensions;
using Reflection = Ramda.NET.ReflectionExtensions;
using System.Threading.Tasks;

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

        internal readonly static dynamic Adjust = Curry3<dynamic, int, IList, IList>((fn, idx, list) => {
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
            adjustedValue = Delegate(fn).DynamicInvoke(list[index]);

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

        internal readonly static dynamic Ascend = Curry3<dynamic, dynamic, dynamic, int>((fn, a, b) => AscendDescendInternal(Lt, Gt, fn, a, b));

        internal readonly static dynamic Apply = Curry2<dynamic, object[], object>((fn, args) => Delegate(fn).DynamicInvoke(args));

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

        internal readonly static dynamic Bind = Curry2<dynamic, object, object>((fn, thisObj) => {
            DynamicDelegate dynamicFn = Delegate(fn);

            return Arity(fn.Arity(), Delegate(arguments => {
                var @delegate = dynamicFn.Unwrap();

                @delegate = @delegate.Method.CreateDelegate(thisObj);

                return Delegate(@delegate).DynamicInvoke(arguments);
            }));
        });

        internal readonly static dynamic Clamp = Curry3<dynamic, dynamic, dynamic, dynamic>((min, max, value) => {
            if (min > max) {
                throw new ArgumentOutOfRangeException("min must not be greater than max in Clamp(min, max, value)");
            }

            return value < min ? min : value > max ? max : value;
        });

        internal readonly static dynamic Comparator = Curry1<dynamic, DynamicDelegate>(pred => {
            DynamicDelegate dynamicPred = Delegate(pred);

            return Delegate((a, b) => dynamicPred.DynamicInvoke<bool>(a, b) ? -1 : dynamicPred.DynamicInvoke<bool>(b, a) ? 1 : 0);
        });

        internal readonly static dynamic Dec = Add(-1);

        internal readonly static dynamic DefaultTo = Curry2<object, object, object>((defaultValue, value) => {
            var type = value.GetType();

            if (type.IsClass) {
                return ReferenceEquals(value, @null) ? defaultValue : value;
            }

            return value == type.GetDefaultValue() ? defaultValue : value;
        });

        internal readonly static dynamic Descend = Curry3<dynamic, dynamic, dynamic, int>((fn, a, b) => AscendDescendInternal(Gt, Lt, fn, a, b));

        internal readonly static dynamic DifferenceWith = Curry3<dynamic, IList, IList, IList>((pred, first, second) => {
            var idx = 0;
            var firstLen = first.Count;
            var result = new ArrayList();
            DynamicDelegate dynamicPred = Delegate(pred);
            Func<object, object, bool> containsPredicate = (a, b) => dynamicPred.DynamicInvoke<bool>(a, b);

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
            DynamicDelegate dynamicPred = Delegate(pred);

            while (idx < len && dynamicPred.DynamicInvoke<bool>(list[idx])) {
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

                if (type.Equals(typeofString)) {
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

            return R.@null;
        });

        internal readonly static dynamic Evolve = Curry2<object, object, object>(InternalEvolve);

        internal readonly static dynamic Find = Curry2(Dispatchable2("Find", XFind, new Func<dynamic, IList, object>((fn, list) => {
            return FindInternal(0, 1, idx => idx < list.Count, obj => Delegate(fn).DynamicInvoke<bool>(obj), list);
        })));

        internal readonly static dynamic FindIndex = Curry2(Dispatchable2("FindIndex", XFindIndex, new Func<dynamic, IList, int>((fn, list) => {
            return FindIndexInternal(0, 1, idx => idx < list.Count, obj => Delegate(fn).DynamicInvoke<bool>(obj), list);
        })));

        internal readonly static dynamic FindLast = Curry2(Dispatchable2("FindLast", XFindLast, new Func<dynamic, IList, object>((fn, list) => {
            return FindInternal(list.Count - 1, -1, idx => idx >= 0, obj => Delegate(fn).DynamicInvoke<bool>(obj), list);
        })));

        internal readonly static dynamic FindLastIndex = Curry2(Dispatchable2("FindLastIndex", XFindLastIndex, new Func<dynamic, IList, int>((fn, list) => {
            return FindIndexInternal(list.Count - 1, -1, idx => idx >= 0, obj => Delegate(fn).DynamicInvoke<bool>(obj), list);
        })));

        internal readonly static dynamic ForEach = Curry2(CheckForMethod2("ForEach", Delegate(new Func<dynamic, IList, IList>((fn, list) => {
            DynamicDelegate dynamicFn = Delegate(fn);

            foreach (var item in list) {
                dynamicFn.DynamicInvoke(item);
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

        internal readonly static dynamic GroupWith = Curry2<dynamic, IEnumerable, IEnumerable>((fn, list) => {
            var idx = 0;
            var res = new ArrayList();
            var strategy = ListStrategy.Resolve(list);
            var len = strategy.Length;
            var dynamicFn = Delegate(fn);

            while (idx < len) {
                var nextidx = idx + 1;

                while (nextidx < len && dynamicFn.DynamicInvoke<bool>(strategy[idx], strategy[nextidx])) {
                    nextidx += 1;
                }

                res.Add(strategy.Slice(idx, nextidx));
                idx = nextidx;
            }

            if (list.IsArray()) {
                return res.ToArray();
            }

            return res;
        });

        internal readonly static dynamic Gt = Curry2<dynamic, dynamic, bool>((a, b) => {
            var strA = a as string;

            if (strA != null) {
                return string.Compare(strA, (string)b) > 0;
            }

            return a > b;
        });

        internal readonly static dynamic Gte = Curry2<dynamic, dynamic, bool>((a, b) => {
            var strA = a as string;

            if (strA != null) {
                return string.Compare(strA, (string)b) >= 0;
            }

            return a >= b;
        });

        internal readonly static dynamic Has = Curry2<string, object, bool>((prop, obj) => obj.Has(prop));

        internal readonly static dynamic Identical = Curry2<object, object, bool>((a, b) => a?.Equals(b) ?? b.IsNull());

        internal readonly static dynamic Identity = Curry1<object, object>(IdentityInternal);

        internal readonly static dynamic IfElse = Curry3<dynamic, dynamic, dynamic, DynamicDelegate>((condition, onTrue, onFalse) => {
            int maxArity = 0;

            onTrue = Delegate(onTrue);
            onFalse = Delegate(onFalse);
            condition = Delegate(condition);
            maxArity = new[] { condition.Length, onTrue.Length, onFalse.Length }.Max();

            return CurryN(maxArity, Delegate(arguments => {
                if (arguments.Length > maxArity) {
                    arguments = new[] { arguments };
                }

                return InternalIfElse(condition, onTrue, onFalse, arguments);
            }));
        });

        internal readonly static dynamic Inc = Add(1);

        internal readonly static dynamic Insert = Curry3<int, object, IList, IList>((idx, elt, list) => {
            var elementType = list.GetElementType();
            var eltIsSameType = elementType.Equals(elt.GetType());
            var result = list.CreateNewList(list, eltIsSameType ? elementType : typeofObject);

            idx = idx < list.Count && idx >= 0 ? idx : list.Count;
            result.Insert(idx, elt);

            if (list.IsArray()) {
                return result.ToArray<Array>();
            }

            return result;
        });

        internal readonly static dynamic InsertAll = Curry3<int, IList, IList, IList>((idx, elts, list) => {
            idx = idx < list.Count && idx >= 0 ? idx : list.Count;

            return list.Slice(0, idx).Concat(elts).Concat(list.Slice(idx));
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

        internal readonly static dynamic IsArrayLike = Curry1<object, bool>(x => {
            int length;
            object member;
            var type = x.GetType();

            if (x.IsList()) {
                return true;
            }

            if (type.TypeIsFunction()) {
                return false;
            }

            if (type.Equals(typeofString)) {
                return false;
            }

            member = x.Member("Length");

            if (member is int) {
                length = (int)member;

                if (length == 0) {
                    return true;
                }

                if (length > 0) {
                    var dictionary = x as IDictionary;

                    if (dictionary != null) {
                        return dictionary.ContainsKeys(new object[] { 0, length - 1 });
                    }
                }
            }

            return false;
        });

        internal readonly static dynamic Is = Curry2<Type, object, bool>((type, val) => {
            if (val.IsNotNull()) {
                var valueType = val.GetType();

                return valueType.Equals(type) || type.IsInstanceOfType(val);
            }

            return false;
        });

        internal readonly static dynamic IsNil = Curry1<object, bool>(val => val.IsNull());

        internal readonly static dynamic Keys = Curry1<object, string[]>(val => {
            if (val.IsPrimitive()) {
                return emptyArray;
            }

            return val.ToMemberDictionary().Select(kv => kv.Key).ToArray();
        });

        internal readonly static dynamic Length = Curry1<object, int>(list => {
            object member = list.Member("Length");

            if (member is int) {
                return (int)member;
            }

            return -1;
        });

        internal readonly static dynamic Lt = Curry2<dynamic, dynamic, bool>((a, b) => {
            var strA = a as string;

            if (strA != null) {
                return string.Compare(strA, (string)b) < 0;
            }

            return a < b;
        });

        internal readonly static dynamic Lte = Curry2<dynamic, dynamic, bool>((a, b) => {
            var strA = a as string;

            if (strA != null) {
                return string.Compare(strA, (string)b) <= 0;
            }

            return a <= b;
        });

        internal readonly static dynamic MapAccum = Curry3<dynamic, object, IList, Tuple<object, IList>>((fn, acc, list) => {
            var idx = 0;
            var len = list.Count;
            var tuple = R.Tuple.Create(acc, null);
            IList result = new object[list.Count];
            DynamicDelegate dynamicFn = Delegate(fn);

            while (idx < len) {
                tuple = dynamicFn.DynamicInvoke<Tuple<object, object>>(tuple.Item1, list[idx]);
                result[idx] = tuple.Item1;
                idx += 1;
            }

            return Sys.Tuple.Create(tuple.Item1, result);
        });

        internal readonly static dynamic MapAccumRight = Curry3<dynamic, object, IList, Tuple<IList, object>>((fn, acc, list) => {
            var idx = list.Count - 1;
            var tuple = R.Tuple.Create(acc, null);
            IList result = new object[list.Count];
            DynamicDelegate dynamicFn = Delegate(fn);

            while (idx >= 0) {
                tuple = dynamicFn.DynamicInvoke<Tuple<object, object>>(list[idx], tuple.Item1);
                result[idx] = tuple.Item1;
                idx -= 1;
            }

            return Sys.Tuple.Create(result, tuple.Item1);
        });

        internal readonly static dynamic Match = Curry2<Regex, string, MatchCollection>((rx, str) => rx.Matches(str));

        internal readonly static dynamic MathMod = Curry2<dynamic, dynamic, int>((m, p) => {
            var modulus = (uint)p;
            var dividend = (int)m;

            return (int)((dividend % modulus + modulus) % modulus);
        });

        internal readonly static dynamic Max = Curry2<dynamic, dynamic, dynamic>((a, b) => Gt(b, a) ? b : a);

        internal readonly static dynamic MaxBy = Curry3<dynamic, dynamic, dynamic, dynamic>((f, a, b) => {
            DynamicDelegate dynamicFn = Delegate(f);

            return dynamicFn.DynamicInvoke(b) > dynamicFn.DynamicInvoke(a) ? b : a;
        });

        internal readonly static dynamic Merge = Curry2<object, object, object>((l, r) => Assign(l, r));

        internal readonly static dynamic MergeAll = Curry1<IList, object>(Assign);

        internal readonly static dynamic MergeWithKey = Curry3<dynamic, object, object, object>((fn, l, r) => {
            DynamicDelegate dynamicFn = Delegate(fn);
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var pair in l.ToMemberDictionary()) {
                if (l.Has(pair.Key)) {
                    result[pair.Key] = r.Has(pair.Key) ? dynamicFn.DynamicInvoke(pair.Key, l.Member(pair.Key), r.Member(pair.Key)) : l.Member(pair.Key);
                }
            }

            foreach (var pair in r.ToMemberDictionary()) {
                if (r.Has(pair.Key) && !result.Has(pair.Key)) {
                    result[pair.Key] = r.Member(pair.Key);
                }
            }

            return result;
        });

        internal readonly static dynamic Min = Curry2<dynamic, dynamic, dynamic>((a, b) => Gt(a, b) ? b : a);

        internal readonly static dynamic MinBy = Curry3<dynamic, dynamic, dynamic, dynamic>((f, a, b) => {
            DynamicDelegate dynamicFn = Delegate(f);

            return dynamicFn.DynamicInvoke(b) < dynamicFn.DynamicInvoke(a) ? b : a;
        });

        internal readonly static dynamic Modulo = Curry2<dynamic, dynamic, dynamic>((a, b) => a % b);

        internal readonly static dynamic Multiply = Curry2<dynamic, dynamic, dynamic>((a, b) => a * b);

        internal readonly static dynamic NAry = Curry2<int, dynamic, DynamicDelegate>((length, fn) => {
            if (length <= 10) {
                return Delegate(arguments => {
                    object[] args;
                    DynamicDelegate dynamicDelegate = Delegate(fn);
                    var copyRange = Math.Min(length, arguments.Length);

                    args = (object[])R.Repeat(R.@null, length);
                    Array.Copy(arguments, (Array)args, copyRange);

                    return Reflection.DynamicDirectInvoke(dynamicDelegate, args);
                }, length);
            }
            else {
                throw new ArgumentOutOfRangeException("length", "First argument to NAry must be a non-negative integer no greater than ten");
            }
        });

        internal readonly static dynamic Negate = Curry1<dynamic, dynamic>(n => -n);

        internal readonly static dynamic None = Curry2(ComplementInternal(Dispatchable2("Any", XAny, new Func<object, object, dynamic>((a, b) => Any(a, b)))));

        internal readonly static dynamic Not = Curry1<bool, bool>(a => !a);

        internal readonly static dynamic Nth = Curry2<int, dynamic, object>((offset, list) => {
            object item = R.@null;
            var @string = list as string;
            var isString = @string != null;
            var count = isString ? @string.Length : ((IList)list).Count;
            var idx = offset < 0 ? count + offset : offset;

            if (idx < 0 || idx >= count) {
                if (isString) {
                    return string.Empty;
                }
            }
            else {
                item = list[idx];

                if (isString) {
                    item = item.ToString();
                }
            }

            return item;
        });

        internal readonly static dynamic NthArg = Curry1<int, DynamicDelegate>(n => {
            var arity = n < 0 ? 1 : n + 1;

            return Arity(arity, CurryNInternal(arity, new object[0], Delegate((object[] arguments) => {
                return Nth(n, arguments);
            })));
        });

        internal readonly static dynamic ObjOf = Curry2<string, object, object>((key, val) => {
            IDictionary<string, object> obj = new ExpandoObject();

            obj[key] = val;

            return obj;
        });

        internal readonly static dynamic Of = Curry1<object, dynamic>(OfInternal);

        internal readonly static dynamic Once = Curry1<dynamic, DynamicDelegate>(fn => {
            var called = false;
            object result = R.@null;
            DynamicDelegate dynamicFn = Delegate(fn);

            return Arity(fn.Arity(), Delegate((object[] arguments) => {
                if (called) {
                    return result;
                }

                called = true;
                result = dynamicFn.DynamicInvoke(arguments);
                return result;
            }));
        });

        internal readonly static dynamic Or = Curry2<bool, bool, bool>((a, b) => a || b);

        internal readonly static dynamic Over = Curry3<dynamic, dynamic, object, object>((lens, f, x) => {
            return Delegate(lens).DynamicInvoke(Delegate((object y) => IdentityFunctor(Delegate(f).DynamicInvoke(y))))(x).Value;
        });

        internal readonly static dynamic Pair = Curry2<object, object, object[]>((fst, snd) => new object[2] { fst, snd });

        internal readonly static dynamic Path = Curry2<IList, object, object>((paths, obj) => {
            var idx = 0;
            var val = obj;

            while (idx < paths.Count) {
                if (val.IsNull()) {
                    return R.@null;
                }

                val = val.Member(paths[idx]);
                idx += 1;
            }

            return val;
        });

        internal readonly static dynamic PathOr = Curry3<object, IList, object, object>((d, p, obj) => DefaultTo(d, Path(p, obj)));

        internal readonly static dynamic PathSatisfies = Curry3<dynamic, IList, object, bool>((pred, propPath, obj) => propPath.Count > 0 && Delegate(pred).DynamicInvoke(Path(propPath, obj)));

        internal readonly static dynamic Pick = Curry2<IList, object, IDictionary<string, object>>((names, obj) => PickIntrenal(names, obj));

        internal readonly static dynamic PickAll = Curry2<IList, object, IDictionary<string, object>>((names, obj) => PickIntrenal(names, obj, true));

        internal readonly static dynamic PickBy = Curry2<dynamic, object, IDictionary<string, object>>((pred, obj) => {
            DynamicDelegate dynamicDelegate = Delegate(pred);
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var pair in obj.ToMemberDictionary()) {
                if (dynamicDelegate.DynamicInvoke<bool>(pair.Value, pair.Key, obj)) {
                    result[pair.Key] = pair.Value;
                }
            }

            return result;
        });

        internal readonly static dynamic Prepend = Curry2<object, IList, IList>((el, list) => new[] { el }.Concat(list));

        internal readonly static dynamic Prop = Curry2<dynamic, object, object>((p, obj) => Reflection.Member(obj, p));

        internal readonly static dynamic PropIs = Curry3<Type, dynamic, object, bool>((type, name, obj) => Is(type, Reflection.Member(obj, name)));

        internal readonly static dynamic PropOr = Curry3<object, string, object, object>((val, p, obj) => obj.IsNotNull() && obj.Has(p) ? Reflection.Member(obj, p) : val);

        internal readonly static dynamic PropSatisfies = Curry3<dynamic, dynamic, object, bool>((pred, name, obj) => Delegate(pred).DynamicInvoke(Reflection.Member(obj, name)));

        internal readonly static dynamic Props = Curry2<IEnumerable<string>, object, object>((ps, obj) => {
            var result = new ArrayList();

            foreach (var prop in ps) {
                result.Add(obj.Member(prop));
            }

            return result.ToArray<Array>();
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

        internal readonly static dynamic ReduceRight = Curry3<dynamic, object, IList, object>((fn, acc, list) => {
            var idx = list.Count - 1;
            DynamicDelegate dynamicDelegate = Delegate(fn);

            while (idx >= 0) {
                acc = dynamicDelegate.DynamicInvoke(list[idx], acc);
                idx -= 1;
            }

            return acc;
        });

        internal readonly static dynamic Reduced = Curry1<object, IReduced>(ReducedInternal);

        internal readonly static dynamic Remove = Curry3<int, int, IList, IList>((start, count, list) => {
            return list.Slice(0, Math.Min(start, list.Count)).Concat(list.Slice(Math.Min(list.Count, start + count)));
        });

        internal readonly static dynamic Replace = Curry3<Regex, string, string, string>((regex, replacement, str) => regex.Replace(str, replacement));

        internal readonly static dynamic Reverse = Curry1<IEnumerable, IEnumerable>(enumerable => {
            var listOfString = enumerable as string;

            if (listOfString != null) {
                return string.Join(string.Empty, listOfString.Reverse());
            }

            var list = enumerable.CreateNewList();
            var enumerator = enumerable.GetEnumerator();

            while (enumerator.MoveNext()) {
                list.Insert(0, enumerator.Current);
            }

            return list.ToArray<Array>();
        });

        internal readonly static dynamic Scan = Curry3<dynamic, object, IList, IList>((fn, acc, list) => {
            var idx = 0;
            var len = list.Count;
            var result = new ArrayList() { acc };
            DynamicDelegate dynamicDelegate = Delegate(fn);


            while (idx < len) {
                acc = dynamicDelegate.DynamicInvoke(acc, list[idx]);
                result.Insert(idx + 1, acc);
                idx += 1;
            }

            return result.ToArray<Array>();
        });

        internal readonly static dynamic Set = Curry3<dynamic, object, object, object>((lens, v, x) => {
            return Over(lens, Always(v), x);
        });

        internal readonly static dynamic Slice = Curry3(CheckForMethod3("Slice", new Func<int, int, IEnumerable, IEnumerable>((int fromIndex, int toIndex, IEnumerable list) => {
            var strategy = ListStrategy.Resolve(list);
            var upTo = Math.Min(toIndex, strategy.Length);
            var start = (fromIndex >= 0) ? fromIndex : Math.Max(0, strategy.Length + fromIndex);

            if (toIndex < 0) {
                upTo = strategy.Length + toIndex;
            }

            return list.Slice(start, upTo);
        })));

        internal readonly static dynamic Sort = Curry2<dynamic, IList, IList>((comparator, list) => {
            DynamicDelegate dynamicDelegate = Delegate(comparator);

            return SortInternal(list.Slice(), dynamicDelegate.ToComparer((x, y) => dynamicDelegate.DynamicInvoke<int>(x, y)));
        });

        internal readonly static dynamic SortBy = Curry2<dynamic, IList, IList>((fn, list) => {
            DynamicDelegate dynamicDelegate = Delegate(fn);

            return SortInternal(list, dynamicDelegate.ToComparer((x, y) => {
                return Ascend(dynamicDelegate, x, y);
            }));
        });

        internal readonly static dynamic SplitAt = Curry2<int, IEnumerable, IList>((index, array) => {
            var strategy = ListStrategy.Resolve(array);

            if (index < 0) {
                index = strategy.Length + index;
            }

            return new[] {
                strategy.Slice(0, index),
                strategy.Slice(index, (int)Length(array))
            };
        });

        internal readonly static dynamic SortWith = Curry2<IList, IList, IList>((fns, list) => {
            return list.Sort<dynamic>((a, b) => {
                var i = 0;
                var result = 0;

                while (result == 0 && i < fns.Count) {
                    DynamicDelegate dynamicDelegate = Delegate(fns[i]);

                    result = dynamicDelegate.DynamicInvoke(a, b);
                    i += 1;
                }

                return result;
            });
        });

        internal readonly static dynamic SplitEvery = Curry2<int, IEnumerable, IEnumerable>((n, list) => {
            if (n <= 0) {
                throw new ArgumentOutOfRangeException(nameof(n), "First argument to splitEvery must be a positive integer");
            }

            var idx = 0;
            var strategy = ListStrategy.Resolve(list);
            var result = list.CreateNewList(type: strategy.GetElementType());

            while (idx < strategy.Length) {
                result.Add(Slice(idx, idx += n, list));
            }

            return result;
        });

        internal readonly static dynamic SplitWhen = Curry2<DynamicDelegate, IList, IList>((pred, list) => {
            var idx = 0;
            var len = list.Count;
            var prefix = list.CreateNewList();
            DynamicDelegate dynamicDelegate = Delegate(pred);
            var result = list.CreateNewList(type: prefix.GetType());

            if (list.IsArray()) {
                list = list.CreateNewList(list);
            }

            while (idx < len && !dynamicDelegate.DynamicInvoke<bool>(list[idx])) {
                prefix.Add(list[idx]);
                idx += 1;
            }

            result.Add(prefix);
            result.Add(list.Slice(idx));

            return result;
        });

        internal readonly static dynamic Subtract = Curry2<object, object, dynamic>((arg1, arg2) => arg1.ToNumber() - arg2.ToNumber());

        internal readonly static dynamic Tail = CheckForMethod1("Tail", Slice(1, int.MaxValue));

        internal readonly static dynamic Take = Curry2(Dispatchable2("Take", XTake, new Func<int, IEnumerable, IEnumerable>((n, xs) => {
            return Slice(0, n < 0 ? int.MaxValue : n, xs);
        })));

        internal readonly static dynamic TakeLastWhile = Curry2<dynamic, IList, IList>((fn, list) => {
            DynamicDelegate dynamicDelegate = Delegate(fn);

            return TakeWhileInternal(list.Count - 1, -1, idx => idx >= 0, dynamicDelegate, list, idx => idx + 1, idx => int.MaxValue);
        });

        internal readonly static dynamic TakeWhile = Curry2(Dispatchable2("TakeWhile", XTakeWhile, new Func<dynamic, IList, IList>((fn, list) => {
            DynamicDelegate dynamicDelegate = Delegate(fn);

            return TakeWhileInternal(0, 1, idx => idx < list.Count, dynamicDelegate, list, idx => 0, idx => idx);
        })));

        internal readonly static dynamic Tap = Curry2<dynamic, object, object>((fn, value) => {
            Delegate(fn).DynamicInvoke(value);
            return value;
        });

        internal readonly static dynamic Times = Curry2<dynamic, int, IList>((fn, n) => {
            if (n < 0) {
                throw new ArgumentOutOfRangeException("n must be a non-negative number");
            }

            var idx = 0;
            var list = new object[n];
            DynamicDelegate dynamicDelegate = Delegate(fn);

            while (idx < n) {
                list[idx] = dynamicDelegate.DynamicInvoke(idx);
                idx += 1;
            }

            return list.ToArray<Array>();
        });

        internal readonly static dynamic ToPairs = Curry1<object, object[]>((obj) => {
            return obj.ToMemberDictionary()
                      .OrderBy(prop => prop.Key)
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

        internal readonly static dynamic TryCatch = Curry2<dynamic, dynamic, DynamicDelegate>((tryer, catcher) => {
            return Delegate(arguments => {
                try {
                    return Delegate(tryer).DynamicInvoke(arguments);
                }
                catch (Exception e) {
                    return Delegate(catcher).DynamicInvoke(e).Concat(arguments);
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

        internal readonly static dynamic Unapply = Curry1<dynamic, object>(fn => {
            return Delegate(arguments => Delegate(fn).DynamicInvoke(arguments.Slice()));
        });

        internal readonly static dynamic Unary = Curry1<dynamic, DynamicDelegate>(fn => NAry(1, fn));

        internal readonly static dynamic UncurryN = Curry2<int, dynamic, object>((depth, fn) => {
            return CurryN(depth, Delegate(arguments => {
                int endIdx;
                var idx = 0;
                DynamicDelegate dynamicDelegate = Delegate(fn);
                var currentDepth = 1;

                while (currentDepth <= depth && dynamicDelegate.IsFunction()) {
                    var length = dynamicDelegate.Length;

                    endIdx = currentDepth == depth ? arguments.Length : idx + length;
                    dynamicDelegate = dynamicDelegate.DynamicInvoke<DynamicDelegate>(arguments.Slice(idx, endIdx));
                    currentDepth += 1;
                    idx = endIdx;
                }

                return dynamicDelegate;
            }));
        });

        internal readonly static dynamic Unfold = Curry2<dynamic, int, IList>((fn, seed) => {
            IList list;
            IList result = new List<int>();
            DynamicDelegate dynamicDelegate = Delegate(fn);
            var pair = dynamicDelegate.DynamicInvoke(seed);

            while ((list = pair as IList).IsNotNull()) {
                result.Add(list[0]);
                pair = dynamicDelegate.DynamicInvoke(list[1]);
            }

            return result.ToArray<Array>();
        });

        internal readonly static dynamic UniqWith = Curry2<dynamic, IList, IList>((pred, list) => {
            var result = list.CreateNewList();
            DynamicDelegate dynamicDelegate = Delegate(pred);
            var prediacte = new Func<object, object, bool>((a, b) => dynamicDelegate.DynamicInvoke<bool>(a, b));

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

        internal readonly static dynamic Unless = Curry3<dynamic, dynamic, object, object>((pred, whenFalseFn, x) => {
            return Delegate(pred).DynamicInvoke(x) ? x : Delegate(whenFalseFn).DynamicInvoke(x);
        });

        internal readonly static dynamic Until = Curry3<dynamic, dynamic, object, object>((pred, fn, init) => {
            var val = init;
            var args = new[] { val };
            DynamicDelegate predicate = Delegate(pred);
            DynamicDelegate dynamicDelegate = Delegate(fn);

            while (!predicate.DynamicInvoke<bool>(args)) {
                val = fn.DynamicInvoke(args);
            }

            return val;
        });

        internal readonly static dynamic Update = Curry3<int, object, IList, IList>((idx, x, list) => Adjust(Always(x), idx, list));

        internal readonly static dynamic UseWith = Curry2<dynamic, IList, object>((fn, transformers) => {
            return CurryN(transformers.Count, Delegate(arguments => {
                var args = transformers.Select((item, i) => {
                    dynamic transformer = Delegate(item);

                    return transformer(arguments[i]);
                });

                return Reflection.DynamicDirectInvoke(fn, args.Concat((object[])arguments.Slice(transformers.Count)).ToArray());
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

        internal readonly static dynamic View = Curry2<dynamic, object, object>((lens, x) => {
            return Delegate(lens).DynamicInvoke((Func<object, Functor>)Const)(x).Value;
        });

        internal readonly static dynamic When = Curry3<dynamic, dynamic, object, object>((pred, whenTrueFn, x) => {
            return Delegate(pred).DynamicInvoke(x) ? Delegate(whenTrueFn).DynamicInvoke(x) : x;
        });

        internal readonly static dynamic Where = Curry2<IDictionary<string, object>, object, bool>((spec, testObj) => {
            foreach (var pair in spec.ToMemberDictionary()) {
                var testObjMember = testObj.Member(pair.Key);
                var @delegate = (Delegate)pair.Value;

                if (!(bool)@delegate.Invoke(new[] { testObjMember })) {
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

        internal readonly static dynamic ZipWith = Curry3<dynamic, IList, IList, IList>((fn, a, b) => {
            var idx = 0;
            Type type = null;
            var allSameType = true;
            var rv = new List<object>();
            var len = Math.Min(a.Count, b.Count);
            DynamicDelegate dynamicDelegate = Delegate(fn);

            while (idx < len) {
                var value = dynamicDelegate.DynamicInvoke(a[idx], b[idx]);
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

        internal readonly static dynamic AddIndex = Curry1<dynamic, DynamicDelegate>(fn => {
            return CurryN(fn.Length, Delegate((object[] arguments) => {
                var idx = 0;
                var origFn = Delegate(arguments[0]);
                var args = (object[])arguments.Slice();
                var list = arguments[arguments.Length - 1];

                args[0] = Delegate((object[] innerArguments) => {
                    var result = origFn.DynamicInvoke((object[])innerArguments.Concat(new[] { idx, list }));

                    idx += 1;

                    return result;
                });

                return Delegate(fn).DynamicInvoke(args);
            }));
        });

        internal readonly static dynamic Binary = Curry1<dynamic, DynamicDelegate>(fn => NAry(2, fn));

        internal readonly static dynamic Clone = Curry1<object, object>(DeepCloner.Clone);

        internal readonly static dynamic Curry = Curry1<dynamic, DynamicDelegate>(fn => {
            DynamicDelegate dynamicDelegate = Delegate(fn);

            return CurryN(Reflection.FunctionArity(dynamicDelegate), dynamicDelegate);
        });

        internal readonly static dynamic Drop = Curry2(Dispatchable2("Drop", XDrop, new Func<int, IEnumerable, IEnumerable>((n, xs) => {
            string value = xs as string;

            if (value != null) {
                return value.Substring(Math.Min(n, value.Length));
            }

            return Slice(Math.Max(0, n), int.MaxValue, xs);
        })));

        internal readonly static dynamic DropLast = Curry2(Dispatchable2("DropLast", XDropLast, new Func<int, IEnumerable, IEnumerable>(DropLastInternal)));

        internal readonly static dynamic DropLastWhile = Curry2(Dispatchable2("DropLastWhile", XDropLastWhile, new Func<dynamic, IList, IEnumerable>(DropLastWhileInternal)));

        internal new readonly static dynamic Equals = Curry2<object, object, bool>((a, b) => EqualsInternal(a, b, new ArrayList(), new ArrayList()));

        internal readonly static dynamic Filter = Curry2(Dispatchable2("Filter", XFilter, new Func<dynamic, object, object>((pred, filterable) => {
            DynamicDelegate predicate = Delegate(pred);

            return !filterable.IsEnumerable() ? ReduceInternal(Delegate(new Func<IDictionary<string, object>, string, IDictionary<string, object>>((acc, key) => {
                var value = filterable.Member(key);

                if (predicate.DynamicInvoke<bool>(value)) {
                    acc[key] = value;
                }

                return acc;
            })), new ExpandoObject(), filterable.Keys()) : FilterInternal(item => predicate.DynamicInvoke<bool>(item), (IList)filterable);
        })));

        internal readonly static dynamic Flatten = Curry1(MakeFlat(true));

        internal readonly static dynamic Flip = Curry1<dynamic, dynamic>(fn => {
            return Curry(Delegate((object[] arguments) => {
                var args = (object[])arguments.Slice();
                DynamicDelegate dynamicDelegate = Delegate(fn);

                if (args.Length > 1) {
                    var a = args[0];
                    var b = args[1];

                    args[0] = b;
                    args[1] = a;
                }

                return dynamicDelegate.DynamicInvoke(args);
            }, 2));
        });

        internal readonly static dynamic Head = Nth(0);

        internal readonly static dynamic Init = Curry1<IEnumerable, object>(list => {
            var strategy = ListStrategy.Resolve(list);

            return strategy.Slice(0, strategy.Length - 1);
        });

        internal readonly static dynamic IntersectionWith = Curry3<dynamic, IList, IList, IList>((pred, list1, list2) => {
            IList lookupList;
            IList filteredList;
            var results = new ArrayList();
            DynamicDelegate predicate = Delegate(pred);

            Func<object, object, bool> containsPredicate = (a, b) => predicate.DynamicInvoke<bool>(a, b);

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

            return ReduceInternal(Delegate(xf).DynamicInvoke(StepCat(acc)), ShallowCloner.Clone(acc), list);
        });

        internal readonly static dynamic Invert = Curry1<object, IDictionary<string, object>>(obj => {
            var inverted = new Dictionary<string, ArrayList>();
            IDictionary<string, object> result = new ExpandoObject();

            if (!obj.IsPrimitive()) {
                foreach (var prop in obj.Keys()) {
                    var val = obj.Member(prop).ToString();
                    var list = inverted.ContainsKey(val) ? inverted[val] : inverted[val] = new ArrayList();

                    list.Add(prop);
                }

                inverted.Keys.ForEach(key => {
                    result[key] = inverted[key].ToArray<Array>();
                });
            }

            return result;
        });

        internal readonly static dynamic InvertObj = Curry1<object, IDictionary<string, object>>(obj => {
            IDictionary<string, object> result = new ExpandoObject();

            if (!obj.IsPrimitive()) {
                foreach (var prop in obj.Keys()) {
                    var val = obj.Member(prop).ToString();

                    result[val] = prop;
                }
            }

            return result;
        });

        internal readonly static dynamic IsEmpty = Curry1<object, bool>(x => x.IsNotNull() && Equals(x, Empty(x)));

        internal readonly static dynamic Last = Nth(-1);

        internal readonly static dynamic LastIndexOf = Curry2<object, object, int>((target, xs) => {
            return IndexOfInternal("LastIndexOf", target, xs, list => new ArrayList(list).LastIndexOf(target), (str, targetStr) => str.LastIndexOf(targetStr));
        });

        internal readonly static dynamic Map = Curry2(Dispatchable2("Map", XMap, new Func<dynamic, object, object>((fn, functor) => {
            IList listFunctor = null;
            DynamicDelegate dynamicDelegate = Delegate(fn);

            if (functor.IsFunction()) {
                var functionFunctor = Delegate(functor);

                return CurryN(functionFunctor.Length, Delegate(((object[] arguments) => {
                    return dynamicDelegate.DynamicInvoke(functionFunctor.DynamicInvoke(arguments));
                })));
            }

            listFunctor = functor as IList;

            if (listFunctor.IsNull()) {
                return ReduceInternal(Delegate(new Func<IDictionary<string, object>, string, IDictionary<string, object>>((acc, key) => {
                    acc[key] = dynamicDelegate.DynamicInvoke(functor.Member(key));
                    return acc;
                })), new ExpandoObject(), functor.Keys());
            }

            return MapInternal(dynamicDelegate, listFunctor);
        })));

        internal readonly static dynamic MapObjIndexed = Curry2<dynamic, object, object>((fn, obj) => {
            return ReduceInternal(Delegate(new Func<IDictionary<string, object>, string, object>((acc, key) => {
                acc[key] = Delegate(fn).DynamicInvoke(obj.Member(key), key, obj);
                return acc;
            })), new ExpandoObject(), obj.Keys());
        });

        internal readonly static dynamic MergeWith = Curry3<dynamic, object, object, object>((fn, l, r) => {
            return MergeWithKey(Delegate(new Func<object, object, object, object>((_, _l, _r) => Delegate(fn).DynamicInvoke(_l, _r))), l, r);
        });

        internal readonly static dynamic Partial = CreatePartialApplicator(Delegate(new Func<IList, IList, IList>(Core.Concat)));

        internal readonly static dynamic PartialRight = CreatePartialApplicator(Flip(new Func<IList, IList, IList>(Core.Concat)));

        internal readonly static dynamic PathEq = Curry3<IList, object, object, bool>((_path, val, obj) => Equals(Path(_path, obj), val));

        internal readonly static dynamic Pluck = Curry2<object, dynamic, dynamic>((p, list) => Map(Prop(p), list));

        internal readonly static dynamic Project = UseWith(Delegate(new Func<dynamic, IList, object>(MapInternal)), new object[] { PickAll, Identity });

        internal readonly static dynamic PropEq = Curry3<string, object, object, bool>((name, val, obj) => Equals(val, obj.Member(name)));

        internal readonly static dynamic Reduce = Curry3<object, object, object, object>(ReduceInternal);

        internal readonly static dynamic ReduceBy = CurryN(4, Dispatchable4("ReduceBy", XReduceBy, Delegate(new Func<dynamic, object, dynamic, IList, object>((valueFn, valueAcc, keyFn, list) => {
            DynamicDelegate dynamicKeyFn = Delegate(keyFn);
            DynamicDelegate dynamicValueFn = Delegate(valueFn);

            return ReduceInternal(Delegate(new Func<IDictionary<string, object>, object, object>((acc, elt) => {
                var key = dynamicKeyFn.DynamicInvoke(elt).ToString();

                acc[key] = dynamicValueFn.DynamicInvoke(acc.ContainsKey(key) ? acc[key] : valueAcc, elt);

                return acc;
            })), new ExpandoObject(), list);
        }))));

        internal readonly static dynamic ReduceWhile = CurryN(4, new Func<dynamic, dynamic, object, IList, object>((pred, fn, a, list) => {
            DynamicDelegate predicate = Delegate(pred);
            DynamicDelegate dynamicDelegate = Delegate(fn);

            return ReduceInternal(Delegate(new Func<object, object, object>((acc, x) => {
                var args = new[] { acc, x };

                return predicate.DynamicInvoke<bool>(acc, x) ? dynamicDelegate.DynamicInvoke(acc, x) : ReducedInternal(acc);
            })), a, list);
        }));

        internal readonly static dynamic Reject = Curry2<dynamic, object, object>((pred, filterable) => Filter(ComplementInternal(Delegate(pred)), filterable));

        internal readonly static dynamic Repeat = Curry2<object, int, IList>((value, n) => Times(Always(value), n));

        internal readonly static dynamic Sum = Reduce(Add, 0);

        internal readonly static dynamic TakeLast = Curry2<int, IEnumerable, IEnumerable>((n, xs) => Drop(n >= 0 ? ListStrategy.Resolve(xs).Length - n : 0, xs));

        internal readonly static dynamic Transduce = CurryN(4, Delegate(new Func<dynamic, object, object, object, object>((xf, fn, acc, list) => {
            DynamicDelegate transducer = Delegate(xf);
            DynamicDelegate dynamicDelegate = Delegate(fn);

            return ReduceInternal(transducer.DynamicInvoke(fn.IsFunction() ? new XWrap(dynamicDelegate) : fn), acc, list);
        })));

        internal readonly static dynamic UnionWith = Curry3<dynamic, IList, IList, IList>((pred, list1, list2) => UniqWith(pred, list1.Concat(list2)));

        internal readonly static dynamic WhereEq = Curry2<object, object, bool>((spec, testObj) => Where(Map(Equals, spec), testObj));

        internal readonly static dynamic AllPass = Curry1<IList, DynamicDelegate>(preds => CurryN(Reduce(Max, 0, Pluck("Length", preds)), AnyOrAllPass(preds, false)));

        internal readonly static dynamic AnyPass = Curry1<IList, DynamicDelegate>(preds => CurryN(Reduce(Max, 0, Pluck("Length", preds)), AnyOrAllPass(preds, true)));

        internal readonly static dynamic Ap = Curry2<object, object, object>((applicative, fn) => {
            var ap = applicative.Member("Ap");

            if (ap.IsNotNull()) {
                return Delegate(ap).DynamicInvoke(fn);
            }

            if (applicative.IsFunction()) {
                return Delegate(new Func<object, object>(x => {
                    var result = Delegate(applicative).DynamicInvoke(x);

                    return Delegate(result).DynamicInvoke(Delegate(fn).DynamicInvoke(x));
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
            DynamicDelegate fn = Delegate(arguments[0]);

            return fn.DynamicInvoke((object[])arguments[1]);
        }));

        internal readonly static dynamic Chain = Curry2(Dispatchable2("Chain", XChain, new Func<dynamic, object, object>((fn, monad) => {
            if (monad.IsFunction()) {
                DynamicDelegate dynamicMonad = Delegate(monad);
                DynamicDelegate dynamicDelegate = Delegate(fn);

                return Delegate((object[] arguments) => {
                    var dynamicResult = dynamicDelegate.DynamicInvoke(arguments);
                    var resultFn = Delegate(dynamicMonad.DynamicInvoke(dynamicResult));

                    return resultFn.DynamicInvoke(arguments);
                });
            }

            return MakeFlat(false)(Map(fn, monad));
        })));

        internal readonly static dynamic Cond = Curry1<IList, object>(pairs => {
            var dynamicPairs = pairs.Cast<IList>().Select(pair => new[] { Delegate(pair[0]), Delegate(pair[1]) }).ToList();
            int arity = Reduce(Max, 0, Map(Delegate(pair => ((DynamicDelegate)pair[0]).Length), dynamicPairs));

            return Arity(arity, Delegate((object[] arguments) => {
                foreach (IList pair in dynamicPairs) {
                    DynamicDelegate @delegate = Delegate(pair[0]);

                    if (@delegate.DynamicInvoke<bool>(arguments)) {
                        return Delegate(pair[1]).DynamicInvoke(arguments);
                    }
                }

                return R.@null;
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
            DynamicDelegate dynamicDelegate = Delegate(after);

            return CurryN(Reduce(Max, 0, Pluck("Length", fns)), Delegate(arguments => {
                return dynamicDelegate.DynamicInvoke(MapInternal(Delegate(fn => Reflection.DynamicInvoke(fn, arguments)), dynamicFns));
            }));
        });

        internal readonly static dynamic CountBy = ReduceBy(Delegate(new Func<int, object, int>((acc, elem) => acc + 1)), 0);

        internal readonly static dynamic DropRepeatsWith = Curry2(Dispatchable2("DropRepeatsWith", XDropRepeatsWith, new Func<dynamic, IList, IList>((pred, list) => {
            var idx = 1;
            var len = list.Count;
            var result = new List<object>();
            DynamicDelegate dynamicDelegate = Delegate(pred);

            if (len != 0) {
                result.Add(list[0]);

                while (idx < len) {
                    if (!dynamicDelegate.DynamicInvoke<bool>(Last(result), list[idx])) {
                        result.Add(list[idx]);
                    }

                    idx += 1;
                }
            }

            return result.ToArray<Array>();
        })));

        internal readonly static dynamic EqBy = Curry3<DynamicDelegate, object, object, bool>((f, x, y) => R.Equals(((dynamic)f)(x), ((dynamic)f)(y)));

        internal readonly static dynamic EqProps = Curry3<string, object, object, bool>((prop, obj1, obj2) => R.Equals(obj1.Member(prop), obj2.Member(prop)));

        internal readonly static dynamic GroupBy = Curry2(CheckForMethod2("GroupBy", ReduceBy(Delegate(new Func<object, object, IList>((acc, item) => {
            var result = acc as IList;

            if (result.IsNull()) {
                result = new List<object>();
            }

            result.Add(item);

            return result;
        })), R.@null)));

        internal readonly static dynamic IndexBy = ReduceBy(Delegate((acc, elem) => elem), R.@null);

        internal readonly static dynamic IndexOf = Curry2<object, object, int>((target, xs) => {
            return IndexOfInternal("IndexOf", target, xs, list => list.IndexOf(target), (str, targetStr) => str.IndexOf(targetStr));
        });

        internal readonly static dynamic Juxt = Curry1<IList, DynamicDelegate>(fns => Converge(ArrayOf, fns));

        internal readonly static dynamic Lens = Curry2<dynamic, dynamic, dynamic>((getter, setter) => {
            return Delegate((dynamic toFunctorFn) => {
                return Delegate(target => {
                    DynamicDelegate dynamicGetter = Delegate(getter);
                    DynamicDelegate dynamicSetter = Delegate(setter);
                    var fn = Delegate(focus => dynamicSetter.DynamicInvoke(focus, target));
                    var functor = Delegate(toFunctorFn).DynamicInvoke(dynamicGetter.DynamicInvoke(target));

                    return Map(fn, functor);
                });
            });
        });

        internal readonly static dynamic LensIndex = Curry1<int, dynamic>(n => Lens(Nth(n), Update(n)));

        internal readonly static dynamic LensPath = Curry1<IList, dynamic>(p => Lens.DynamicInvoke(Path(p), AssocPath(p)));

        internal readonly static dynamic LensProp = Curry1<string, dynamic>(k => Lens(Prop(k), Assoc(k)));

        internal readonly static dynamic LiftN = Curry2<int, dynamic, DynamicDelegate>((arity, fn) => {
            return CurryN(arity, Delegate(arguments => ReduceInternal(Ap, Map(CurryN(arity, Delegate(fn)), arguments[0]), arguments.Slice(1))));
        });

        internal readonly static dynamic Mean = Curry1<IList, dynamic>(list => {
            if (list.Count == 0) {
                throw new NaNException();
            }

            return Sum(list) / list.Count;
        });

        internal readonly static dynamic Median = Curry1<IList, dynamic>(list => {
            var len = list.Count;

            if (len == 0) {
                throw new NaNException();
            }

            var width = 2 - len % 2;
            var idx = (len - width) / 2;

            return Mean(list.Slice().Sort<dynamic>((a, b) => {
                return a < b ? -1 : a > b ? 1 : 0;
            }).Slice(idx, idx + width));
        });

        internal readonly static dynamic Partition = Juxt(new[] { Filter, Reject });

        private readonly static dynamic ComposeFactory = Curry3<IList, DynamicDelegate, string, DynamicDelegate>((arguments, compose, name) => {
            if (arguments.Count == 0) {
                throw new ArgumentNullException($"{name} requires at least one argument");
            }

            return compose.DynamicInvoke<DynamicDelegate>(new object[] { Reverse(arguments) });
        });

        internal readonly static dynamic Pipe = Curry1<IList, DynamicDelegate>(arguments => {
            if (arguments.Count == 0) {
                throw new ArgumentNullException("Pipe requires at least one argument");
            }

            var delegates = arguments.Select(Delegate).ToArray();

            return Arity(delegates[0].Length, (DynamicDelegate)Reduce(Delegate(PipeInternal), delegates[0], Tail(delegates)));
        });

        internal readonly static dynamic PipeK = ComposeFactory(R.__, Delegate((object[] arguments) => ComposeK(arguments)), "PipeK");

        internal readonly static dynamic PipeP = Curry1<IList<Delegate>, DynamicDelegate>(arguments => {
            if (arguments.Count == 0) {
                throw new ArgumentNullException("PipeP requires at least one argument");
            }

            var delegates = arguments.Select(AwaitableDelegate).ToArray();

            return Arity(delegates[0].Length, ReduceTaskInternal(delegates[0], Tail(delegates)));
        });

        internal readonly static dynamic Product = Reduce(Multiply, 1);

        internal readonly static dynamic Sequence = Curry2<dynamic, IList, object>((of, traversable) => {
            if (!traversable.IsArray() && traversable.HasMemberWhere("Sequence", m => m.IsFunction())) {
                return ((dynamic)traversable).Sequence(of);
            }

            return ReduceRight(Delegate((x, acc) => Ap(Map(Prepend, x), acc)), Delegate(of).DynamicInvoke(new[] { new object[0] }), traversable);
        });

        internal readonly static dynamic Traverse = Curry3<dynamic, dynamic, object, IList>((of, f, traversable) => Sequence(of, Map(f, traversable)));

        internal readonly static dynamic Unnest = Chain(Delegate(IdentityInternal));

        internal readonly static dynamic Compose = ComposeFactory(R.__, Delegate((object[] arguments) => Pipe(arguments)), "Compose");

        internal readonly static dynamic ComposeK = Curry1<IList, DynamicDelegate>(arguments => {
            if (arguments.Count == 0) {
                throw new ArgumentNullException("ComposeK requires at least one argument");
            }

            var init = arguments.Select(Delegate).ToArray();
            var last = init.Last();

            return Compose.DynamicInvoke(Prepend(Identity, Map(Chain, init), last));
        });

        internal readonly static dynamic ComposeP = ComposeFactory(R.__, PipeP, "ComposeP");

        internal readonly static dynamic Construct = Curry1<dynamic, object>(Fn => {
            DynamicDelegate dynamicDelegate = Delegate(Fn);

            return ConstructN(dynamicDelegate.Length, dynamicDelegate);
        });

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

        internal readonly static dynamic Lift = Curry1<dynamic, dynamic>(fn => {
            DynamicDelegate dynamicDelegate = Delegate(fn);

            return LiftN(dynamicDelegate.Arity(), dynamicDelegate);
        });

        internal readonly static dynamic Omit = Curry2<object, object, IDictionary<string, object>>((names, obj) => {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var pair in obj.ToMemberDictionary()) {
                if (!ContainsInternal(pair.Key, names)) {
                    result[pair.Key] = pair.Value;
                }
            }

            return result;
        });

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

        internal static dynamic Either = Curry2<dynamic, dynamic, dynamic>((f, g) => BothOrEither(Delegate(f), Delegate(g), new Func<Func<bool>, Func<bool>, bool>((a, b) => a() || b()), Or));

        internal static dynamic Invoker = Curry2<int, string, object>((arity, method) => {
            return InvokerInternal(arity, method, (target, _) => NullableDelegate(target.Member(method, arity) as Delegate));
        });

        internal static dynamic Join = Curry2<string, IList, string>((separator, xs) => {
            return string.Join(separator, xs.Select(item => item.ToString()));
        });

        internal static dynamic Memoize = Curry1<dynamic, dynamic>(fn => {
            var cache = new Dictionary<string, object>();
            DynamicDelegate dynamicDelegate = Delegate(fn);

            return Arity(fn.Length, Delegate((object[] arguments) => {
                var args = Arguments(arguments);
                var key = args.IsNotNull() ? (string)ToString(args) : string.Empty;

                return cache.GetOrAdd<string, object>(key, () => {
                    return dynamicDelegate.DynamicInvoke(args);
                });
            }));
        });

        internal static dynamic Split = InvokerInternal(1, "Split", new Func<object, int, DynamicDelegate>((t, arity) => {
            return Delegate((object[] arguments) => {
                var target = (string)t;
                var seperator = (string)arguments[0];

                return target.Split(new[] { seperator }, StringSplitOptions.None);
            });
        }));

        internal static dynamic SymmetricDifference = Curry2<IList, IList, IList>((list1, list2) => {
            return Concat(Difference(list1, list2), Difference(list2, list1));
        });

        internal static dynamic SymmetricDifferenceWith = Curry3<dynamic, IList, IList, IList>((pred, list1, list2) => {
            DynamicDelegate predicate = Delegate(pred);

            return Concat(DifferenceWith(predicate, list1, list2), DifferenceWith(predicate, list2, list1));
        });

        internal static dynamic Test = Curry2<Regex, string, bool>((pattern, str) => pattern.IsMatch(str));

        internal static dynamic ToLower = Invoker(0, "ToLower");

        internal static dynamic ToUpper = Invoker(0, "ToUpper");

        internal static dynamic UniqBy = Curry2<dynamic, IList, IList>((fn, list) => {
            var result = new ArrayList();
            var set = new HashSet<object>();
            DynamicDelegate dynamicDelegate = Delegate(fn);

            foreach (var item in list) {
                var appliedItem = dynamicDelegate.DynamicInvoke(item);

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

        internal static dynamic Union = Curry2(Compose(R.__, Uniq, new Func<IList, IList, IList>(Core.ConcatInternal)));
    }
}
