namespace Ramda.NET
{
    internal class StepCatString : ITransformer
    {
        public object Init() {
            return string.Empty;
        }

        public object Result(object result) => result;

        public object Step(object result, object input) {
            return result.ToString() + input.ToString();
        }
    }
}
