using System;
using System.Dynamic;
using static Ramda.NET.Currying;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal class DelegateDecorator : DynamicDelegate
    {
        private readonly Delegate @delegate;

        internal DelegateDecorator(Delegate fn) {
            @delegate = fn;
            Length = fn.Method.GetParameters().Length;
        }

        internal DelegateDecorator(Func<object[], object> fn) : this((Delegate)fn) {
        }

        public object InvokeWithArray(object[] arguments) {
            return @delegate.InvokeWithArray(arguments);
        }

        public override bool TryInvoke(InvokeBinder binder, object[] arguments, out object result) {
            result = @delegate.Invoke(arguments);
            return true;
        }
    }
}
