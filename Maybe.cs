using System;

namespace Ramda.NET
{
    public class Maybe
    {
        public object Value { get; private set; }

        internal Maybe(object obj = null) {
            Value = obj;
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }

        public override bool Equals(object obj) {
            Maybe maybe;

            if (Equals(null, Value) || Equals(null, obj)) {
                return false;
            }

            maybe = obj as Maybe;

            if (maybe != null) {
                obj = maybe.Value;
            }

            return obj == Value;
        }
    }
}
