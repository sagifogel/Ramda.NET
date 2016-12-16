namespace Ramda.NET
{
    public class Nothing
    {
        internal Nothing() {
        }

        public override bool Equals(object obj) {
            if (obj is Nothing) {
                return true;
            }

            return Equals(obj, null);
        }

        public override int GetHashCode() {
            return base.GetHashCode();
        }
    }
}
