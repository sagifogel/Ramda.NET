namespace Ramda.NET
{
    internal static class CurryingExtensions
    {
        internal static object AssignIfArgumentInRange(this object[] arguments, int index) {
            if (arguments.IsNull()) {
                return null;
            }

            if (arguments.IsJaggedArray() && index == 0) {
                return arguments;
            }

            return index <= arguments.Length - 1 ? arguments[index] : null;
        }
    }
}
