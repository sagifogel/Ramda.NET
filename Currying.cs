using System;
using Ramda.NET;
using System.Linq;
using System.Dynamic;
using System.Collections;
using static Ramda.NET.R;
using static Ramda.NET.Lambda;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Object = Ramda.NET.ReflectionExtensions;
using Core = Ramda.NET.IEnumerableExtensions;

namespace Ramda.NET
{
    internal static partial class Currying
    {
        internal static dynamic Curry1<TArg1, TResult>(Func<TArg1, TResult> fn) {
            return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                var length = arguments?.Length ?? 0;

                if (R.__.Equals(arg1) || length == 0) {
                    return Curry1(fn);
                }

                return fn(arg1.CastTo<TArg1>());
            });
        }

        internal static dynamic Curry2<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> fn) {
            return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var arg1IsPlaceHolder = false;
                var arg2IsPlaceHolder = false;
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                var length = arguments?.Length ?? 0;

                switch (length) {
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
            return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var arg1IsPlaceHolder = false;
                var arg2IsPlaceHolder = false;
                var arg3IsPlaceHolder = false;
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                var length = arguments?.Length ?? 0;

                switch (length) {
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

        internal static dynamic CurryParams(Delegate fn) {
            return new Lambda2((object arg1, object arg2) => {
                object[] args = null;
                var arg1IsPlaceHolder = false;
                var arg2IsPlaceHolder = false;
                var arguments = Arity(arg1, arg2);
                var length = arguments?.Length ?? 0;

                switch (length) {
                    case 0:
                        return CurryParams(fn);
                    case 1:
                        return IsPlaceholder(arg1) ? CurryParams(fn) : fn.Invoke(arg1);
                    default:
                        arg1IsPlaceHolder = IsPlaceholder(arg1);
                        arg2IsPlaceHolder = IsPlaceholder(arg2);
                        args = arg2 as object[];

                        return arg1IsPlaceHolder && arg2IsPlaceHolder ? CurryParams(fn) : arg1IsPlaceHolder ? Apply(R.__, args)
                        : arg2IsPlaceHolder ? new LambdaN((_arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10, ag11) => {
                            return Apply(fn, Arity(arg1, _arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
                        }) : fn.Invoke(new[] { arg1 }.Concat<object>(args).ToArray());
                }
            });
        }

        internal static dynamic CurryN = Curry2<int, Delegate, dynamic>((length, fn) => {
            if (length == 1) {
                return Curry1(new Func<object, object>(arg => fn.Invoke(arg)));
            }

            return Arity(length, CurryNInternal(length, new object[0], fn));
        });

        internal readonly static dynamic Add = Curry2<dynamic, dynamic, dynamic>((arg1, arg2) => arg1 + arg2);

        public readonly static dynamic Adjust = Curry3<Delegate, int, IList, IList>((fn, idx, list) => {
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
            adjustedValue = fn.Invoke(list[index]);

            if (!elementType.Equals(adjustedValue.GetType())) {
                adjustedValue = adjustedValue.Cast(elementType);
            }

            concatedList[index] = adjustedValue;

            return concatedList;
        });


        internal readonly static dynamic Always = Curry1<dynamic, LambdaN>(value => new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => value));

        internal readonly static dynamic And = Curry2<bool, bool, bool>((a, b) => a && b);

        internal readonly static dynamic All = Curry2(new Func<object, object, dynamic>(Dispatchable2("All", (Delegate)XAll, new Func<Delegate, IList, bool>((fn, list) => AllOrAny(fn, list, false)))));

        internal readonly static dynamic Any = Curry2(new Func<object, object, dynamic>(Dispatchable2("Any", (Delegate)XAny, new Func<Delegate, IList, bool>((fn, list) => AllOrAny(fn, list, true)))));

        internal readonly static dynamic Aperture = Curry2(new Func<object, object, dynamic>(Dispatchable2("Aperture", (Delegate)XAperture, new Func<int, IList, IList>(ApertureInternal))));

        internal readonly static dynamic Append = Curry2<object, IList, IList>((el, list) => list.Concat(list.CreateNewList(new object[] { el })));

        internal readonly static dynamic Apply = Curry2<Delegate, object[], object>((fn, args) => fn.Invoke(args));

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
            var result = new ArrayList();

            while (idx < firstLen) {
                if (!ContainsWith(pred, first[idx], second) && !ContainsWith(pred, first[idx], result)) {
                    result.Add(first[idx]);
                }

                idx += 1;
            }

            if (first.IsArray()) {
                return result.ToArray<Array>();
            }

            return result.ToArray();
        });

        internal readonly static dynamic Dissoc = Curry2<string, object, object>((prop, obj) => ShallowCloner.CloneAndAssignDefaultValue(prop, obj));

        internal readonly static dynamic DissocPath = Curry2<IList, object, object>(InternalDissocPath);

        internal readonly static dynamic Divide = Curry2<dynamic, dynamic, dynamic>((a, b) => a / b);

        internal readonly static dynamic DropWhile = Curry2(new Func<object, object, dynamic>(Dispatchable2("DropWhile", (Delegate)XDropWhile, new Func<Delegate, IList, IList>((pred, list) => {
            var idx = 0;
            var len = list.Count;

            while (idx < len && (bool)pred.Invoke(list[idx])) {
                idx += 1;
            }

            return list.Slice(idx);
        }))));

        internal readonly static dynamic Empty = Curry1<object, dynamic>(x => {
            if (x.IsNotNull()) {
                var type = x.GetType();

                if (type.Equals(typeof(string))) {
                    return string.Empty;
                }

                if (x.IsEnumerable()) {
                    return new object[0];
                }

                if (type.IsClass && !type.IsDelegate()) {
                    if (type.IsAnonymousType()) {
                        return new { };
                    }

                    return x.GetFactory().Invoke();
                }
            }

            return null;
        });

        internal readonly static dynamic Evolve = Curry2<IDictionary<string, object>, object, object>(InternalEvolve);

        internal readonly static dynamic Find = CurryN(Dispatchable2("Find", (Delegate)XFind, new Func<Delegate, IList, object>((fn, list) => {
            return FindInternal(0, 1, idx => idx < list.Count, obj => (bool)fn.Invoke(obj), list);
        })));

        internal readonly static dynamic FindIndex = CurryN(Dispatchable2("FindIndex", (Delegate)XFindIndex, new Func<Delegate, IList, int>((fn, list) => {
            return FindIndexInternal(0, 1, idx => idx < list.Count, obj => (bool)fn.Invoke(obj), list);
        })));

        internal readonly static dynamic FindLast = CurryN(Dispatchable2("FindLast", (Delegate)XFindLast, new Func<Delegate, IList, object>((fn, list) => {
            return FindInternal(list.Count - 1, -1, idx => idx >= 0, obj => (bool)fn.Invoke(obj), list);
        })));

        internal readonly static dynamic FindLastIndex = CurryN(Dispatchable2("FindLastIndex", (Delegate)XFindLastIndex, new Func<Delegate, IList, int>((fn, list) => {
            return FindIndexInternal(list.Count - 1, -1, idx => idx >= 0, obj => (bool)fn.Invoke(obj), list);
        })));

        internal readonly static dynamic ForEach = Curry2(CheckForMethod2("ForEach", new Func<Delegate, IList, IList>((fn, list) => {
            foreach (var item in list) {
                fn.Invoke(item);
            }

            return list;
        })));

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

        internal readonly static dynamic Has = Curry2<string, object, bool>(HasInternal);

        internal readonly static dynamic Identical = Curry2<object, object, bool>((a, b) => a?.Equals(b) ?? b.IsNull());

        internal readonly static dynamic Identity = Curry1<object, object>(IdentityInternal);

        internal readonly static dynamic IfElse = Curry3<Delegate, Delegate, Delegate, LambdaN>((condition, onTrue, onFalse) => {
            return CurryN(1, new Lambda1(arg1 => InternalIfElse(condition, onTrue, onFalse, arg1)));
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

        internal readonly static dynamic IsNil = Curry1<object, bool>(val => ReferenceEquals(val, null));

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

        internal readonly static dynamic NAry = Curry2<int, Delegate, Delegate>((length, fn) => {
            if (length <= 10) {
                return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                    object[] args;
                    var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                    length = Math.Min(length, arguments.Length);
                    args = new object[length];
                    Array.Copy(arguments, (Array)args, length);

                    return fn.InvokeWithArray(args);
                });
            }
            else {
                throw new ArgumentOutOfRangeException("length", "First argument to Arity must be a non-negative integer no greater than ten");
            }
        });

        internal readonly static dynamic Negate = Curry1<dynamic, dynamic>(n => -n);

        internal readonly static dynamic None = Curry2(new Func<object, object, dynamic>(Dispatchable2("Any", (Delegate)XAny, new Func<object, object, dynamic>((a, b) => Any(a, b)))));

        internal readonly static dynamic Not = Curry1<bool, bool>(a => !a);

        internal readonly static dynamic Nth = Curry2<int, dynamic, object>((offset, list) => {
            var count = list.GetType().Equals(typeof(string)) ? ((string)list).Length : ((IList)list).Count;
            var idx = offset < 0 ? count + offset : offset;

            return count > 0 && idx <= count ? list[idx] : null;
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

        internal readonly static dynamic Of = Curry1<object, dynamic>(OfInternal);

        internal readonly static dynamic Once = Curry1<Delegate, Delegate>(fn => {
            var called = false;
            object result = null;
            var arity = fn.Arity();

            return Arity(arity, new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                if (called) {
                    return result;
                }


                called = true;
                result = fn.DynamicInvoke(Pad(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10));
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

        internal readonly static dynamic Prop = Curry2<dynamic, object, object>((p, obj) => Object.Member(obj, p));

        internal readonly static dynamic PropIs = Curry3<Type, dynamic, object, bool>((type, name, obj) => Is(type, Object.Member(obj, name)));

        internal readonly static dynamic PropOr = Curry3<object, dynamic, object, object>((val, p, obj) => obj.IsNotNull() ? Object.Member(obj, p) ?? val : val);

        internal readonly static dynamic PropSatisfies = Curry3<Delegate, dynamic, object, bool>((pred, name, obj) => (bool)pred.Invoke((object)Object.Member(obj, name)));

        internal readonly static dynamic Props = Curry2<IEnumerable<dynamic>, object, object>((ps, obj) => {
            var result = new List<object>();

            foreach (var prop in ps) {
                object member;

                if (Object.TryGetMember(prop, obj, out member)) {
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

        internal readonly static dynamic ReduceRight = Curry3<Delegate, object, IList, object>((fn, acc, list) => {
            var idx = list.Count - 1;

            while (idx >= 0) {
                acc = fn.Invoke(acc, list[idx]);
                idx -= 1;
            }

            return acc;
        });

        internal readonly static dynamic Reduced = Currying.Curry1<object, IReduced>(ReducedInternal);

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

            return list;
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

        internal readonly static dynamic Tail = CheckForMethod1("Tail", Slice(1, int.MinValue));

        internal readonly static dynamic Take = Curry2(new Func<object, object, dynamic>(Dispatchable2("Take", (Delegate)XTake, new Func<int, IList, IList>((n, xs) => {
            return Slice(0, n < 0 ? int.MaxValue : n, xs);
        }))));

        internal readonly static dynamic TakeLastWhile = Curry2<Delegate, IList, IList>((fn, list) => {
            return TakeWhileInternal(list.Count - 1, -1, idx => idx >= 0, fn, list, idx => idx + 1, idx => int.MaxValue);
        });

        internal readonly static dynamic TakeWhile = Curry2(new Func<object, object, dynamic>(Dispatchable2("TakeWhile", (Delegate)XTakeWhile, new Func<Delegate, IList, IList>((fn, list) => {
            return TakeWhileInternal(0, 1, idx => idx < list.Count, fn, list, idx => 0, idx => idx);
        }))));

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

        internal readonly static dynamic TryCatch = Curry2<Delegate, Delegate, LambdaN>((tryer, catcher) => {
            return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                try {
                    return tryer.Invoke(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                }
                catch (Exception e) {
                    return catcher.Invoke(new object[] { e }.Concat(arguments));
                }
            });
        });

        internal readonly static dynamic Type = Curry1<object, string>(obj => obj?.GetType().Name ?? "null");

        internal readonly static dynamic Unapply = Curry1<Delegate, object>(fn => {
            return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                return fn.DynamicInvoke(arguments.Slice());
            });
        });

        internal readonly static dynamic Unary = Curry1<Delegate, Delegate>(fn => NAry(1, fn));

        internal readonly static dynamic UncurryN = Curry2<int, Delegate, object>((depth, fn) => {
            return CurryN(depth, new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                int endIdx;
                var idx = 0;
                object value = fn;
                var currentDepth = 1;
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                while (currentDepth <= depth && value.IsFunction()) {
                    var @delegate = value as Delegate;
                    var length = @delegate.Arity();

                    endIdx = currentDepth == depth ? arguments.Length : idx + length;
                    value = @delegate.DynamicInvoke((object[])arguments.Slice(idx, endIdx));
                    currentDepth += 1;
                    idx = endIdx;
                }

                return value;
            }));
        });

        internal readonly static dynamic Unfold = Curry2<Delegate, int, IList>((fn, seed) => {
            IList list;
            var pair = fn.DynamicInvoke(seed);
            IList result = new List<int>();

            while ((list = pair as IList).IsNotNull()) {
                result.Add(list[0]);
                pair = fn.DynamicInvoke(list[1]);
            }

            return result.ToArray<Array>();
        });

        internal readonly static dynamic UniqWith = Curry2<Delegate, IList, IList>((pred, list) => {
            var result = list.CreateNewList();
            var prediacte = new Func<object, object, bool>((a, b) => (bool)pred.Invoke(a, b));

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

        internal readonly static dynamic UseWith = Curry2<Delegate, IList<Delegate>, object>((fn, transformers) => {
            var length = transformers.Count;

            return CurryN(length, new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var idx = 0;
                var args = new List<object>();
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                while (idx < length) {
                    args.Add(transformers[idx].Invoke(arguments[idx]));
                    idx += 1;
                }

                return fn.Invoke((object[])args.Concat(arguments.Slice(length)));
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

        internal readonly static dynamic Clone = Curry1<object, object>(DeepCloner.Clone);

        internal readonly static dynamic Curry = Curry1<Delegate, Delegate>(fn => CurryN(fn.Arity(), fn));

        internal readonly static dynamic Drop = Curry2(new Func<object, object, dynamic>(Dispatchable2("Drop", (Delegate)XDrop, new Func<int, IList, IList>((n, xs) => Slice(Math.Max(0, n), int.MaxValue, xs)))));

        internal readonly static dynamic DropLast = Curry2(new Func<object, object, dynamic>(Dispatchable2("DropLast", (Delegate)XDropLast, new Func<int, IList, IList>(DropLastInternal))));

        internal readonly static dynamic DropLastWhile = Curry2(new Func<object, object, dynamic>(Dispatchable2("DropLastWhile", (Delegate)XDropLastWhile, new Func<Delegate, IList, IList>(DropLastWhileInternal))));

        internal new readonly static dynamic Equals = Curry2<object, object, bool>(EqualsInternal);

        internal readonly static dynamic Filter = Curry2(new Func<object, object, dynamic>(Dispatchable2("Filter", (Delegate)XFilter, new Func<Delegate, object, object>((pred, filterable) => {
            return !filterable.IsEnumerable() ? ReduceInternal(new Func<IDictionary<string, object>, string, IDictionary<string, object>>((acc, key) => {
                var value = filterable.Member(key);

                if ((bool)pred.Invoke(value)) {
                    acc[key] = value;
                }

                return acc;
            }), new ExpandoObject(), filterable.Keys()) : FilterInternal(item => (bool)pred.Invoke(item), (IList)filterable);
        }))));

        internal readonly static dynamic Flatten = Curry1(MakeFlat(true));

        internal readonly static dynamic Flip = Curry1<Delegate, Delegate>(fn => {
            return CurryN(2, new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                var args = arguments.Slice();

                args[0] = arguments[1];
                args[1] = arguments[0];

                return fn.InvokeWithArray(args.ToArray<object[]>(typeof(object)));
            }));
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

        internal readonly static dynamic Into = Curry3<object, Func<ITransformer, ITransformer>, IList, object>((acc, xf, list) => {
            var transformer = acc as ITransformer;

            if (transformer.IsNotNull()) {
                return ReduceInternal(xf(transformer), transformer.Init(), list);
            }

            return ReduceInternal(xf(StepCat(acc)), ShallowCloner.Clone(acc), list);
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

        internal readonly static dynamic Map = Curry2(new Func<object, object, dynamic>(Dispatchable2("Map", (Delegate)XMap, new Func<Delegate, object, object>((fn, functor) => {
            IList listFunctor = null;
            var functionFunctor = functor as Delegate;

            if (functionFunctor.IsNotNull()) {
                return CurryN(functionFunctor.Arity(), new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                    var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                    return fn.Invoke(functionFunctor.Invoke(arguments));
                }));
            }

            listFunctor = functor as IList;

            if (listFunctor.IsNull()) {
                return ReduceInternal(new Func<IDictionary<string, object>, string, IDictionary<string, object>>((acc, key) => {
                    acc[key] = fn.Invoke(functor.Member(key));
                    return acc;
                }), new ExpandoObject(), functor.Keys());
            }

            return MapInternal(fn, listFunctor);
        }))));

        internal readonly static dynamic MapObjIndexed = Curry2<Delegate, object, object>((fn, obj) => {
            return ReduceInternal(new Func<IDictionary<string, object>, string, object>((acc, key) => {
                acc[key] = fn.Invoke(obj.Member(key), key, obj);
                return acc;
            }), new ExpandoObject(), obj.Keys());
        });

        internal readonly static dynamic MergeWith = Curry3<Delegate, object, object, object>((fn, l, r) => {
            return MergeWithKey(new Func<object, object, object, object>((_, _l, _r) => fn.Invoke(_l, _r)), l, r);
        });

        internal readonly static dynamic Partial = CreatePartialApplicator(new Func<IList, IList, IList>(Core.Concat));

        internal readonly static dynamic PartialRight = CreatePartialApplicator(Flip(new Func<IList, IList, IList>(Core.Concat)));

        internal readonly static dynamic PathEq = Curry3<IList<string>, object, object, bool>((_path, val, obj) => Equals(Path(_path, obj), val));

        internal readonly static dynamic Pluck = Curry2<object, IList, IList>((p, list) => Map(Prop(p), list));

        internal readonly static dynamic Project = UseWith(new Func<Delegate, IList, object>(MapInternal), new[] { (Delegate)PickAll, (Delegate)Identity });

        internal readonly static dynamic PropEq = Curry3<string, object, object, bool>((name, val, obj) => Equals(val, obj.Member(name)));

        internal readonly static dynamic Reduce = Curry3<object, object, object, object>(ReduceInternal);

        internal readonly static dynamic ReduceBy = CurryN(4, Dispatchable4("ReduceBy", (Delegate)XReduceBy, new Func<Delegate, object, Delegate, IList, object>((valueFn, valueAcc, keyFn, list) => {
            return ReduceInternal(new Func<IDictionary<string, object>, object, object>((acc, elt) => {
                var key = keyFn.Invoke(elt).ToString();

                acc[key] = valueFn.Invoke(acc.ContainsKey(key) ? acc[key] : valueAcc, elt);

                return acc;
            }), new ExpandoObject(), list);
        })));

        internal readonly static dynamic ReduceWhile = CurryN(4, new Func<Delegate, Delegate, object, IList, object>((pred, fn, a, list) => {
            return ReduceInternal(new Func<object, object, object>((acc, x) => {
                return (bool)pred.Invoke(acc, x) ? fn.Invoke(acc, x) : ReducedInternal(acc);
            }), a, list);
        }));

        internal readonly static dynamic Reject = Curry2<Delegate, object, object>((pred, filterable) => {
            return Filter(Complement(pred), filterable);
        });

        internal readonly static dynamic Repeat = Curry2<object, int, IList>((value, n) => Times(Always(value), n));

        internal readonly static dynamic Sum = Reduce(Add, 0);

        internal readonly static dynamic TakeLast = Curry2<int, IList, IList>((n, xs) => Drop(n >= 0 ? xs.Count - n : 0, xs));

        internal readonly static dynamic Transduce = CurryN(4, new Func<Delegate, object, object, object, object>((xf, fn, acc, list) => {
            return ReduceInternal(xf.Invoke(fn.IsFunction() ? new XWrap((Delegate)fn) : fn), acc, list);
        }));

        internal readonly static dynamic UnionWith = Curry3<Delegate, IList, IList, IList>((pred, list1, list2) => UniqWith(pred, list1.Concat(list2)));

        internal readonly static dynamic WhereEq = Curry2<object, object, bool>((spec, testObj) => Where(Map(Equals, spec), testObj));

        internal readonly static dynamic AllPass = Curry1<IList, Delegate>(preds => CurryN(Reduce(Max, 0, Pluck("Length", preds)), AnyOrAllPass(preds, false)));

        internal readonly static dynamic AnyPass = Curry1<IList, Delegate>(preds => CurryN(Reduce(Max, 0, Pluck("Length", preds)), AnyOrAllPass(preds, true)));

        internal readonly static dynamic Ap = Curry2<object, object, object>((applicative, fn) => {
            Delegate applicativeFn = null;
            var ap = applicative.Member("Ap");

            if (ap.IsNotNull()) {
                applicativeFn = (Delegate)ap;

                return applicativeFn.Invoke(fn);
            }

            if (applicative.IsFunction()) {
                var @delegete = (Delegate)fn;

                applicativeFn = (Delegate)applicative;

                return new Func<object, object>((object x) => {
                    var result = (Delegate)applicativeFn.Invoke(x);

                    return result.Invoke(@delegete.Invoke(x));
                });
            }

            return ReduceInternal(new Func<IList, Delegate, IList>((acc, f) => acc.Concat((IList)Map(f, fn))), new object[0], applicative);
        });

        internal readonly static dynamic ApplySpec = Curry1<object, object>(ApplySpecInternal);

        internal readonly static dynamic Call = CurryParams(new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
            var fn = (Delegate)arg1;
            var arguments = Arity(arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

            return fn.InvokeWithArray(arguments);
        }));

        internal readonly static dynamic Chain = Curry2(new Func<object, object, dynamic>(Dispatchable2("Chain", (Delegate)XChain, new Func<Delegate, object, object>((fn, monad) => {
            if (monad.IsFunction()) {
                var monadFn = (Delegate)monad;

                return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                    var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);
                    var resultFn = (Delegate)monadFn.Invoke(fn.Invoke(arguments));

                    return resultFn.Invoke(arguments);
                });
            }

            return MakeFlat(false)(Map(fn, monad));
        }))));

        internal readonly static dynamic Cond = Curry1<IList, object>(pairs => {
            int arity = Reduce(Max, 0, Map(new Func<IList, int>(pair => ((Delegate)pair[0]).Arity()), pairs));

            return Arity(arity, new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                foreach (IList pair in pairs) {
                    var @delegate = (Delegate)pair[0];

                    if ((bool)@delegate.Invoke(arguments)) {
                        @delegate = (Delegate)pair[1];

                        return @delegate.Invoke(arguments);
                    }
                }

                return null;
            }));
        });

        internal readonly static dynamic ConstructN = Curry2<int, Type, object>((n, Fn) => {
            if (n > 10) {
                throw new ArgumentOutOfRangeException("Constructor with greater than ten arguments");
            }

            if (n == 0) {
                return new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                    return Fn.GetFactory(n).DynamicInvoke();
                });
            }

            return CurryN(n, new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                return Fn.GetFactory(n).DynamicInvoke((object[])arguments.Slice(0, n));
            }));
        });

        internal readonly static dynamic Converge = Curry2<Delegate, IList<Delegate>, object>((after, fns) => {
            return CurryN(Reduce(Max, 0, Pluck("Length", fns)), new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                return after.Invoke(MapInternal(new Func<Delegate, object>(fn => fn.Invoke(arguments)), fns.ToList<IList>()));
            }));
        });

        internal readonly static dynamic CountBy = ReduceBy(new Func<int, object, int>((acc, elem) => acc + 1), 0);

        internal readonly static dynamic DropRepeatsWith = Curry2(new Func<object, object, dynamic>(Dispatchable2("DropRepeatsWith", (Delegate)XDropRepeatsWith, new Func<Delegate, IList, IList>((pred, list) => {
            var idx = 1;
            var len = list.Count;
            var result = new List<object>();

            if (len != 0) {
                result.Add(list[0]);

                while (idx < len) {
                    if (!(bool)pred.Invoke((object)Last(result), list[idx])) {
                        result.Add(list[idx]);
                    }

                    idx += 1;
                }
            }

            return result.ToArray<Array>();
        }))));

        internal readonly static dynamic EqBy = Curry3<Delegate, object, object, bool>((f, x, y) => Equals(f.Invoke(x), f.Invoke(y)));

        internal readonly static dynamic EqProps = Curry3<string, object, object, bool>((prop, obj1, obj2) => Equals(obj1.Member(prop), obj2.Member(prop)));

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

            if (xs.HasMemberWhere("IndexOf", t => t.IsDelegate())) {
                return ((dynamic)xs).IndexOf(target);
            }

            return -1;
        });

        internal readonly static dynamic Juxt = Curry1<IList<Delegate>, Delegate>(fns => Converge(ArrayOf, fns));

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

        internal readonly static dynamic LiftN = Curry2<int, Delegate, Delegate>((arity, fn) => {
            var lifted = CurryN(arity, fn);

            return CurryN(arity, new LambdaN((arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10) => {
                var arguments = Arity(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8, arg9, arg10);

                return ReduceInternal(Ap, Map(lifted, arguments[0]), arguments.Slice(1));
            }));
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

        internal readonly static dynamic Partition = Juxt(new Delegate[] { Filter, Reject });
        
        internal readonly static dynamic Contains = Curry2<object, object, bool>(ContainsInternal);

        internal readonly static dynamic Concat = Curry2<object, object, IEnumerable>((a, b) => {
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

            throw new ArgumentException($"{a.GetType().Name} is not a list or string");
        });
    }
}
