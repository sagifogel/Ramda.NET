using System;
using System.Threading.Tasks;

namespace Ramda.NET
{
    internal static class TaskExtensions
    {
        internal static async Task Then(this Task<object> task, Func<object, Task<object>> continuation) {
            await continuation(await task);
        }
    }
}
