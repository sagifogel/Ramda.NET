namespace Ramda.NET
{
    internal class ReducedImpl : IReduced
    {
        public ReducedImpl(object value) {
            Value = value;
            Reduced = true;
        }

        public object Value { get; private set; }
        public bool Reduced { get; private set; }
    }
}
