using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET
{
    public interface IGroupWithStrategy
    {
        int Length { get;}
        object Slice(int from, int to);
        object this[int index] { get; }
    }

    public static class GroupByStrategy
    {
        public static IGroupWithStrategy Resolve(IEnumerable enumerable) {
            var @string = enumerable as string;

            if (@string != null) {
                return new GroupWithOfString(@string);
            }

            return new GroupWithOfList((IList)enumerable);
        }

        private class GroupWithOfString : IGroupWithStrategy
        {
            private readonly string value;

            public GroupWithOfString(string value) {
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

        private class GroupWithOfList : IGroupWithStrategy
        {
            private readonly IList list;

            public GroupWithOfList(IList list) {
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
