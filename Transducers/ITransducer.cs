namespace Ramda.NET
{
    internal interface ITransducer
    {
        object Init();
        object Result(object result);
        object Step(object result, object input);
    }
}
