namespace Ramda.NET
{
    internal interface IReduced
    {
        object Value { get; }
        bool IsReduced { get; }
    }
}
