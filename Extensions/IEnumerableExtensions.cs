using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal static class IEnumerableExtensions
    {
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

        internal static Array CopyToNewArray(this IList list) {
            var array = list.ToArray<Array>();

            return list.CreateNewArray(array);
        }

        internal static Array CopyToNewArray(this IList list, Type type) {
            return list.ToArray<Array>(type);
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

                elements.Cast<object>().Select(e => e.Cast(type));
                list = (IList)typeof(List<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(null);
                elements.Cast<object>().ForEach(e => list.Add(e));

                return list;
            }

            return (IList)typeof(List<>).MakeGenericType(type).GetConstructor(Type.EmptyTypes).Invoke(null);
        }

        internal static Type GetElementType(this IEnumerable enumerable) {
            var elementType = typeof(object);
            var enumerableType = enumerable.GetType();

            if (enumerableType.HasElementType) {
                elementType = enumerableType.GetElementType();
            }
            else if (enumerableType.IsGenericType) {
                elementType = enumerableType.GetGenericArguments()[0];
            }
            else {
                Type firstElementType = null;
                var list = enumerable.Cast<object>();
                var allSameType = list.Aggregate(true, (e1, e2) => {
                    firstElementType = firstElementType ?? e2.GetType();

                    return e1 && firstElementType.Equals(e2.GetType());
                });

                if (allSameType) {
                    elementType = firstElementType;
                }
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
    }
}
