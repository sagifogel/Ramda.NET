﻿using System;
using System.Dynamic;
using System.Collections;
using static Ramda.NET.Core;
using System.Collections.Generic;

namespace Ramda.NET
{
    public static partial class Currying
    {
        public delegate dynamic Lambda1(object arg = null);
        public delegate dynamic LambdaN(params object[] arguments);
        public delegate dynamic Lambda2(object arg1 = null, object arg2 = null);
        public delegate dynamic Lambda3(object arg1 = null, object arg2 = null, object arg3 = null);

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
                        return IsPlaceholder(arg1 = arguments[0]) ? Curry2(fn) : Curry1<TArg2, TResult>(_arg2 => fn(arg1.CastTo<TArg1>(), _arg2));
                    default:
                        return (arg1IsPlaceHolder = IsPlaceholder(arg1 = arguments[0])) && (arg2IsPlaceHolder = IsPlaceholder(arg2 = arguments[0])) ? Curry2(fn) : arg1IsPlaceHolder ? Curry1<TArg1, TResult>(_arg1 => {
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
                        return (arg1IsPlaceHolder = IsPlaceholder(arg1)) && (arg2IsPlaceHolder = IsPlaceholder(arg2)) ? Curry3(fn) : arg1IsPlaceHolder ? Curry2<TArg1, TArg3, TResult>((_arg1, _arg3) => {
                            return fn(_arg1, arg2.CastTo<TArg2>(), _arg3);
                        }) : arg2IsPlaceHolder ? Curry2<TArg2, TArg3, TResult>((_arg2, _arg3) => {
                            return fn(arg1.CastTo<TArg1>(), _arg2, _arg3);
                        }) : Curry1<TArg3, TResult>(_arg3 => {
                            return fn(arg1.CastTo<TArg1>(), arg2.CastTo<TArg2>(), _arg3);
                        });
                    default:
                        return (arg1IsPlaceHolder = IsPlaceholder(arg1)) && (arg2IsPlaceHolder = IsPlaceholder(arg2)) && (arg3IsPlaceHolder = IsPlaceholder(arg3)) ? Curry3(fn) : arg1IsPlaceHolder && arg2IsPlaceHolder ? Curry2<TArg1, TArg2, TResult>((_arg1, _arg2) => {
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
                return Curry1(new Func<object, object>(arg => fn.DynamicInvoke(new[] { arg })));
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

        internal readonly static dynamic All = CurryN(Dispatchable2("All", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, bool>((fn, list) => AddOrAny(fn, list, false))));

        internal readonly static dynamic Any = CurryN(Dispatchable2("Any", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, bool>((fn, list) => AddOrAny(fn, list, true))));

        internal readonly static dynamic Aperture = CurryN(Dispatchable2("Aperture", new LambdaN(arguments => null), new Func<int, IList, IList>(Core.Aperture)));

        internal readonly static dynamic Append = Curry2<object, IList, IList>((el, list) => Concat(list, new List<object>() { el }));

        internal readonly static dynamic Apply = Curry2<LambdaN, object[], dynamic>((fn, arguments) => fn(arguments));

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

        internal readonly static dynamic Clamp = Curry3<double, double, double, double>((min, max, value) => {
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
            return ReferenceEquals(value, null) ? defaultValue : value;
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

        internal readonly static dynamic Divide = Curry2<double, double, double>((a, b) => a / b);

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

                if (type.IsClass && !type.IsFunction()) {
                    x.GetFactory().Invoke();
                }
            }

            return null;
        });

        internal readonly static dynamic Evolve = Curry2<IDictionary<string, object>, object, object>(InternalEvolve);

        internal readonly static dynamic Find = CurryN(Dispatchable2("find", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, object>((fn, list) => {
            var idx = 0;
            var len = list.Count;

            while (idx < len) {
                if (fn(list[idx])) {
                    return list[idx];
                }

                idx += 1;
            }

            return null;
        })));

        internal readonly static dynamic FindIndex = CurryN(Dispatchable2("FindIndex", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, int>((fn, list) => {
            var idx = 0;
            var len = list.Count;

            while (idx < len) {
                if (fn(list[idx])) {
                    return idx;
                }

                idx += 1;
            }

            return -1;
        })));

        internal readonly static dynamic FindLast = CurryN(Dispatchable2("FindLast", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, object>((fn, list) => {
            var idx = list.Count - 1;

            while (idx >= 0) {
                if (fn(list[idx])) {
                    return list[idx];
                }

                idx -= 1;
            }

            return null;
        })));

        internal readonly static dynamic FindLastIndex = CurryN(Dispatchable2("FindLastIndex", new LambdaN(arguments => null), new Func<Func<object, bool>, IList, int>((fn, list) => {
            var idx = list.Count - 1;

            while (idx >= 0) {
                if (fn(list[idx])) {
                    return idx;
                }

                idx -= 1;
            }

            return -1;
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

        internal readonly static dynamic FromPairs = Curry1<object[][], object>((pairs) => {
            var idx = 0;
            IDictionary<string, object> result = new ExpandoObject();

            while (idx < pairs.Length) {
                var key = pairs[idx][0];

                if (key is string) {
                    result[pairs[idx][0].ToString()] = pairs[idx][1];
                }

                idx += 1;
            }

            return result;
        });

        internal readonly static dynamic GroupWith = Curry2<Func<object, object, bool>, IList, IList>((fn, list) => {
            var idx = 0;
            var len = list.Count;
            var res = new List<object>();

            while (idx < len) {
                var nextidx = idx + 1;

                while (nextidx < len && fn(list[idx], list[nextidx])) {
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

        internal readonly static dynamic IfElse = Curry3<LambdaN, LambdaN, LambdaN, LambdaN>((condition, onTrue, onFalse) => {
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

        private static object InternalIfElse(LambdaN condition, LambdaN onTrue, LambdaN onFalse, params object[] arguments) {
            return (bool)condition.Invoke(arguments) ? onTrue.Invoke(arguments) : onFalse.Invoke(arguments);
        }

        private static object InternalEvolve(IDictionary<string, object> transformations, object target) {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var keyValue in target.ToMemberDictionary()) {
                object transformation;
                var key = keyValue.Key;
                var value = keyValue.Value;

                if (transformations.TryGetValue(key, out transformation)) {
                    var transformationType = transformation.GetType();

                    if (transformationType.IsFunction()) {
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

        private static bool AddOrAny(Func<object, bool> fn, IList list, bool returnValue) {
            var idx = 0;

            while (idx < list.Count) {
                if (fn(list[idx])) {
                    return returnValue;
                }

                idx += 1;
            }

            return !returnValue;
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

                return left <= 0 ? fn.DynamicInvoke(combined.ToArray()) : Arity(left, InternalCurryN(length, combined.ToArray(), fn));
            });
        }
    }
}
