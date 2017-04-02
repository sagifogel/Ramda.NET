using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET.Tests
{
    public class Just : IEquatable<Just>
    {
        public object Value { get; private set; }

        public Just(object value) {
            Value = value;
        }

        public override bool Equals(object obj) {
            return Equals(obj as Just);
        }

        public bool Equals(Just other) {
            if (other == null) {
                return false;
            }

            return R.Equals(other.Value, Value);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }
    }
}
