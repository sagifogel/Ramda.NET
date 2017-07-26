using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET
{
    /// <summary>
    ///  Provides a unified contract for list or string manipulation
    /// </summary>
    public interface IListStrategy
    {
        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <value>
        /// The length.
        /// </value>
        int Length { get; }
        /// <summary>
        /// Slices the specified list.
        /// </summary>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        /// <returns></returns>
        object Slice(int from, int to);
        /// <summary>
        /// Gets the <see cref="System.Object"/> at the specified index.
        /// </summary>
        /// <value>
        /// The <see cref="System.Object"/>.
        /// </value>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        object this[int index] { get; }
        /// <summary>
        /// Gets the type of the element.
        /// </summary>
        /// <returns></returns>
        Type GetElementType();
        /// <summary>
        /// Adds the specified item.
        /// </summary>
        /// <param name="item">The item.</param>
        void Add(object item);
    }

    internal static class ListStrategy
    {
        public static IListStrategy New(IEnumerable enumerable) {
            return ResolveInternal(enumerable, true);
        }

        public static IListStrategy Resolve(IEnumerable enumerable) {
            return ResolveInternal(enumerable);
        }

        public static IListStrategy ResolveInternal(IEnumerable enumerable, bool isNew = false) {
            var @string = enumerable as string;

            if (@string != null) {
                return new StringListStrategy(isNew ? string.Empty : @string);
            }

            return new ListStrategyImpl(isNew ? new ArrayList() : (IList)enumerable);
        }

        private class StringListStrategy : IListStrategy
        {
            private string value;

            public StringListStrategy(string value) {
                this.value = value;
            }

            public object this[int index] {
                get {
                    return value[index];
                }
            }

            public int Length {
                get {
                    return value.Length;
                }
            }

            public void Add(object item) {
                value += item;
            }

            public Type GetElementType() {
                return typeof(string);
            }

            public object Slice(int from, int to) {
                return value.Slice(from, to);
            }
        }

        private class ListStrategyImpl : IListStrategy
        {
            private readonly IList list;

            public ListStrategyImpl(IList list) {
                this.list = list;
            }

            public object this[int index] {
                get {
                    return list[index];
                }
            }

            public int Length {
                get {
                    return list.Count;
                }
            }

            public object Slice(int from, int to) {
                return list.Slice(from, to);
            }

            public Type GetElementType() {
                return typeof(object);
            }

            public void Add(object item) {
                list.Add(item);
            }
        }
    }
}
