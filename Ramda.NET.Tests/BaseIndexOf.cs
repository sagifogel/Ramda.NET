using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    [TestClass]
    public class BaseIndexOf
    {
        protected interface IIndexOf
        {
            int IndexOf(object x);
            int LastIndexOf(object x);
        }

        protected class StringIndexOf : IIndexOf, IEquatable<StringIndexOf>
        {
            private string Value { get; set; }

            public StringIndexOf(string value) {
                Value = value;
            }

            public int IndexOf(object x) {
                if (x is string) {
                    return Value.IndexOf((string)x);
                }

                return -1;
            }

            public override bool Equals(object obj) {
                var str = obj as string;

                if (str != null) {
                    return Value.Equals(str);
                }

                return Equals(obj as StringIndexOf);
            }

            public override int GetHashCode() {
                return Value.GetHashCode();
            }

            public bool Equals(StringIndexOf other) {
                if (other != null) {
                    return false;
                }

                return Value.Equals(other.Value);
            }

            public int LastIndexOf(object x) {
                if (x is string) {
                    return Value.LastIndexOf((string)x);
                }

                return -1;
            }
        }

        protected class Empty : IIndexOf
        {
            private dynamic indexOf => R.Always(-1);

            public int IndexOf(object x) {
                return indexOf(x);
            }

            public int LastIndexOf(object x) {
                return indexOf(x);
            }
        }

        protected class List : IIndexOf
        {
            public IIndexOf Head { get; set; }
            public IIndexOf Tail { get; set; }

            public List(string head, IIndexOf tail) {
                Head = new StringIndexOf(head);
                Tail = tail;
            }

            public List(IIndexOf head, IIndexOf tail) {
                Head = head;
                Tail = tail;
            }

            public int IndexOf(object x) {
                var idx = Tail.IndexOf(x);

                return Head.Equals(x) ? 0 : idx >= 0 ? 1 + idx : -1;
            }

            public int LastIndexOf(object x) {
                var idx = Tail.LastIndexOf(x);

                return idx >= 0 ? 1 + idx : Head.Equals(x) ? 0 : -1;
            }
        }
    }
}
