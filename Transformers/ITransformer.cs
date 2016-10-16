namespace Ramda.NET
{
    public interface ITransformer : ITransformerBase
    {   
        object Step(object result, object input);
    }
}
