namespace Ramda.NET
{
    internal interface ITransducer
    {
        dynamic Init();
        dynamic Step();
        dynamic result();
    }
}
