using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;

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
                return Curry1(new Func<object[], dynamic>(fn.DynamicInvoke));
            }

            return Arity(length, InternalCurryN(length, new object[0], fn));
        });

        internal readonly static dynamic Add = Currying.Curry2<double, double, double>((arg1, arg2) => {
            return arg1 + arg2;
        });

        public readonly static dynamic Adjust = Currying.Curry3<Func<dynamic, dynamic>, int, IList, IList>((fn, idx, list) => {
            var start = 0;
            var index = 0;
            IList concatedList = null;

            if (idx >= list.Count || idx < -list.Count) {
                return list;
            }

            start = idx < 0 ? list.Count : 0;
            index = start + idx;
            concatedList = Core.Concat(list);
            concatedList[index] = fn(list[index]);

            return concatedList;
        });


        internal readonly static dynamic Always = Curry1<dynamic, Func<dynamic>>(value => () => value);

        internal readonly static dynamic And = Curry2<bool, bool, bool>((a, b) => a && b);

        internal readonly static dynamic All = CurryN(Core.Dispatchable2("All", new LambdaN((arguments) => null), new Func<Func<object, bool>, IList, bool>((fn, list) => AddOrAny(fn, list, false))));

        internal readonly static dynamic Any = CurryN(Core.Dispatchable2("Any", new LambdaN((arguments) => null), new Func<Func<object, bool>, IList, bool>((fn, list) => AddOrAny(fn, list, true))));

        internal readonly static dynamic Aperture = CurryN(Core.Dispatchable2("Aperture", new LambdaN((arguments) => null), new Func<int, IList, IList>(Core.Aperture)));

        internal readonly static dynamic Append = Curry2<object, IList, IList>((el, list) => Core.Concat(list, new List<object>() { el }));

        internal readonly static dynamic Apply = Curry2<LambdaN, object[], dynamic>((fn, arguments) => fn(arguments));

        internal readonly static dynamic Assoc = Curry3<string, object, object, object>((prop, val, obj) => ShallowCloner.CloneAndAssignValue(prop, val, obj));

        internal readonly static dynamic AssocPath = Curry3<IList<string>, object, object, object>((path, val, obj) => {
            switch (path.Count) {
                case 0:
                    return val;
                case 1:
                    return Assoc(path[0], val, obj);
                default:
                    return Assoc(path[0], AssocPath(Core.Slice((IList)path, 1), val, obj.Member(path[0])), obj);
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
                if (!Core.ContainsWith(pred, first[idx], second) && !Core.ContainsWith(pred, first[idx], result)) {
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

        internal readonly static dynamic DropWhile = CurryN(Core.Dispatchable2("DropWhile", new LambdaN((arguments) => null), new Func<Func<object, bool>, IList, IList>((pred, list) => {
            var idx = 0;
            var len = list.Count;

            while (idx < len && pred(list[idx])) {
                idx += 1;
            }

            return Core.Slice(list, idx);
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

        internal readonly static dynamic Find = CurryN(Core.Dispatchable2("find", new LambdaN((arguments) => null), new Func<Func<object, bool>, IList, object>((fn, list) => {
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
                    var tail = Core.Slice(path, 1);
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
