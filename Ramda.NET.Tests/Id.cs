using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ramda.NET.Tests
{
    public class _Id : IEquatable<_Id>
    {
        public static _Id Id(object x) {
            return new _Id(x);
        }

        public object Value { get; set; }

        private _Id(object x) {
            Value = x;
        }

        public _Id Ap(_Id id) {
            return new _Id(((dynamic)Value)(id.Value));
        }

        public _Id Map(dynamic f) {
            return Id(f(Value));
        }

        public object Sequence(object of) {
            return ((object[])Value).Select(i => _Id.Id(i)).ToArray<Array>();
        }

        public override bool Equals(object obj) {
            return Equals(obj as _Id);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        public override string ToString() {
            return $"Id({R.ToString(Value)})";
        }

        public bool Equals(_Id other) {
            if (other == null) {
                return false;
            }

            return Value.Equals(other.Value);
        }
    }
}
