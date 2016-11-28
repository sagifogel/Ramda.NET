using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal static class IEnumerableExtensions
    {
        internal static IList Slice(this IList arguments, int from = int.MinValue, int to = int.MaxValue) {
            if (from == int.MinValue) {
                return arguments.Slice(0, arguments.Count);
            }
            else if (to == int.MaxValue) {
                return arguments.Slice(from, arguments.Count);
            }
            else {
                IList result;
                var len = Math.Max(0, Math.Min(arguments.Count, to) - from);

                if (arguments.IsArray()) {
                    result = arguments.CreateNewArray(len);
                    Array.Copy((Array)arguments, from, (Array)result, 0, len);
                }
                else {
                    var idx = 0;

                    result = arguments.CreateNewList();

                    while (idx < len) {
                        result.Add(arguments[from + idx]);
                        idx += 1;
                    }
                }

                return result;
            }
        }

        internal static IList Concat(this IList set1, IList set2 = null) {
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

        public static Dictionary<TKey, TElement> ToDictionary<TKey, TElement>(this IList source, Func<object, int, TKey> keySelector, Func<object, int, TElement> elementSelector) {
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

        public static IEnumerable<TSource> Distinct<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> predicate) {
            var keys = new HashSet<TKey>();

            return source.Where(element => keys.Add(predicate(element)));
        }

        internal static Array CreateNewArray(this IList list, int? len = null, Type type = null) {
            return (type ?? list.GetElementType()).CreateNewArray<Array>(len ?? list.Count);
        }

        internal static Array CreateNewArray(this IList list, Array sourceToCopy) {
            var array = list.CreateNewArray(sourceToCopy.Length);

            sourceToCopy.CopyTo(array, 0);

            return array;
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
            var enumerableType = enumerable.GetType();

            if (enumerableType.HasElementType) {
                return enumerableType.GetElementType();
            }

            if (enumerableType.IsGenericType) {
                return enumerableType.GetGenericArguments()[0];
            }
            return FindElementType(enumerable);
        }

        private static Type FindElementType(this IEnumerable enumerable) {
            Type firstElementType = null;
            var elementType = typeof(object);
            var list = enumerable.Cast<object>();
            var allSameType = list.Aggregate(true, (e1, e2) => {
                firstElementType = firstElementType ?? e2.GetType();

                return e1 && firstElementType.Equals(e2.GetType());
            });

            if (allSameType) {
                elementType = firstElementType;
            }

            return elementType;
        }

        internal static bool IsJaggedArray(this object[] arr) {
            return arr.GetType().GetElementType().IsArray;
        }

        internal static object[] ToArgumentsArray(this object value, Type type = null) {
            type = type ?? value.GetType();

            if (type.IsArray) {
                value = value.ToArgumentsArray(type.GetElementType());
            }

            return new[] { value };
        }

        internal static TArray ToArray<TArray>(this IList list, Type type = null) where TArray : IList {
            IList arr = list.CreateNewArray(list.Count, type);

            for (int i = 0; i < list.Count; i++) {
                arr[i] = list[i];
            }

            return (TArray)arr;
        }

        internal static Array NormalizeArray(this IList list)  {
            var type = list.FindElementType();
            IList arr = list.CreateNewArray(list.Count, type);

            for (int i = 0; i < list.Count; i++) {
                arr[i] = list[i];
            }

            return (Array)arr;
        }

        internal static TList ToList<TList>(this IEnumerable list, Type type = null) where TList : IList {
            IList result = list.CreateNewList(type: type);

            foreach (var item in list) {
                result.Add(item);
            }

            return (TList)result;
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
    }
}
