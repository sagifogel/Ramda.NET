namespace Ramda.NET
{
    internal class ReducedImpl : IReduced
    {
        public ReducedImpl(object value) {
            Value = value;
            IsReduced = true;
        }

        public object Value { get; private set; }
        public bool IsReduced { get; private set; }
    }
}
