using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using static Ramda.NET.Currying;

namespace Ramda.NET
{
    internal static partial class Core
    {
        internal static Func<object[], bool> Complement(Func<object[], bool> fn) {
            return (arguments => !fn.Invoke(arguments));
        }

        internal static IList Concat(IList set1, IList set2 = null) {
            var result = new List<object>();

            if (set1 != null) {
                foreach (var item in set1) {
                    result.Add(item);
                }
            }

            if (set2 != null) {
                foreach (var item in set2) {
                    result.Add(item);
                }
            }

            return result;
        }

        public static bool ContainsWith(Func<object, object, bool> predicate, object x, IList list) {
            foreach (var item in list) {
                if (predicate(x, item)) {
                    return true;
                }
            }

            return false;
        }

        private static IList Filter(Func<object, bool> fn, IList list) {
            var result = new List<object>();

            foreach (var item in list) {
                if (fn(item)) {
                    result.Add(item);
                }
            }

            return result;
        }

        internal static IList Slice(IList arguments, int from = int.MinValue, int to = int.MinValue) {
            if (from == int.MinValue) {
                return Slice(arguments, 0, arguments.Count);
            }
            else if (to == int.MinValue) {
                return Slice(arguments, from, arguments.Count);
            }
            else {
                IList result;
                var len = Math.Max(0, Math.Min(arguments.Count, to) - from);

                if (arguments.IsArray()) {
                    result = arguments.CreateNewArray(len);
                    Array.Copy((Array)arguments, from, (Array)result, 0, len);
                }
                else {
                    result = arguments.CreateNewList();
                    to = len + from;

                    for (int i = from; i < to; i++) {
                        result.Add(result[i]);
                    }
                }

                return result;
            }
        }

        internal static LambdaN Dispatchable(LambdaN transducerFunction, Delegate fn) {
            return new LambdaN(arguments => {
                if (arguments.Arity() != 1) {
                    return fn.DynamicInvoke(new object[0]);
                }

                return fn.DynamicInvoke(arguments[0]);
            });
        }

        internal static LambdaN Dispatchable2(string methodName, LambdaN transducerFunction, Delegate fn) {
            return new LambdaN(arguments => {
                var length = arguments.Arity();

                if (length == 2) {
                    return fn.DynamicInvoke(new object[0]);
                }

                var arg1 = arguments[0];
                var arg2 = arguments[2];

                if (!arg2.GetType().IsArray) {
                    var members = arg2.GetType().GetMember(methodName);

                    if (members.Length == 1) {
                        var member = members[0];

                        if (member.MemberType == MemberTypes.Method) {
                            return ((MethodInfo)member).Invoke(arg2, new[] { arg1 });
                        }
                    }

                    if (arg2 is ITransducer) {
                        var transducer = transducerFunction(arg1);

                        return transducer(arg2);
                    }
                }

                return fn.DynamicInvoke(arg1, arg2);
            });
        }

        private static IList DropLastWhile(Func<object, bool> predicate, object x, IList list) {
            var idx = list.Count - 1;

            while (idx >= 0 && predicate(list[idx])) {
                idx -= 1;
            }

            return Slice(list, 0, idx + 1);
        }

        internal static IList Aperture(int length, IList list) {
            var idx = 0;
            var limit = list.Count - (length - 1);
            IList acc = null;

            limit = limit >= 0 ? limit : 0;
            acc = list.IsArray() ? (IList)new object[limit] : new List<object>(limit);

            while (idx < limit) {
                acc[idx] = Slice(list, idx, idx + length);
                idx += 1;
            }

            return acc;
        }

        internal static LambdaN CheckForMethod(string methodName, Delegate fn) {
            return new LambdaN((arguments) => {
                object obj;
                object member;
                Type memberType;
                var length = arguments.Length;

                if (length == 0) {
                    return fn.DynamicInvoke();
                }

                obj = arguments[length - 1];
                member = obj.Member(methodName);
                memberType = member.GetType();

                if (memberType.IsArray || !memberType.IsFunction()) {
                    return fn.DynamicInvoke(arguments);
                }

                return ((Delegate)member).DynamicInvoke(obj, Slice(arguments, 0, length - 1));
            });
        }

        internal static bool Has(string prop, object obj) {
            return obj.TryGetMember(prop).IsNotNull();
        }

        internal static TValue Identity<TValue>(TValue x) {
            return x;
        }
    }
}
