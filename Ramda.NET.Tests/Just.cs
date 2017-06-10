using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET.Tests
{
    public class Just : _Maybe, IEquatable<Just>, IAppable, IMappable
    {
        public Just(object value) : base(value) {
        }

        public override bool Equals(object obj) {
            return Equals(obj as Just);
        }

        public override bool Equals(Just other) {
            if (other == null) {
                return false;
            }

            return R.Equals(other.Value, Value);
        }

        public object Filter(dynamic pred) {
            return pred(Value) ? this : (object)_Nothing.Nothing();
        }

        public object Map(dynamic f) {
            return Of(f(Value));
        }

        public object Ap(IMappable m) {
            return m.Map(Value);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        public override string ToString() {
            return $"Just({R.ToString(Value)})";
        }
    }
}
