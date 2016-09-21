using System;
using System.Linq;
using System.Dynamic;
using System.Collections;
using static Ramda.NET.Core;
using static Ramda.NET.Lambda;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Object = Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    internal static partial class Currying
    {
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

        internal readonly static dynamic Add = Curry2<double, double, double>((arg1, arg2) => arg1 + arg2);

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

        internal readonly static dynamic Dissoc = Curry2<string, object, object>((prop, obj) => ShallowCloner.CloneAndAssignDefaultValue(prop, obj));

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

        internal readonly static dynamic Find = CurryN(Dispatchable2("Find", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, object>((fn, list) => {
            return FindInternal(0, 1, idx => idx < list.Count, fn, list);
        })));

        internal readonly static dynamic FindIndex = CurryN(Dispatchable2("FindIndex", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, int>((fn, list) => {
            return FindIndexInternal(0, 1, idx => idx < list.Count, fn, list);
        })));

        internal readonly static dynamic FindLast = CurryN(Dispatchable2("FindLast", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, object>((fn, list) => {
            return FindInternal(list.Count - 1, -1, idx => idx >= 0, fn, list);
        })));

        internal readonly static dynamic FindLastIndex = CurryN(Dispatchable2("FindLastIndex", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, int>((fn, list) => {
            return FindIndexInternal(list.Count - 1, -1, idx => idx >= 0, fn, list);
        })));

        internal readonly static dynamic ForEach = Curry2(CheckForMethod2<Action<object>, IList, IList>("ForEach", (fn, list) => {
            foreach (var item in list) {
                fn(item);
            }

            return list;
        }));

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

        internal readonly static dynamic Intersperse = Curry2(CheckForMethod2<object, IList, IList>("Intersperse", (separator, list) => {
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

        internal readonly static dynamic MapAccum = Curry3<Delegate, object, IList, Tuple<object, IList>>((fn, acc, list) => MapAccumInternal(0, 1, from => from < list.Count, fn, acc, list));

        internal readonly static dynamic MapAccumRight = Curry3<Delegate, object, IList, Tuple<object, IList>>((fn, acc, list) => MapAccumInternal(list.Count - 1, -1, from => from >= 0, fn, acc, list));

        internal readonly static dynamic Match = Curry2<Regex, string, MatchCollection>((rx, str) => rx.Matches(str));

        internal readonly static dynamic MathMod = Curry2<dynamic, dynamic, dynamic>((m, p) => (m % p + p) % p);

        internal readonly static dynamic Max = Curry2<dynamic, dynamic, dynamic>((a, b) => b > a ? b : a);

        internal readonly static dynamic MaxBy = Curry3<Delegate, dynamic, dynamic, dynamic>((f, a, b) => f.DynamicInvoke(b) > f.DynamicInvoke(a) ? b : a);

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

        internal readonly static dynamic MinBy = Curry3<Delegate, dynamic, dynamic, dynamic>((f, a, b) => f.DynamicInvoke(b) < f.DynamicInvoke(a) ? b : a);

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

        internal readonly static dynamic None = Curry2(new Func<object, object, dynamic>(Dispatchable2("Any", new LambdaN(argumnets => null), new Func<object, object, dynamic>((a, b) => Any(a, b)))));

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
            return lens(y => IdentityInternal(f.DynamicInvoke(y)))(x).Value;
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

            foreach (var keyVal in obj.ToMemberDictionary()) {
                if ((bool)pred.DynamicInvoke(keyVal.Value, keyVal.Key, obj)) {
                    result[keyVal.Key] = keyVal.Value;
                }
            }

            return result;
        });

        internal readonly static dynamic Prepend = Curry2<object, IList, IList>((el, list) => Concat(new[] { el }, list));

        internal readonly static dynamic Prop = Curry2<dynamic, object, object>((p, obj) => Member(obj, p));

        internal readonly static dynamic PropIs = Curry3<Type, dynamic, object, bool>((type, name, obj) => Is(type, Member(obj, name)));

        internal readonly static dynamic PropOr = Curry3<object, dynamic, object, object>((val, p, obj) => obj.IsNotNull() ? Member(obj, p) ?? val : val);

        internal readonly static dynamic PropSatisfies = Curry3<Delegate, dynamic, object, bool>((pred, name, obj) => (bool)pred.DynamicInvoke(Member(obj, name)));

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

        internal readonly static dynamic ReduceRight = Curry3<Delegate, object, IList, object>((fn, acc, list) => ReduceInternal(list.Count - 1, -1, (from) => from >= 0, fn, acc, list));

        internal readonly static dynamic Reduced = Curry1<object, Reduced>(ReducedInternal);

        internal readonly static dynamic Remove = Curry3<int, int, IList, IList>((start, count, list) => {
            return Concat(Slice(list, 0, Math.Min(start, list.Count)), Slice(list, Math.Min(list.Count, start + count)));
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

        internal readonly static dynamic Set = Curry3<Func<Func<object, IdentityObj>>, object, object, object>((lens, v, x) => {
            return Over(lens, Always(v), x);
        });

        internal readonly static dynamic Slice = Curry3(CheckForMethod3("Slice", new Func<int, int, IList, IList>((fromIndex, toIndex, list) => Core.Slice(list, fromIndex, toIndex))));

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
            var result = list.CreateNewListOfType(list.GetType());

            while (idx < list.Count) {
                result.Add(Slice(idx, idx += n, list));
            }

            return result;
        });

        internal readonly static dynamic SplitWhen = Curry2<Delegate, IList, IList>((pred, list) => {
            var idx = 0;
            var len = list.Count;
            var prefix = list.CreateNewList();
            var result = list.CreateNewListOfType(prefix.GetType());

            if (list.IsArray()) {
                list = list.CreateNewList(list);
            }

            while (idx < len && !(bool)pred.DynamicInvoke(list[idx])) {
                prefix.Add(list[idx]);
                idx += 1;
            }

            result.Add(prefix);
            result.Add(Core.Slice(list, idx));

            return result;
        });

        internal readonly static dynamic Subtract = Curry2<double, double, double>((arg1, arg2) => arg1 - arg2);

        internal readonly static dynamic Tail = CheckForMethod1("Tail", Slice(1, int.MinValue));

        internal readonly static dynamic Take = Curry2(new Func<object, object, dynamic>(Dispatchable2("Take", new LambdaN(arguments => null), new Func<int, IList, IList>((n, xs) => {
            return Slice(0, n < 0 ? int.MaxValue : n, xs);
        }))));

        internal readonly static dynamic TakeLastWhile = Curry2<Delegate, IList, IList>((fn, list) => {
            return TakeWhileInternal(list.Count - 1, -1, idx => idx >= 0, fn, list, idx => idx + 1, idx => int.MaxValue);
        });

        internal readonly static dynamic TakeWhile = Curry2(new Func<object, object, dynamic>(Dispatchable2("TakeWhile", new LambdaN(arguments => null), new Func<Delegate, IList, IList>((fn, list) => {
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
                list[idx] = fn.DynamicInvoke(idx);
                idx += 1;
            }

            return list;
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

            return outerlist;//.CreateNewArray( result.Select(list => list.ToArray()).ToArray();
        });
    }
}