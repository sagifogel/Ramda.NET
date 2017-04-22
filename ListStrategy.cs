using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET
{
    public interface IListStrategy
    {
        int Length { get; }
        object Slice(int from, int to);
        object this[int index] { get; }
    }

    public static class ListStrategy
    {
        public static IListStrategy Resolve(IEnumerable enumerable) {
            var @string = enumerable as string;

            if (@string != null) {
                return new StringListStrategy(@string);
            }

            return new ListStrategyImpl((IList)enumerable);
        }

        private class StringListStrategy : IListStrategy
        {
            private readonly string value;

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
        }
    }
}
