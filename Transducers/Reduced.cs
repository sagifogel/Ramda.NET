namespace Ramda.NET
{
    internal class Reduced
    {
        public Reduced(object value) {
            Value = value;
            IsReduced = true;
        }

        internal object Value { get; private set; }
        internal bool IsReduced { get; private set; }
    }
}
