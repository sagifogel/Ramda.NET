using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ramda.NET.Tests
{
    public class _Maybe
    {
        protected object Value { get; private set; }
        protected static _Nothing _nothing = new _Nothing(null);

        public static _Maybe Maybe(object value) {
            return value.IsNull() ? _nothing : (_Maybe)Just(value);
        }

        public _Maybe(object value) {
            Value = value;
        }

        public static _Nothing Nothing() {
            return _nothing;
        }

        public static Just Just(object value) {
            return new Just(value);
        }
        
        public static Just Of(object value) {
            return Just(value);
        }

        public override bool Equals(object obj) {
            return Equals(obj as Just);
        }

        public virtual bool Equals(Just other) {
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
