using System;
using System.Dynamic;
using System.Collections;
using static Ramda.NET.Core;
using static Ramda.NET.Lambda;
using System.Collections.Generic;
using Object = Ramda.NET.ReflectionExtensions;

namespace Ramda.NET
{
    internal static partial class Currying
    {
        internal class IdentityObj
        {
            public object Value { get; set; }
            public Delegate Map { get; set; }
        }

        private static Tuple<object, IList> MapAccumInternal(int from, int indexerAcc, Func<int, bool> loopPredicate, Delegate fn, object acc, IList list) {
            var tuple = R.Tuple.Create(acc, null);
            IList result = new object[list.Count];

            while (loopPredicate(from)) {
                tuple = (R.Tuple)fn.DynamicInvoke(tuple.Item1, list[from]);
                result[from] = tuple.Item1;
                from += indexerAcc;
            }

            return Tuple.Create(tuple.Item1, result);
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
            foreach (var item in list) {
                if ((bool)fn.DynamicInvoke(item)) {
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

        private static Reduced ReducedInternal(object x) {
            var reduced = x as Reduced;

            return reduced.IsNotNull() && reduced.IsReduced ? reduced : new Reduced(x);
        }

        private static IdentityObj IdentityInternal(object x) {
            return new IdentityObj() {
                Value = x,
                Map = new Func<Delegate, IdentityObj>(f => {
                    return IdentityInternal(f.DynamicInvoke(x));
                })
            };
        }
    }
}
