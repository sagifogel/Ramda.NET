using System;
using System.Collections;
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
            return new Lambda1(arg1 => {
                if (R.__.Equals(arg1) || arg1 == null) {
                    return Curry1(fn);
                }

                return fn(arg1.CastTo<TArg1>());
            });
        }

        internal static dynamic Curry2<TArg1, TArg2, TResult>(Func<TArg1, TArg2, TResult> fn) {
            return new Lambda2((arg1, arg2) => {
                bool arg1IsPlaceHolder = false;
                bool arg2IsPlaceHolder = false;

                switch (FunctionArity(arg1, arg2)) {
                    case 0:
                        return Curry2(fn);
                    case 1:
                        return IsPlaceholder(arg1) ? Curry2(fn) : Curry1<TArg2, TResult>(_arg2 => fn(arg1.CastTo<TArg1>(), _arg2));
                    default:
                        return (arg1IsPlaceHolder = IsPlaceholder(arg1)) && (arg2IsPlaceHolder = IsPlaceholder(arg2)) ? Curry2(fn) : arg1IsPlaceHolder ? Curry1<TArg1, TResult>(_arg1 => {
                            return fn(_arg1, arg2.CastTo<TArg2>());
                        }) : arg2IsPlaceHolder ? Curry1<TArg2, TResult>(_arg2 => {
                            return fn(arg1.CastTo<TArg1>(), _arg2);
                        }) : fn(arg1.CastTo<TArg1>(), arg2.CastTo<TArg2>());
                }
            });
        }

        internal static dynamic Curry3<TArg1, TArg2, TArg3, TResult>(Func<TArg1, TArg2, TArg3, TResult> fn) {
            return new Lambda3((arg1, arg2, arg3) => {
                bool arg1IsPlaceHolder = false;
                bool arg2IsPlaceHolder = false;
                bool arg3IsPlaceHolder = false;

                switch (FunctionArity(arg1, arg2, arg3)) {
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
            int start = 0;
            int index = 0;
            IList concatedList = null;

            if (idx >= list.Count || idx < -list.Count) {
                return list;
            }

            start = idx < 0 ? list.Count : 0;
            index = start + idx;
            concatedList = Core.InternalConcat(list);
            concatedList[index] = fn(list[index]);

            return concatedList;
        });

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
