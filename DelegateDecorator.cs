using System;
using System.Dynamic;
using static Ramda.NET.Currying;
using System.Collections.Generic;

namespace Ramda.NET
{
    internal class DelegateDecorator : DynamicDelegate
    {
        private Delegate @delegate;

        internal DelegateDecorator(Delegate fn) {
            @delegate = fn;
            Length = fn.Method.GetParameters().Length;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] arguments, out object result) {
            result = @delegate.Invoke(arguments);
            return true;
        }
    }
}
