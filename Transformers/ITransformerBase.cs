namespace Ramda.NET
{
    public interface ITransformerBase
    {
        object Init();
        object Result(object result);
    }
}
