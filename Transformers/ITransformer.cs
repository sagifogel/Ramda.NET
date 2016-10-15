namespace Ramda.NET
{
    public interface ITransformer
    {
        object Init();
        object Result(object result);
        object Step(object result, object input);
    }
}
