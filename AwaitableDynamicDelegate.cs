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
        private readonly Delegate fn;

        public AwaitableDynamicDelegate(Delegate fn) {
            this.fn = fn;
            Length = fn.Method.GetParameters().Length;
        }

        internal AwaitableDynamicDelegate(Func<dynamic[], Task<dynamic>> fn) {
            this.fn = fn;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] arguments, out object result) {
            this.arguments = arguments;
            result = this;

            return true;
        }

        public dynamic GetAwaiter() {
            dynamic task = fn.Invoke(arguments);

            return task.GetAwaiter();
        }

        public dynamic Result => GetAwaiter().GetResult();

        public override Delegate Unwrap() {
            return null;
        }
    }
}
