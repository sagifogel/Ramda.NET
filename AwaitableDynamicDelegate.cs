using System;
using System.Dynamic;
using System.Threading.Tasks;
using System.Collections.Generic;
using static Ramda.NET.ReflectionExtensions;
using System.Threading;

namespace Ramda.NET
{
    public class AwaitableDynamicDelegate : DynamicDelegate
    {
        protected object[] arguments;
        private Func<dynamic, Task<dynamic>> fn;

        public AwaitableDynamicDelegate(Func<dynamic, Task<dynamic>> fn) {
            this.fn = fn;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] arguments, out object result) {
            this.arguments = arguments;
            result = this;

            return true;
        }

        public dynamic GetAwaiter() {
            dynamic task = DynamicInvoke(fn, arguments);

            return task.GetAwaiter();
        }

        public dynamic Result => GetAwaiter().GetResult();

        public override Delegate Unwrap() {
            return null;
        }
    }
}
