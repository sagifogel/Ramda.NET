﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace Ramda.NET
{
    internal static class IEnumerableExtensions
    {
        internal static IEnumerable Slice(this IEnumerable arguments, int from = int.MinValue, int to = int.MaxValue, Type type = null) {
            var enumerableOfString = arguments as string;

            if (enumerableOfString != null) {
                return enumerableOfString.Slice(from, to, type);
            }

            return (arguments as IList).Slice(from, to, type);
        }

        internal static string Slice(this string arguments, int from = int.MinValue, int to = int.MaxValue, Type type = null) {
            return string.Join(string.Empty, arguments.ToCharArray().Slice(from, to, type).Select(o => o.ToString()));
        }

        internal static IList Slice(this IList arguments, int from = int.MinValue, int to = int.MaxValue, Type type = null) {
            if (from == int.MinValue) {
                return arguments.Slice(0, arguments.Count, type);
            }
            else if (to == int.MaxValue) {
                return arguments.Slice(from, arguments.Count, type);
            }
            else {
                IList result;
                var len = Math.Max(0, Math.Min(arguments.Count, to) - from);

                if (arguments.IsArray()) {
                    var arr = arguments.CreateNewArray(len, type);

                    Array.Copy((Array)arguments, Math.Min(from, arguments.Count), arr, 0, len);
                    result = arr;
                }
                else {
                    var idx = 0;

                    result = arguments.CreateNewList(type: type);

                    while (idx < len) {
                        result.Add(arguments[from + idx]);
                        idx += 1;
                    }
                }

                return result;
            }
        }

        internal static Array Concat(this IList set1, IList set2 = null) {
            return ConcatInternal(set1, set2);
        }

        internal static Array ConcatInternal(IList set1, IList set2 = null) {
            var list1ElemType = set1.GetElementType();
            var list2ElemType = set2?.GetElementType() ?? list1ElemType;
            var result = list1ElemType.Equals(list2ElemType) ? set1.CreateNewList() : new List<object>();

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

            return result.ToArray<Array>();
        }

        internal static IEnumerable<TResult> Select<TResult>(this IList source, Func<object, TResult> selector) {
            foreach (var tSource in source) {
                yield return selector(tSource);
            }
        }

        internal static IEnumerable<TResult> Select<TResult>(this IList source, Func<object, int, TResult> selector) {
            for (int i = 0; i < source.Count; i++) {
                yield return selector(source[i], i);
            }
        }

        internal static Dictionary<TKey, TElement> ToDictionary<TKey, TElement>(this IList source, Func<object, int, TKey> keySelector, Func<object, int, TElement> elementSelector) {
            int num = -1;
            Dictionary<TKey, TElement> tKeys = new Dictionary<TKey, TElement>();

            foreach (var tSource in source) {
                num++;
                tKeys.Add(keySelector(tSource, num), elementSelector(tSource, num));
            }

            return tKeys;
        }

        internal static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action) {
            foreach (var item in source) {
                action(item);
            }
        }

        internal static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource, int> action) {
            var count = 0;

            foreach (var item in source) {
                action(item, count++);
            }
        }

        internal static void ForEach(this ICollection source, Action<object> action) {
            foreach (var item in source) {
                action(item);
            }
        }

        internal static void ForEach(this IList source, Action<object, int> action) {
            var count = 0;

            foreach (var item in source) {
                action(item, count++);
            }
        }

        internal static void ForEach(this IList source, Action<object> action) {
            foreach (var item in source) {
                action(item);
            }
        }

        internal static Array CopyToNewArray(this IList list, int? len = null, Type type = null) {
            var newArray = list.CreateNewArray(len ?? list.Count, type);

            list.CopyTo(newArray, 0);

            return newArray;
        }

        internal static Array CreateNewArray(this IEnumerable list, int len, Type type = null) {
            return (type ?? list.GetElementType()).CreateNewArray<Array>(len);
        }

        internal static ListType CreateNewArray<ListType>(this Type type, int len) {
            return (ListType)type.MakeArrayType(1).GetConstructors()[0].Invoke(new object[] { len }); ;
        }

        internal static ListType CreateNewList<ListType>(this Type type) {
            return (ListType)typeof(List<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
        }

        internal static IList CreateNewList(this IEnumerable enumerable, IEnumerable elements = null, Type type = null) {
            type = type ?? enumerable.GetElementType();

            if (elements != null) {
                IList list = null;
                var elementType = elements.GetElementType();

                if (!elementType.Equals(type)) {
                    type = typeof(object);
                }

                elements.Cast<object>().Select(e => e.Cast(type));
                list = (IList)typeof(List<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(null);
                elements.Cast<object>().ForEach(e => list.Add(e));

                return list;
            }

            return (IList)typeof(List<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        internal static Type GetElementType(this IEnumerable enumerable) {
            Type elementType = null;
            var enumerableType = enumerable.GetType();

            if (enumerableType.HasElementType) {
                elementType = enumerableType.GetElementType();

                if (elementType != typeof(object)) {
                    return elementType;
                }
            }

            if (enumerableType.IsGenericType) {
                var genericArgs = enumerableType.GetGenericArguments();

                return genericArgs[genericArgs.Length - 1];
            }

            elementType = FindElementType(enumerable);

            if (elementType == null) {
                elementType = typeof(object);
            }

            return elementType;
        }

        private static Type FindElementType(this IEnumerable enumerable) {
            Type firstElementType = null;
            var elementType = typeof(object);
            var list = enumerable.Cast<object>();
            var allSameType = list.Aggregate(true, (e1, e2) => {
                Type type = e2?.GetType() ?? typeof(object);

                firstElementType = firstElementType ?? type;

                return e1 && firstElementType.Equals(type);
            });

            if (allSameType) {
                elementType = firstElementType;
            }

            return elementType;
        }

        internal static object[] ToArgumentsArray(this object value, Type type = null) {
            type = type ?? value.GetType();

            if (type.TypeIsArray()) {
                value = value.ToArgumentsArray(type.GetElementType());
            }

            return new[] { value };
        }

        internal static TArray ToArray<TArray>(this ICollection list, Type type = null) where TArray : IList {
            return list.ToArray<TArray>(list.Count, type);
        }

        internal static TArray ToArray<TArray>(this IEnumerable list, int len, Type type = null) where TArray : IList {
            int i = 0;
            IList arr = list.CreateNewArray(len, type);

            foreach (var item in list) {
                arr[i] = item;
                i++;
            }

            return (TArray)arr;
        }

        internal static TArray ToArray<TArray>(this IEnumerable list, Type type = null) where TArray : IList {
            return list.ToList<IList>().ToArray<TArray>(type);
        }

        internal static TList ToList<TList>(this IEnumerable list, Type type = null) where TList : IList {
            IList result = list.CreateNewList(type: type);

            foreach (var item in list) {
                result.Add(item);
            }

            return (TList)result;
        }

        internal static TArray ToArray<TArray>(this ICollection<object> list, Type type = null) where TArray : IList {
            return list.ToArray<TArray>(list.Count);
        }

        internal static bool SequenceEqual(this IEnumerable first, IEnumerable second, Func<object, object, bool> comparer) {
            bool flag;
            var enumerator = first.GetEnumerator();
            var enumerator1 = second.GetEnumerator();

            while (enumerator.MoveNext()) {
                if (enumerator1.MoveNext() && comparer(enumerator.Current, enumerator1.Current)) {
                    continue;
                }

                return false;
            }
            if (!enumerator1.MoveNext()) {
                return true;
            }
            else {
                flag = false;
            }

            return flag;
        }

        internal static TResult[] Sort<TResult>(this IList list, Comparison<TResult> comparison) {
            var newList = new List<TResult>(list.Cast<TResult>());

            newList.Sort(comparison);

            return newList.ToArray();
        }

        internal static IEnumerable GetDictionaryValues(this object val) {
            var dictionary = val as IDictionary;

            if (dictionary != null) {
                return dictionary.Values;
            }

            if (val.IsExpandoObject()) {
                return ((IDictionary<string, object>)val).Values;
            }

            return ((dynamic)val).Values;
        }

        internal static object[] Unwrap(this object[] arr, Delegate @delegate) {
            IEnumerable<ParameterInfo> delegateParams = @delegate.Method.GetParameters();
            var @params = delegateParams.Select(p => p.ParameterType).ToArray();

            for (int i = 0; i < arr.Length; i++) {
                var dynamicDelegate = arr[i] as DynamicDelegate;
                var param = @params[i];

                if (dynamicDelegate != null && param.TypeIsDelegate()) {
                    arr[i] = dynamicDelegate.Unwrap();
                }
            }

            return arr;
        }
    }
}
