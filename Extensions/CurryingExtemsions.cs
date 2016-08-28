namespace Ramda.NET
{
    internal static class CurryingExtensions
    {
        internal static object AssignIfArgumentInRange(this object[] arguments, int index) {
            return arguments != null && index <= arguments.Length - 1 ? arguments[index] : null;
        }

        private static void Stam() {
            object[] args = null;

            args.AssignIfArgumentInRange(0);
        }
    }
}
