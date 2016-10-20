namespace Ramda.NET
{
    public class Nothing
    {
        internal Nothing() {
        }

        public override bool Equals(object obj) {
            return Equals(obj, null);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
