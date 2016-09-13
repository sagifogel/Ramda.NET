using System;
using System.Linq;
using System.Dynamic;
using System.Collections;
using static Ramda.NET.Core;
using static Ramda.NET.Lambda;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Ramda.NET
{
    internal static partial class Currying
    {
        internal class IdentityObj
        {
            public object Value { get; set; }
            public Delegate Map { get; set; }
        }

        internal static dynamic Curry1<TArg1, TResult>(Func<TArg1, TResult> fn) {
            return new LambdaN(arguments => {
                var arg1 = arguments.AssignIfArgumentInRange(0);

                if (R.__.Equals(arg1) || arguments.Arity() == 0) {
                    return Curry1(fn);
                }

                return fn(arg1.CastTo<TArg1>());
            });
        }

        internal static dynamic Curry2<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> fn) {
            return new LambdaN(arguments => {
                var arg1IsPlaceHolder = false;
                var arg2IsPlaceHolder = false;
                var arg1 = arguments.AssignIfArgumentInRange(0);
                var arg2 = arguments.AssignIfArgumentInRange(1);

                switch (Arity(arguments)) {
                    case 0:
                        return Curry2(fn);
                    case 1:
                        return IsPlaceholder(arg1) ? Curry2(fn) : Curry1<TArg2, TResult>(_arg2 => fn(arg1.CastTo<TArg1>(), _arg2));
                    default:
                        arg1IsPlaceHolder = IsPlaceholder(arg1);
                        arg2IsPlaceHolder = IsPlaceholder(arg2);

                        return arg1IsPlaceHolder && arg2IsPlaceHolder ? Curry2(fn) : arg1IsPlaceHolder ? Curry1<TArg1, TResult>(_arg1 => {
                            return fn(_arg1, arg2.CastTo<TArg2>());
                        }) : arg2IsPlaceHolder ? Curry1<TArg2, TResult>(_arg2 => {
                            return fn(arg1.CastTo<TArg1>(), _arg2);
                        }) : fn(arg1.CastTo<TArg1>(), arg2.CastTo<TArg2>());
                }
            });
        }

        internal static dynamic Curry3<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, TResult> fn) {
            return new LambdaN(arguments => {
                var arg1IsPlaceHolder = false;
                var arg2IsPlaceHolder = false;
                var arg3IsPlaceHolder = false;
                var arg1 = arguments.AssignIfArgumentInRange(0);
                var arg2 = arguments.AssignIfArgumentInRange(1);
                var arg3 = arguments.AssignIfArgumentInRange(2);

                switch (arguments.Arity()) {
                    case 0:
                        return Curry3(fn);
                    case 1:
                        return IsPlaceholder(arg1) ? Curry3(fn) : Curry2<TArg2, TArg3, TResult>((_arg2, _arg3) => fn(arg1.CastTo<TArg1>(), _arg2, _arg3));
                    case 2:
                        arg1IsPlaceHolder = IsPlaceholder(arg1);
                        arg2IsPlaceHolder = IsPlaceholder(arg2);

                        return (arg1IsPlaceHolder = IsPlaceholder(arg1)) && (arg2IsPlaceHolder = IsPlaceholder(arg2)) ? Curry3(fn) : arg1IsPlaceHolder ? Curry2<TArg1, TArg3, TResult>((_arg1, _arg3) => {
                            return fn(_arg1, arg2.CastTo<TArg2>(), _arg3);
                        }) : arg2IsPlaceHolder ? Curry2<TArg2, TArg3, TResult>((_arg2, _arg3) => {
                            return fn(arg1.CastTo<TArg1>(), _arg2, _arg3);
                        }) : Curry1<TArg3, TResult>(_arg3 => {
                            return fn(arg1.CastTo<TArg1>(), arg2.CastTo<TArg2>(), _arg3);
                        });
                    default:

                        arg1IsPlaceHolder = IsPlaceholder(arg1);
                        arg2IsPlaceHolder = IsPlaceholder(arg2);
                        arg3IsPlaceHolder = IsPlaceholder(arg3);

                        return arg1IsPlaceHolder && arg2IsPlaceHolder && arg3IsPlaceHolder ? Curry3(fn) : arg1IsPlaceHolder && arg2IsPlaceHolder ? Curry2<TArg1, TArg2, TResult>((_arg1, _arg2) => {
                            return fn(_arg1, _arg2, arg3.CastTo<TArg3>());
                        }) : arg1IsPlaceHolder && arg3IsPlaceHolder ? Curry2<TArg1, TArg3, TResult>((_arg1, _arg3) => {
                            return fn(_arg1, arg2.CastTo<TArg2>(), _arg3);
                        }) : arg2IsPlaceHolder && arg3IsPlaceHolder ? Curry2<TArg2, TArg3, TResult>((_arg2, _arg3) => {
                            return fn(arg1.CastTo<TArg1>(), _arg2, _arg3);
                        }) : arg1IsPlaceHolder ? Curry1<TArg1, TResult>(_arg1 => {
                            return fn(_arg1, arg2.CastTo<TArg2>(), arg3.CastTo<TArg3>());
                        }) : arg2IsPlaceHolder ? Curry1<TArg2, TResult>(_arg2 => {
                            return fn(arg1.CastTo<TArg1>(), _arg2, arg3.CastTo<TArg3>());
                        }) : arg3IsPlaceHolder ? Curry1<TArg3, TResult>(_arg3 => {
                            return fn(arg1.CastTo<TArg1>(), arg2.CastTo<TArg2>(), _arg3);
                        }) : fn(arg1.CastTo<TArg1>(), arg2.CastTo<TArg2>(), arg3.CastTo<TArg3>());
                }
            });
        }

        internal static dynamic CurryN = Curry2<int, Delegate, dynamic>((length, fn) => {
            if (length == 1) {
                return Curry1(new Func<object, object>(arg => fn.DynamicInvoke(new[] { arg.ToInvokable() })));
            }

            return Arity(length, InternalCurryN(length, new object[0], fn));
        });

        internal readonly static dynamic Add = Curry2<double, double, double>((arg1, arg2) => {
            return arg1 + arg2;
        });

        public readonly static dynamic Adjust = Curry3<Func<dynamic, dynamic>, int, IList, IList>((fn, idx, list) => {
            var start = 0;
            var index = 0;
            IList concatedList = null;

            if (idx >= list.Count || idx < -list.Count) {
                return list;
            }

            start = idx < 0 ? list.Count : 0;
            index = start + idx;
            concatedList = Concat(list);
            concatedList[index] = fn(list[index]);

            return concatedList;
        });


        internal readonly static dynamic Always = Curry1<dynamic, Func<dynamic>>(value => () => value);

        internal readonly static dynamic And = Curry2<bool, bool, bool>((a, b) => a && b);

        internal readonly static dynamic All = CurryN(Dispatchable2("All", new LambdaN(arguments => null), new Func<Delegate, IList, bool>((fn, list) => AllOrAny(fn, list, false))));

        internal readonly static dynamic Any = Curry2(new Func<object, object, dynamic>(Dispatchable2("Any", new LambdaN(arguments => null), new Func<Delegate, IList, bool>((fn, list) => AllOrAny(fn, list, true)))));

        internal readonly static dynamic Aperture = CurryN(Dispatchable2("Aperture", new LambdaN(arguments => null), new Func<int, IList, IList>(Core.Aperture)));

        internal readonly static dynamic Append = Curry2<object, IList, IList>((el, list) => Concat(list, list.CreateNewList(new object[] { el })));

        internal readonly static dynamic Assoc = Curry3<string, object, object, object>((prop, val, obj) => ShallowCloner.CloneAndAssignValue(prop, val, obj));

        internal readonly static dynamic AssocPath = Curry3<IList<string>, object, object, object>((path, val, obj) => {
            switch (path.Count) {
                case 0:
                    return val;
                case 1:
                    return Assoc(path[0], val, obj);
                default:
                    return Assoc(path[0], AssocPath(Slice((IList)path, 1), val, obj.Member(path[0])), obj);
            }
        });

        internal readonly static dynamic Clamp = Curry3<dynamic, dynamic, dynamic, dynamic>((min, max, value) => {
            if (min > max) {
                throw new ArgumentOutOfRangeException("min must not be greater than max in Clamp(min, max, value)");
            }

            return value < min ? min : value > max ? max : value;
        });

        internal readonly static dynamic Comparator = Curry1<Func<object, object, bool>, Lambda2>(pred => {
            return new Lambda2((a, b) => pred(a, b) ? -1 : pred(b, a) ? 1 : 0);
        });

        internal readonly static dynamic Dec = Add(-1);

        internal readonly static dynamic DefaultTo = Curry2<object, object, object>((defaultValue, value) => {
            var type = value.GetType();

            if (type.IsClass) {
                return ReferenceEquals(value, null) ? defaultValue : value;
            }

            return value == type.GetDefaultValue() ? defaultValue : value;
        });

        internal readonly static dynamic DifferenceWith = Curry3<Func<object, object, bool>, IList, IList, IList>((pred, first, second) => {
            var idx = 0;
            var firstLen = first.Count;
            var result = new List<object>();

            while (idx < firstLen) {
                if (!ContainsWith(pred, first[idx], second) && !ContainsWith(pred, first[idx], result)) {
                    result.Add(first[idx]);
                }

                idx += 1;
            }

            return result;
        });

        internal readonly static dynamic Dissoc = Curry2<string, object, object>((prop, obj) => {
            return ShallowCloner.CloneAndAssignDefaultValue(prop, obj);
        });

        internal readonly static dynamic DissocPath = Curry2<IList, object, object>(InternalDissocPath);

        internal readonly static dynamic Divide = Curry2<dynamic, dynamic, dynamic>((a, b) => a / b);

        internal readonly static dynamic DropWhile = CurryN(Dispatchable2("DropWhile", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, IList>((pred, list) => {
            var idx = 0;
            var len = list.Count;

            while (idx < len && pred(list[idx])) {
                idx += 1;
            }

            return Slice(list, idx);
        })));

        internal readonly static dynamic Empty = Curry1<object, dynamic>(x => {
            if (x.IsNotNull()) {
                var type = x.GetType();

                if (type.Equals(typeof(string))) {
                    return string.Empty;
                }

                if (type.IsClass && !type.IsDelegate()) {
                    x.GetFactory().Invoke();
                }
            }

            return null;
        });

        internal readonly static dynamic Evolve = Curry2<IDictionary<string, object>, object, object>(InternalEvolve);

        internal readonly static dynamic Find = CurryN(Dispatchable2("find", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, object>((fn, list) => {
            return FindInternal(0, list.Count, 1, (idx, len) => idx < len, fn, list);
        })));

        internal readonly static dynamic FindIndex = CurryN(Dispatchable2("FindIndex", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, int>((fn, list) => {
            return FindIndexInternal(0, list.Count, 1, (idx, len) => idx < len, fn, list);
        })));

        internal readonly static dynamic FindLast = CurryN(Dispatchable2("FindLast", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, object>((fn, list) => {
            return FindInternal(list.Count - 1, 0, -1, (idx, len) => idx >= len, fn, list);
        })));

        internal readonly static dynamic FindLastIndex = CurryN(Dispatchable2("FindLastIndex", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, int>((fn, list) => {
            return FindIndexInternal(list.Count - 1, 0, -1, (idx, len) => idx >= len, fn, list);
        })));

        internal readonly static dynamic ForEach = Curry2(CheckForMethod2<Action<object>, IList, IList>("ForEach", (fn, list) => {
            var len = list.Count;
            var idx = 0;

            while (idx < len) {
                fn(list[idx]);
                idx += 1;
            }

            return list;
        }));

        internal readonly static dynamic FromPairs = Curry1<object[][], IDictionary<string, object>>(pairs => {
            var idx = 0;
            IDictionary<string, object> result = new ExpandoObject();

            while (idx < pairs.Length) {
                var key = pairs[idx][0];

                if (key is string) {
                    result[key.ToString()] = pairs[idx][1];
                }

                idx += 1;
            }

            return result;
        });

        internal readonly static dynamic GroupWith = Curry2<Delegate, IList, IList>((fn, list) => {
            var idx = 0;
            var len = list.Count;
            var res = new List<object>();

            while (idx < len) {
                var nextidx = idx + 1;

                while (nextidx < len && (bool)fn.DynamicInvoke(list[idx], list[nextidx])) {
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

        internal readonly static dynamic Has = Curry2<string, object, bool>(Core.Has);

        internal readonly static dynamic Identical = Curry2<object, object, bool>((a, b) => a.IsNotNull() ? a.Equals(b) : b.IsNull());

        internal readonly static dynamic Identity = Curry1<object, object>(Core.Identity);

        internal readonly static dynamic IfElse = Curry3<Delegate, Delegate, Delegate, LambdaN>((condition, onTrue, onFalse) => {
            return CurryN(1, new Lambda1(arg1 => InternalIfElse(condition, onTrue, onFalse, arg1)));
        });

        internal readonly static dynamic Inc = Add(1);

        internal readonly static dynamic Insert = Curry3<int, object, IList, IList>((idx, elt, list) => {
            var result = list.CreateNewList();

            idx = idx < list.Count && idx >= 0 ? idx : list.Count;
            result.Insert(idx, elt);

            if (list.IsArray()) {
                var arrayResult = result.CreateNewArray(list.Count);

                Array.Copy((Array)result, (Array)arrayResult, list.Count);

                return arrayResult;
            }

            return result;
        });

        internal readonly static dynamic InsertAll = Curry3<int, IList, IList, IList>((idx, elts, list) => {
            idx = idx < list.Count && idx >= 0 ? idx : list.Count;

            return Concat(Concat(Slice(list, 0, idx), elts), Slice(list, idx));
        });

        internal readonly static dynamic Intersperse = Curry2(CheckForMethod2<object, IList, IList>("intersperse", (separator, list) => {
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

            result = list.CreateNewListOfType(underlyingType);

            while (idx < length) {
                result.Add(list[idx]);

                if (idx != length - 1) {
                    result.Add(separator);
                }

                idx += 1;
            }

            return result;
        }));

        internal readonly static dynamic Is = Curry2<Type, object, bool>((type, val) => {
            if (val.IsNotNull()) {
                var valueType = val.GetType();

                return valueType.Equals(type) || valueType.IsInstanceOfType(type);
            }

            return false;
        });

        internal readonly static dynamic IsNil = Curry1<object, bool>(val => ReferenceEquals(val, null));

        internal readonly static dynamic Keys = Curry1<object, IEnumerable<string>>(val => val.ToMemberDictionary().Select(kv => kv.Key));

        internal readonly static dynamic Length = Curry1<IList, int>(list => list.Count);

        internal readonly static dynamic Lt = Curry2<dynamic, dynamic, bool>((a, b) => a < b);

        internal readonly static dynamic Lte = Curry2<dynamic, dynamic, bool>((a, b) => a <= b);

        internal readonly static dynamic MapAccum = Curry3<Func<object, object, R.Tuple>, object, IList, Tuple<object, IList>>((fn, acc, list) => {
            return MapAccumInternal(0, list.Count, 1, (from, to) => from < to, fn, acc, list);
        });

        internal readonly static dynamic MapAccumRight = Curry3<Func<object, object, R.Tuple>, object, IList, Tuple<object, IList>>((fn, acc, list) => {
            return MapAccumInternal(list.Count - 1, 0, -1, (from, to) => from >= to, fn, acc, list);
        });

        internal readonly static dynamic Match = Curry2<Regex, string, MatchCollection>((rx, str) => rx.Matches(str));

        internal readonly static dynamic MathMod = Curry2<dynamic, dynamic, dynamic>((m, p) => (m % p + p) % p);

        internal readonly static dynamic Max = Curry2<dynamic, dynamic, dynamic>((a, b) => b > a ? b : a);

        internal readonly static dynamic MaxBy = Curry3<Func<object, object>, dynamic, dynamic, dynamic>((f, a, b) => f(b) > f(a) ? b : a);

        internal readonly static dynamic Merge = Curry2<object, object, object>((l, r) => {
            return Assign(l, r);
        });

        internal readonly static dynamic MergeAll = Curry1<IList, object>(list => {
            return Assign(list);
        });

        internal readonly static dynamic MergeWithKey = Curry3<Delegate, object, object, object>((fn, l, r) => {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var pair in l.ToMemberDictionary()) {
                if (Core.Has(pair.Key, l)) {
                    result[pair.Key] = Has(pair, r) ? fn.DynamicInvoke(pair, l.Member(pair.Key), r.Member(pair.Key)) : l.Member(pair.Key);
                }
            }

            foreach (var pair in r.ToMemberDictionary()) {
                if (Core.Has(pair.Key, r) && !Core.Has(pair.Key, result)) {
                    result[pair.Key] = r.Member(pair.Key);
                }
            }

            return result;
        });

        internal readonly static dynamic Min = Curry2<dynamic, dynamic, dynamic>((a, b) => b < a ? b : a);

        internal readonly static dynamic MinBy = Curry3<Func<object, object>, dynamic, dynamic, dynamic>((f, a, b) => f(b) < f(a) ? b : a);

        internal readonly static dynamic Modulo = Curry2<dynamic, dynamic, dynamic>((a, b) => a % b);

        internal readonly static dynamic Multiply = Curry2<dynamic, dynamic, dynamic>((a, b) => a * b);

        internal readonly static dynamic NAry = Curry2<int, Delegate, Delegate>((length, fn) => {
            if (length <= 10) {
                return new LambdaN(arguments => {
                    IList args;

                    length = Math.Min(length, arguments.Length);
                    args = arguments.CreateNewArray(length);
                    Array.Copy(arguments, (Array)args, length);

                    return fn.DynamicInvoke(new[] { args });
                });
            }
            else {
                throw new ArgumentOutOfRangeException("length", "First argument to Arity must be a non-negative integer no greater than ten");
            }
        });

        internal readonly static dynamic Negate = Curry1<dynamic, dynamic>(n => -n);

        internal readonly static dynamic None = Curry2(new Func<object, object, dynamic>(Dispatchable2("any", new LambdaN(argumnets => null), new Func<object, object, dynamic>((a, b) => Any(a, b)))));

        internal readonly static dynamic Not = Curry1<bool, bool>(a => !a);

        internal readonly static dynamic Nth = Curry2<int, dynamic, object>((offset, list) => {
            var count = list.GetType().Equals(typeof(string)) ? ((string)list).Length : ((IList)list).Count;
            var idx = offset < 0 ? count + offset : offset;

            return list[idx];
        });

        internal readonly static dynamic NthArg = Curry1<int, LambdaN>(n => {
            var arity = n < 0 ? 1 : n + 1;

            return CurryN(arity, new Lambda1(arguments => {
                return Nth(n, arguments);
            }));
        });

        internal readonly static dynamic ObjOf = Curry2<string, object, object>((key, val) => {
            IDictionary<string, object> obj = new ExpandoObject();

            obj[key] = val;

            return obj;
        });

        internal readonly static dynamic Of = Curry1<object, dynamic>(x => Core.Of(x));

        internal readonly static dynamic Once = Curry1<Delegate, Delegate>(fn => {
            var called = false;
            object result = null;
            var arity = fn.Arity();

            return Arity(arity, new LambdaN(argumnets => {
                if (called) {
                    return result;
                }

                called = true;
                result = fn.DynamicInvoke(argumnets);
                return result;
            }));
        });

        internal readonly static dynamic Or = Curry2<bool, bool, bool>((a, b) => a || b);

        internal readonly static dynamic Over = Curry3<Func<Func<object, IdentityObj>, Func<object, IdentityObj>>, Delegate, object, object>((lens, f, x) => {
            return lens(y => IdentityInternal(f.DynamicInvoke(new[] { y.ToInvokable() })))(x).Value;
        });

        internal readonly static dynamic Pair = Curry2<object, object, object[]>((fst, snd) => {
            return new object[2] { fst, snd };
        });

        internal readonly static dynamic Path = Curry2<IList<string>, object, object>((paths, obj) => {
            var idx = 0;
            var val = obj;

            while (idx < paths.Count) {
                if (val == null) {
                    return null;
                }

                val = val.Member(paths[idx]);
                idx += 1;
            }

            return val;
        });

        internal readonly static dynamic PathOr = Curry3<object, IList<string>, object, object>((d, p, obj) => {
            return DefaultTo(d, Path(p, obj));
        });

        internal readonly static dynamic PathSatisfies = Curry3<Delegate, IList<string>, object, bool>((pred, propPath, obj) => {
            return propPath.Count > 0 && (bool)pred.DynamicInvoke(new[] { Path(propPath, obj) });
        });

        private static Tuple<object, IList> MapAccumInternal(int from, int to, int indexerAcc, Func<int, int, bool> loopPredicate, Func<object, object, R.Tuple> fn, object acc, IList list) {
            var tuple = R.Tuple.Create(acc, null);
            IList result = new object[list.Count];

            while (loopPredicate(from, to)) {
                tuple = fn(tuple.Item1, list[from]);
                result[from] = tuple.Item1;
                from += indexerAcc;
            }

            return Tuple.Create(tuple.Item1, result);
        }

        private static object FindInternal(int from, int to, int indexerAcc, Func<int, int, bool> loopPredicate, Func<object, bool> fn, IList list) {
            var idx = from;
            var len = to;

            while (loopPredicate(idx, len)) {
                if (fn(list[idx])) {
                    return list[idx];
                }

                idx += indexerAcc;
            }

            return null;
        }

        private static int FindIndexInternal(int from, int to, int indexerAcc, Func<int, int, bool> loopPredicate, Func<object, bool> fn, IList list) {
            var idx = from;
            var len = to;

            while (loopPredicate(idx, len)) {
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
                    var transformationType = transformation.GetType();

                    if (transformationType.IsDelegate()) {
                        result[key] = ((Delegate)transformation).DynamicInvoke(value);
                        continue;
                    }
                    else if (value is object) {
                        if (!transformationType.IsDictionary()) {
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
            var idx = 0;

            while (idx < list.Count) {
                if ((bool)fn.DynamicInvoke(list[idx])) {
                    return returnValue;
                }

                idx += 1;
            }

            return !returnValue;
        }

        private static bool IsPlaceholder(object param) {
            return param != null && R.__.Equals(param);
        }

        private static LambdaN InternalCurryN(int length, object[] received, Delegate fn) {
            return new LambdaN(arguments => {
                var argsIdx = 0;
                var left = length;
                var combinedIdx = 0;
                var combined = new List<object>();

                while (combinedIdx < received.Length || argsIdx < arguments.Length) {
                    object result = null;

                    if (combinedIdx < received.Length && (!IsPlaceholder(received[combinedIdx]) || argsIdx >= arguments.Length)) {
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

                return left <= 0 ? fn.DynamicInvoke(combined.ToInvokable()) : Arity(left, InternalCurryN(length, combined.ToArray(), fn));
            });
        }

        private static IdentityObj IdentityInternal(object x) {
            return new IdentityObj() {
                Value = x,
                Map = new Func<Delegate, IdentityObj>(f => {
                    return IdentityInternal(f.DynamicInvoke(new[] { x.ToInvokable() }));
                })
            };
        }
    }
}
